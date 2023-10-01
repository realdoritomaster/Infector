using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class SetGameRules : MonoBehaviour, IGameMode
{
    public GameModes gamemode { get { return (GameModes)PlayerPrefs.GetInt("GameMode"); }}
    public static GameModes SelectedGamemode;
    public delegate void PerformGameRules();
    public static event PerformGameRules AdventureStart, AdventureUpdate, EnemyWavesStart, EnemyWavesUpdate, TimeChallengeStart, TimeChallengeUpdate;
    //public EditorGUILayout tab;

    private float _tScale;
    public float _timeScale { get { return _tScale; } set { Time.timeScale = value; } }

    void Start()
    {

        switch (gamemode)
        {
            case GameModes.Adventure:

                _tScale = 1;
                SelectedGamemode = GameModes.Adventure;
                if (AdventureStart != null)
                    AdventureStart();              

                break;
            case GameModes.Enemy_Waves:

                _tScale = 1;
                SelectedGamemode = GameModes.Enemy_Waves;
                EnemyWavesStart();

                break;
            case GameModes.Time_Challenge:

                SelectedGamemode = GameModes.Time_Challenge;
                _tScale = 1;
                TimeChallengeStart();

                break;

        }

        Debug.Log("Gamemode: " + gamemode + " is active");
    }

    void Update()
    {
        switch (gamemode)
        {
            case GameModes.Adventure:
                if (AdventureUpdate != null)
                    AdventureUpdate();
                break;

            case GameModes.Time_Challenge:
                if (TimeChallengeUpdate != null)
                    TimeChallengeUpdate();
                break;
        }
        Debug.Log("Gamemode: " + gamemode + " is running");
    }

    void BaseRules()
    {
        Time.timeScale = _timeScale;
    } 
}
