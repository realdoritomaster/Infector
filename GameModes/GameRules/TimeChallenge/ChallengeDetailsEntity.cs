using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ChallengeDetailsEntity : MonoBehaviour
{
    public int Timelimit, MostCellsInf, MostDNACol; //saved
    public bool RewardCollected, RewardAvailable;
    public Text Details;
    private SavedData data = new SavedData();

    private void OnEnable()
    {
        UIScript.Paused += ApplicationPause;

        MostCellsInf = (int)data.Get(MostCellsInf, 0, $"MostCellsInf{ToString()}");
        MostDNACol = (int)data.Get(MostDNACol, 0, $"MostDNACol{ToString()}");
        Timelimit = GetComponent<ChallengeEntity>().Seconds;       
    }

    public void ApplicationPause(bool paused)
    {
        if (paused)
        {
            data.Add(MostCellsInf, $"MostCellsInf{ToString()}");
            data.Add(MostDNACol, $"MostDNACol{ToString()}");
        }       
    }

}
