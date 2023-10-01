using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMain : MonoBehaviour
{
    public LevelEntity Level;

    public void AddEXP(int exp)
    {
        LevelEntity.EXP += exp;
    }
}
