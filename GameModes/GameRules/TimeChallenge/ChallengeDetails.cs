using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChallengeDetails : MonoBehaviour
{
    public Text TimeLimit, MostCellsInf, MostDNACol;
    public ChallengeDetailsEntity _selectedEntity;
    public static GameObject Reward;

    public void setSelectedChallengeDetails(ChallengeDetailsEntity e)
    {
        _selectedEntity = e;
        Reward.SetActive(e.RewardAvailable);
        
        TimeLimit.text = $"Time\n<b><i>{e.Timelimit} Seconds</i></b>";
        MostCellsInf.text = $"Most Cells Infected\n<b><i>{e.MostCellsInf}</i></b>";
        MostDNACol.text = $"Most DNA Collected\n<b><i>{e.MostDNACol}</i></b>";        

    }

    public void GiveReward()
    {
        Economy._dNA += (Economy._dNA / 4) * (int)(LevelEntity.Level / 1.5);
        Reward.SetActive(false);
        _selectedEntity.RewardAvailable = false;
        _selectedEntity.RewardCollected = true;
        _selectedEntity.Details.text = $"Details";
    }
}
