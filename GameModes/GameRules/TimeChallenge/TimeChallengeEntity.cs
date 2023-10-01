using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeChallengeEntity : MonoBehaviour
{
    public float StartTime;
    public float _time;
    public static Text TimerText;

    //Scores
    public int CellsInfected;
    public int DNA_Collected;
    [HideInInspector]
    public TimeChallengeMain main;
    private UIScript UI;

    private void OnEnable()
    {
        InfectCell.OnCellDestroyed += AddCellInfected;
        InfectCell.OnDNACollected += AddDNA;      
    }

    public void EntityConstruct(float time, Text text, TimeChallengeMain _main, UIScript UI)
    {       
        _time = time;
        StartTime = time;

        TimerText = text;
        main = _main;
        this.UI = UI;
        main.CopyData(true);        
    }

    private void AddCellInfected(int amount)
    {
        CellsInfected += amount;
        Debug.Log("Cell Infected");
    }

    private void AddDNA(int amount)
    {
        DNA_Collected += amount;
        Debug.Log("DNA Collected");
    }

    private void Start()
    {
        StartCoroutine(StartTimer());
        Debug.Log("Challenge Started");
    }

    public void ChallengeBehavior() //runs when the timer is still ticking.
    {
        if (TimerText != null)
        {
            TimerText.text = $"Time <b><i>{_time}</i></b>"; 
            TimerText.gameObject.SetActive(true);
            
        }
        
        //Debug.Log(_time);
    }

    public IEnumerator StartTimer()
    {
        while (_time > 0)
        {
            --_time;
            yield return new WaitForSeconds(1);
            ChallengeBehavior();            
        }
        UI.ToMenu();
        TimerText.gameObject.SetActive(false);
        HealthMain.Health = (int)HealthMain.pSlide.maxValue;
        HealthMain.pSlide.value = HealthMain.pSlide.maxValue;
        
        Destroy(this);
        
    }

    private void OnDestroy()
    {
        if ((DNA_Collected > main.MostDNA_Collected) || (CellsInfected > main.MostCellsInfected))
        {
            main.ShowReward = true;
            main.CD._selectedEntity.Details.text = $"<b><color=orange>Reward</color></b>";
        }
        main.MostCellsInfected = CellsInfected > main.MostCellsInfected ? CellsInfected : main.MostCellsInfected;       
        main.MostDNA_Collected = DNA_Collected > main.MostDNA_Collected ? DNA_Collected : main.MostDNA_Collected;

        if (TimerText != null)
            TimerText.gameObject.SetActive(false);
        Debug.Log("DNA"+DNA_Collected + " | " + "MostDNA" + main.MostDNA_Collected);
        main.CopyData(false);
        TimeChallengeMain.CurrentChallenge = null;
        
    }

    private void OnDisable()
    {
        InfectCell.OnCellDestroyed -= AddCellInfected;
        InfectCell.OnDNACollected -= AddDNA;       
        Debug.Log("Time Challenge Ended!");
    }
}
