using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextAnimationMain : MonoBehaviour
{
    public enum Currency { DNA, RNA }
    public VirusMovement virus;

    private Microbe mi;
    
    
    public List<GameObject> instList = new List<GameObject>();
    private MeshRenderer renderer;
    private Animator anim;
    public InfectCell inf;

    public bool enabled { get; set; }



    public void AddText(Currency e, GameObject text)
    {
        mi = virus.co.gameObject.GetComponent<Microbe>();
        
        switch (e)
        {
            case (Currency)0:

                  GameObject inst = Instantiate(text, inf.TextTrans);
                
                Debug.Log(text.name);
                instList.Add(inst.gameObject);
                TextAnimationLocal local = inst.GetComponent<TextAnimationLocal>();
                local.TextMain = this;
                local.AnimationEnabled = true;
                local.Instantiated();

                break;
            case (Currency)1:

                GameObject inst2;
                inst2 = Instantiate(text, inf.TextTrans);
                
                inst2.transform.localScale *= 2;
                instList.Add(inst2.gameObject);
                TextAnimationLocal local2 = inst2.GetComponent<TextAnimationLocal>();
                TMPro.TextMeshPro render = inst2.GetComponent<TMPro.TextMeshPro>();
                render.color = Color.green;
                local2.TextMain = this;
                local2.AnimationEnabled = true;
                local2.Instantiated();

                break;
        }
    }
}
