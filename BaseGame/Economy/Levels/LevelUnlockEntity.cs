using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUnlockEntity : MonoBehaviour
{
    public LevelEntity LevelEnt;
    public int RequiredLevel;
    public Text RequiredLevelText;

    [HideInInspector]
    public AdUnlock AU;

    void OnEnable()
    {        
        LevelEntity.EntityEvent += Unlock;
    }

    private void Start()
    {
        Unlock();
    }

    private void Update()
    {
        if (RequiredLevelText != null && RequiredLevelText.gameObject.activeInHierarchy)
            RequiredLevelText.text = $"Level {RequiredLevel} Required";
    }

    private void OnDestroy()
    {
        LevelEntity.EntityEvent -= Unlock;
    }

    private void Unlock()
    {
        try
        {
            if (LevelEntity.Level >= RequiredLevel || AU.Unlocked == true)
            {
                Destroy(gameObject);
            }
        } catch (NullReferenceException) {}              
    }

    public void InstantUnlock() { Destroy(gameObject); }

}
