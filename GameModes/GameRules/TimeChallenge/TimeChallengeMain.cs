using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TimeChallengeMain : MonoBehaviour
{
    public Text TimerText;
    public float _time;
    public ChallengeDetails CD;
    public UIScript UI;

    //Top Scores
    public int MostCellsInfected;
    public int MostDNA_Collected;
    public bool ShowReward = false;
    //end of top scores

    public GameObject PlayButtonTIME;
    public GameObject PlayButtonADV;
    public GameObject Reward;

    public static TimeChallengeEntity CurrentChallenge;

    private SavedData data = new SavedData();

    private void OnEnable()
    {
        ChallengeDetails.Reward = Reward;

        SetGameRules.TimeChallengeStart += () =>
        {
            PlayButtonTIME = GameObject.Find("PlayTime");
            PlayButtonADV = GameObject.Find("PlayADV");
            PlayButtonTIME.SetActive(true);
            PlayButtonADV.SetActive(false);
        };
        SetGameRules.AdventureStart += () =>
        {
            PlayButtonTIME = GameObject.Find("PlayTime");
            PlayButtonADV = GameObject.Find("PlayADV");
            PlayButtonADV.SetActive(true);
            PlayButtonTIME.SetActive(false);
        };
    }

    public void Challenge()
    {
        if (!gameObject.GetComponents<TimeChallengeEntity>().Any(t => t != null))
        {
            TimeChallengeEntity TCE = gameObject.AddComponent<TimeChallengeEntity>();
            TCE.EntityConstruct(_time, TimerText, this, UI);
            CurrentChallenge = TCE;
        }     
    }

    public void CopyData(bool start)
    {
        if (start)
        {
            MostCellsInfected = CD._selectedEntity.MostCellsInf;
            MostDNA_Collected = CD._selectedEntity.MostDNACol;
            ShowReward = CD._selectedEntity.RewardAvailable;
        }
        else
        {
            CD._selectedEntity.MostCellsInf = MostCellsInfected;
            CD._selectedEntity.MostDNACol = MostDNA_Collected;
            CD._selectedEntity.RewardAvailable = ShowReward;

            data.Add(CD._selectedEntity.MostCellsInf, $"MostCellsInf{CD._selectedEntity.ToString()}");
            data.Add(CD._selectedEntity.MostDNACol, $"MostDNACol{CD._selectedEntity.ToString()}");
        }
        
    }

    //public void ShowRewardM()
    //{
    //    CD.ShowReward(true);
    //    Debug.Log("Show Reward");
    //}
}


