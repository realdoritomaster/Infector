using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Economy : MonoBehaviour
{
    
    public static int _dNA; //Base Currency | saved
    public static int _rNA; //special currency | saved
    public Text dna;
    public Text rna;
    [SerializeField]
    private bool AddCash;

    private SavedData data = new SavedData();

    private void OnEnable()
    {
        UIScript.Paused += ApplicationPause;
        _dNA = (int)data.Get(_dNA, 0, "_dNA");
        _rNA = (int)data.Get(_rNA, 0, "_rNA");
    }

    void Update()
    {
        if (AddCash)
        {
            AddCash = false;
            _dNA += 20000;
        }

        dna.text = _dNA.ToString();
        rna.text = _rNA.ToString();
    }

    private void ApplicationPause(bool pause)
    {
        if (pause)
        {
            //PlayerPrefs.SetInt("DNA", _dNA);
            //PlayerPrefs.SetInt("Chrome", _rNA);
            data.Add(_dNA, "_dNA");
            data.Add(_rNA, "_rNA");
        }
        
    }
}
