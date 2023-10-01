using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStart : MonoBehaviour
{
    public Virus _virus;
    //public Virus DefaultVirusType;
    
    public Transform _virusStartPosition;
    public GameObject _virusGameobject;

    public Text text;
    //public List<Virus> list = new List<Virus>();

    private bool Active { get { return _virus.active; } set { _virus.active = value; } }
    private bool call;

    //[SerializeField]
    //private GameObject DefaultText;
    [SerializeField]
    private Virus DefaultVirus;
    [SerializeField]
    private PurchasableMain PM;
    private PurchasableEntity PE;
    private SavedData data = new SavedData();

    private void OnEnable()
    {     
        PE = DefaultVirus.GetComponentInChildren<PurchasableEntity>();
        PM.Buy(PE);
        PE.Owned = true;
        call = true;
        PE.Selected = true;
        data.Add(PE.Owned, $"Owned{PE.ToString()}{PE.parent}");
        text = PE.PriceText;
        _virus = DefaultVirus;
    }

    public void IsUsing(GameObject ChangeText)
    {
        
        if (_virus != null && call && _virus.PEntity.Owned)
        {
            text = ChangeText.GetComponent<Text>();
            Active = true;
            text.text = $"<b>Active</b>";

            Debug.Log("using " + _virus);
        }
          
    }

    public void SetVirus(Virus v)
    {
        if (v != _virus && _virus != null && v.PEntity.Owned)
        {
            text.text = $"<b>Use</b>";
            Active = false;
            //list.Clear();           
        }

        if (v != _virus && v.PEntity.Owned)
        {
            _virus = v;
            call = true;
            //list.Add(v);
        }
        else
        {
            call = false;
        }         
    }  

    public void ReplaceVirus()
    {
        Virus v;
        v = _virusGameobject.GetComponent<Virus>();

        v.ability = _virus.ability;
        v.InfectRate = _virus.InfectRate;
        v.InfectDelay = _virus.InfectDelay;
        v.prey = _virus.prey;
        v.Speed = _virus.Speed;
        v.VirusType = _virus.VirusType;
        v.VirusUpgrades = _virus.VirusUpgrades;
        v.Health = _virus.Health;
        v.VirusModel = _virus.VirusModel;

       if (TimeChallengeMain.CurrentChallenge._time >= TimeChallengeMain.CurrentChallenge.StartTime)
        {
            HealthMain.Health = v.Health;
            HealthMain.pSlide.maxValue = v.Health;
        }
          
    }

    public void SetVirusModel(SpriteRenderer s)
    {
        Virus v = _virusGameobject.GetComponent<Virus>();
        s.sprite = v.VirusModel;
    }
}
