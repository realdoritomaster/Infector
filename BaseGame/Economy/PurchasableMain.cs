using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PurchasableMain : MonoBehaviour
{
    public Economy eco;
    public PurchasableStoreData data;
    public Func<string, Text, string> call = (str, txt) => txt.text = str;

    public void Buy(PurchasableEntity e)
    {

        switch (e._entityType)
        {
            case PurchasableEntity.EntityType.OneTimePurchase:
                if (Economy._dNA >= e.Price && e.Owned == false)
                {
                    Text t = e.GetComponentInChildren<Text>();
                    call($"<b>Use</b>", t);
                    
                    //t.text = "Use";
                    Economy._dNA -= e.Price;
                    e.Owned = true;
                    Debug.Log("Virus bought");
                }
                break;
            case PurchasableEntity.EntityType.MultiplePurchase:
                         
                if (Economy._dNA >= e.Price)
                {                 
                    Economy._dNA -= e.Price;
                    e.Owned = true;
                    e.Factor += 1;

                    if (e.FactorText != null)
                        e.FactorText.text = $"<b>Amount</b> <i>{e.Factor.ToString()}</i>"; 

                    e.Price = e.Factor > 0 ? ((int)(e.Price * Mathf.Pow(e.Factor, .1f))) : e.Price;
                    e.PriceText.text = $"<b>Buy</b> <i>{e.Price}</i>";

                    Debug.Log("Upgrade bought");                   
                }
                break;
        }          
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(PurchasableEntity))]
    public class PurchasableEntity_Editor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            PurchasableEntity script = (PurchasableEntity)target;

            switch (script._entityType)
            {
                case PurchasableEntity.EntityType.MultiplePurchase:
                    script.Factor = EditorGUILayout.IntField("Factor", script.Factor);
                    script.FactorText = EditorGUILayout.ObjectField("Factor Text (Not Required)", script.FactorText, typeof(Text), true) as Text;
                    break;
            }
        }
    }
#endif

}
