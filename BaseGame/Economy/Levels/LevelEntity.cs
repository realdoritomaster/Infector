using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelEntity : MonoBehaviour
{
    public static int Level; //saved
    public static int EXP; //saved

    public float _increaseBy;
    public float RequiredEXP; //saved

    public Text _levelText;
    private Slider _slider;
    private SavedData data = new SavedData();

    public delegate void Entity();
    public static event Entity EntityEvent;

    private void OnEnable()
    {
        UIScript.Paused += ApplicationPause;

        
        Level = (int)data.Get(Level, 1, "Level");
        RequiredEXP = (float)data.Get(RequiredEXP, 20, "RequiredEXP");      
        EXP = (int)data.Get(EXP, 0, "EXP");        

        Debug.Log("RequiredEXP is: " + RequiredEXP);       
    }

    private void Start()
    {
        
        Debug.Log("RequiredEXP set to: " + RequiredEXP);
        
        _slider = gameObject.GetComponent<Slider>();
        _slider.maxValue = RequiredEXP;
        _slider.value = EXP;

        _levelText.text = Level.ToString();
    }

    private void Update()
    {
        _slider.value = Mathf.Lerp(_slider.value, EXP, Time.deltaTime * 5);

        if (EXP >= RequiredEXP)
        {
            int Dividend = EXP / (int)RequiredEXP;
            
            //EXP = EXP - ((int)(RequiredEXP * Mathf.Pow(_increaseBy, Dividend)));
            Level += Dividend;
            RequiredEXP = (RequiredEXP * Mathf.Pow(_increaseBy, Dividend));
            EXP = 0;
            RequiredEXP = Mathf.Round(RequiredEXP);

            _slider.maxValue = RequiredEXP;
            _levelText.text = Level.ToString();

            if (EntityEvent != null)
            EntityEvent();
        }      
    }

    public void ApplicationPause(bool pause)
    {
        if (pause)
        {
            data.Add(RequiredEXP, "RequiredEXP");
            data.Add(Level, "Level");
            data.Add(EXP, "EXP");
           
        }
    }

}
