using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ChallengeMain : MonoBehaviour
{
    public GameObject ChallengeEntitiesParent;
    public List<ChallengeEntity> ChallengeEntities = new List<ChallengeEntity>();
    public ChallengeEntity SelectedEntity;
    public TimeChallengeMain TCM;
    public ChallengeDetails CD;

    private void Start()
    {
        if (SetGameRules.SelectedGamemode != GameModes.Time_Challenge)
            gameObject.SetActive(false);

        ChallengeEntities = ChallengeEntitiesParent.GetComponentsInChildren<ChallengeEntity>().ToList();
        StartingChallenge();
    }

    private void StartingChallenge()
    {
        if (SelectedEntity != null)
        {
            SelectedEntity.isStarted = true;
            CD._selectedEntity = SelectedEntity.gameObject.GetComponent<ChallengeDetailsEntity>();
        }
            
    }

    //Functions//
    public void PreviewTimeTXT()
    {
        TCM._time = SelectedEntity.Seconds;
        TCM.TimerText.text = $"Time <b><i>{SelectedEntity.Seconds}</i></b>";
        GameObject.Find("Seconds").GetComponent<Text>().text = $"You will Have <color=orange>{SelectedEntity.Seconds}</color> seconds to collect as much DNA as possible!";
    }

    public void Clicked(ChallengeEntity entity)
    {
        if (!TCM.GetComponents<TimeChallengeEntity>().Any(t => t != null))
        {
            entity.isStarted = true;
            SelectedEntity = entity;
            CD._selectedEntity = entity.gameObject.GetComponent<ChallengeDetailsEntity>();

            foreach (ChallengeEntity e in ChallengeEntities)
            {
                if (e != entity && e.isStarted == true)
                    e.isStarted = false;
            }
        }     
    }
}
