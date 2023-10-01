using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using Unity.Cloud.UserReporting.Plugin;
using Unity.Cloud.UserReporting;

public class PurchasableEntity : MonoBehaviour
{
    public enum EntityType { OneTimePurchase, MultiplePurchase }
    public EntityType _entityType;

    //One Time Purchase and Multiple
    public int Price;
    public Text PriceText;
    public bool Owned;
    public bool Selected;

    private SavedData data = new SavedData();

    //Multiple Purchases
    [HideInInspector]
    public int Factor; //only use for mutliple purchases
    [HideInInspector]
    public Text FactorText;
    public int ID;

    public bool UseSavedData = false;
    public GameObject parent;
    
    private void OnEnable()
    {
        UIScript.Paused += ApplicationPause;
        StartCoroutine(StartFunction());
    }

    private IEnumerator StartFunction()
    {
        yield return new WaitForSecondsRealtime(.5f);
        //ID = (int)data.Get(ID, $"ID{ToString()}{parent}");
        if (UseSavedData)
        {
            Factor = (int)data.Get(Factor, 0, $"Factor{(int)data.Get(ID, $"ID{ToString()}{parent}")}");
            Price = (int)data.Get(Price, Price, $"Price{(int)data.Get(ID, $"ID{ToString()}{parent}")}");
        }
        Owned = (bool)data.Get(Owned, false, $"Owned{ToString()}{parent}");


        Debug.Log($"<b>ToString:</b> ID{ToString()}{parent} \n <b>name:</b> ID{name}{parent}");

        Debug.LogWarning(parent + " BEFORE");

        if (FactorText != null)
            FactorText.text = $"<b>Amount</b> <i>{Factor.ToString()}</i>";

        if (PriceText != null && Owned == false)
        {
            PriceText.text = $"<b>Buy</b> <i>{Price}</i>";
        }
        else if (PriceText != null && Owned && _entityType == EntityType.OneTimePurchase && Selected != true)
        {
            PriceText.text = $"<b>Use</b>";
        }
        else
        {
            PriceText.text = $"<b>Active</b>";
        }
    }

    public void ApplicationPause(bool pause)
    {
        if (pause)
        {
            data.Add(ID, $"ID{ToString()}{parent}");
            data.Add(Owned, $"Owned{ToString()}{parent}");

            if (UseSavedData)
            {
                data.Add(Factor, $"Factor{(int)data.Get(ID, $"ID{ToString()}{parent}")}");
                data.Add(Price, $"Price{(int)data.Get(ID, $"ID{ToString()}{parent}")}");
            }
                
        }       
    }

    //public void ApplicationPause()
    //{
    //    if (UseSavedData)
    //        data.Add(Factor, $"Factor{ID}");

    //    data.Add(Owned, $"Owned{ToString()}{parent}");
    //    data.Add(ID, $"ID{ToString()}{parent}");

    //}
}

