using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InfectCell : MonoBehaviour
{
    public delegate void InfectionEvents(int amount);
    public static event InfectionEvents OnCellDestroyed, OnDNACollected, DamageTaken;

    public GameObject Connection;
    public VirusMovement virus;
    public bool moveable = true;
    private bool inObject;

    public Economy eco;
    public Virus V;
    public TextAnimationMain textMain;
    Coroutine CE;  
   
    public static TMPro.TextMeshPro _text;
    public GameObject TEXT;
    public Transform TextTrans;
    public LevelMain _levelMain;

    public Material Mat;
    private float DNApercentage;
    private float predna;
    private bool doFade;

    [SerializeField]
    private HealthMain HealthM;

    public void Infect()
    {
        if (virus.CellNear && virus.AttachedToCell == false && !inObject)
        {            
            _text = TEXT.GetComponent<TMPro.TextMeshPro>();
            TextTrans = virus.co.gameObject.GetComponent<Transform>();
            //Mat = virus.co.gameObject.GetComponent<Material>();

            SetMaterial(virus.co.gameObject);

            virus.AttachedToCell = true;
            moveable = true;
            Quaternion look;
            GetAngle angle = new GetAngle();
            look = angle.LookAt2D(virus.co.gameObject.transform, virus.transform, virus.Child, 90);
            virus.Child.rotation = look;
            
            
            CE = StartCoroutine(EcoUpdate());
        }
        else if (virus.CellNear && virus.AttachedToCell)
        {
            virus.AttachedToCell = false;
            StopCoroutine(CE);
        }
    }

    void SetMaterial(GameObject obj)
    {
        obj.GetComponent<Renderer>().material = Mat;
        obj.GetComponent<Renderer>().material.SetFloat("_Fade", 100);
    }

    void Update()
    {
        if (doFade)
        {
            Microbe mi = virus.co.gameObject.GetComponent<Microbe>();
            Material ma = virus.co.gameObject.GetComponent<Renderer>().material;

            predna = (mi.TotalDNA);
            DNApercentage = Mathf.Lerp(ma.GetFloat("_Fade"), (predna / mi.StartDNA) * 100, Time.deltaTime * 2);
            ma.SetFloat("_Fade", DNApercentage); // current DNA % total DNA

            if (mi.TotalDNA <= 0 && mi.TotalRNA <= 0 && DNApercentage <= 1)
            {

                doFade = false;
                Destroy(mi.gameObject);
                StopCoroutine(CE);

                OnCellDestroyed(1);
                Debug.Log("Infect Cell Infected!");
            }
        }
        

        if (moveable)
        {
            virus.Topos -= virus.Child.transform.up;
        }
    }

    private void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.tag == "Cell" && virus.AttachedToCell)
        {
            moveable = false;
            virus.Topos = virus.transform.position;
            inObject = true;
        } else if (c.gameObject.tag == "Cell")
        {
            inObject = true;
        } else if (c.gameObject.tag == "Obstacle")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        } //reset scene if hit obstacle
        else if (c.gameObject.tag == "Enemy")
        {
            HealthMain.Health -= (5 * LevelEntity.Level)/2; // Enemy Damage
            DamageTaken(5 * Mathf.CeilToInt(LevelEntity.Level / 3));
            Effect.Explode(c.gameObject);
            
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Cell")
        {
            moveable = false;
            virus.AttachedToCell = false;
            inObject = false;
        }
        
    }

    void DestroyInst()
    {
        if (textMain.instList != null)
            foreach (GameObject i in textMain.instList)
                    Destroy(i);
    }

    private IEnumerator EcoUpdate()
    {             
             
        Debug.Log("Coroutine called");
        Microbe mi = virus.co.gameObject.GetComponent<Microbe>();
        Material ma = virus.co.gameObject.GetComponent<Renderer>().material;

        while ((mi.TotalDNA > 0 || mi.TotalRNA > 0) && virus.AttachedToCell)
        {
            int factor = V.VirusUpgrades.entities.Count > 0 ? V.VirusUpgrades.entities[1].Factor : 0;

            doFade = true;
            while (mi.TotalDNA > 0)
            {

                float infRate = V.InfectRate + factor; //adds upgrade factor to infect rate for DNA
                if (mi.TotalDNA < infRate)
                {
                    infRate = mi.TotalDNA;
                }
                else
                {
                    infRate = V.InfectRate + factor;
                }

                _levelMain.AddEXP(2 + (int)infRate);
                Economy._dNA += Mathf.RoundToInt(infRate);
                mi.TotalDNA -= Mathf.RoundToInt(infRate);
                _text.text = $"+{infRate} DNA";

                if (SetGameRules.SelectedGamemode == GameModes.Time_Challenge)                   
                    OnDNACollected((int)infRate); //Event

                textMain.AddText(TextAnimationMain.Currency.DNA, TEXT);

                Debug.Log(DNApercentage);
                Debug.Log("text added");

                yield return new WaitForSeconds(V.InfectDelay);
                break;
            }

            while (mi.TotalRNA > 0)
            {
                float infRate = V.InfectRate + factor; //adds upgrade factor to infect rate for RNA
                if (mi.TotalRNA < infRate)
                {
                    infRate = mi.TotalRNA;
                }
                else
                {
                    infRate = V.InfectRate;
                }

                _levelMain.AddEXP(10 + ((int)infRate * 5));
                Economy._rNA += Mathf.RoundToInt(infRate);
                mi.TotalRNA -= Mathf.RoundToInt(infRate);
                _text.text = $"+{infRate} RNA";
                textMain.AddText(TextAnimationMain.Currency.RNA, TEXT);
                Debug.LogWarning($"RNA: {Economy._rNA}");

                yield return new WaitForSeconds(V.InfectDelay);
                break;
            }

        }
        //StopCoroutine(CE);

    }
}
