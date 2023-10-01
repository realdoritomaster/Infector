using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChallengeEntity : MonoBehaviour
{
    private bool _started = false;
    public bool isStarted 
    { 
        get { return _started; } 
        set 
        { 
            _started = value;
            ButtonText.text = value ? "Started" : "Start";
        } 
    }
    public int Seconds;
    public Text SecondsText;
    public Text ButtonText;

    // Start is called before the first frame update
    void Start()
    {
        SecondsText.text = $"{Seconds}\nSeconds";
    }
    
    
}
