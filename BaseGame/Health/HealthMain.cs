using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthMain : MonoBehaviour
{
    [SerializeField]
    private Slider _Slider;
    [SerializeField]
    private Text HealthText;
    [SerializeField]
    private UIScript UI;

    [HideInInspector]
    public static Slider pSlide;

    public static int Health; //default = 100

    private void OnEnable()
    {
        Health = 100;
    }

    private void Start()
    {
        pSlide = _Slider;
        _Slider.minValue = 0;
        _Slider.maxValue = Health;
    }
    private void Update()
    {        
        _Slider.value = Mathf.Lerp(_Slider.value, Health, .2f);
        HealthText.text = Health.ToString();

        if (Health <= 0)
        {
            UI.ToMenu();
            Health = (int)pSlide.maxValue;
            pSlide.value = pSlide.maxValue;
            Destroy(TimeChallengeMain.CurrentChallenge);
        }
            
    }
}
