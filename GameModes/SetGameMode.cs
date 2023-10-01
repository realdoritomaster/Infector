using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SetGameMode : MonoBehaviour, IGameMode
{
    public GameModes _gamemode;
    public GameModes gamemode { get { return _gamemode; } }

    public void ReturnGameMode()
    {
        PlayerPrefs.SetInt("GameMode", (int)gamemode);
        
    }
    
}
