using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameModes
{
    Adventure,
    Time_Challenge,
    Enemy_Waves,
    Party_Mode
}

public interface IGameMode
{
    GameModes gamemode { get; }
}

