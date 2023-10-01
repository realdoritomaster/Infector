using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public static Text _menuText;

    private void OnEnable()
    {
        _menuText = GameObject.Find("TitleRule").GetComponent<Text>();

        SetGameRules.AdventureStart += () =>
        {            
            if (_menuText != null)
                _menuText.text = "Adventure";
        };
        SetGameRules.TimeChallengeStart += () =>
        {
            if (_menuText != null)
                _menuText.text = "Time Challenge";
        };
    }
}
