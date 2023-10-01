using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PurchasableStoreData : MonoBehaviour
{
    public PurchasableStoreDataEntity DE;
    public GameObject parent;
    public GameObject ContentParent;

    private SavedData data = new SavedData();

    private void Awake()
    {
        Virus[] a = ContentParent.GetComponentsInChildren<Virus>();
        PurchasableEntity[] c = parent.GetComponentsInChildren<PurchasableEntity>();

        for (int k = 0; k < a.Length; k++)
        {
            PurchasableStoreDataEntity v = a[k].VirusUpgrades;
            int x = 0;
            foreach (PurchasableEntity e in c)
            {
                PurchasableEntity p = Instantiate(e);
                p.name = $"PurchasableEntity ({x})";
                
                v.entities.Add(p);
                p.UseSavedData = true;
                p.parent = v.gameObject;
                p.ID = p.name.GetHashCode() + p.parent.GetHashCode();
                Debug.LogWarning("parent set to " + v.gameObject);
                x++;
            }
        }
    }

    public void SetDataEntity(PurchasableStoreDataEntity DE)
    {
        this.DE = DE;
        PurchasableEntity[] e = parent.GetComponentsInChildren<PurchasableEntity>();

        int i = 0;

        foreach (PurchasableEntity entity in e)
        {
            //e[i] = DE.entities[i];
            Debug.Log(e[i].name + " = " + DE.entities[i].name + DE.entities[i].parent);

            e[i].parent = DE.entities[i].parent;
            e[i].Factor = DE.entities[i].Factor;
            e[i].FactorText.text = $"<b>Amount</b> <i>{DE.entities[i].Factor.ToString()}</i>";
            e[i].Owned = DE.entities[i].Owned;
            e[i].Price = DE.entities[i].Price;
            e[i].PriceText.text = $"<b>Buy</b> <i>{DE.entities[i].Price.ToString()}</i>";
            e[i]._entityType = DE.entities[i]._entityType;

            i++;
        }
        
    }

    public void StoreData(GameObject parent)
    {
        PurchasableEntity[] e = parent.GetComponentsInChildren<PurchasableEntity>();
        int i = 0;
        foreach (PurchasableEntity entity in e)
        {

            Debug.Log(DE.entities[i].name + " = " + e[i].name);

            DE.entities[i].parent = e[i].parent;
            DE.entities[i].Factor = e[i].Factor;
            DE.entities[i].FactorText = e[i].FactorText;
            DE.entities[i].FactorText.text = e[i].FactorText.text;
            DE.entities[i].Owned = e[i].Owned;
            DE.entities[i].Price = e[i].Price;
            DE.entities[i].PriceText = e[i].PriceText;
            DE.entities[i]._entityType = e[i]._entityType;

            //if (DE.entities[i].UseSavedData)
            //    data.Add(DE.entities[i].Factor, $"Factor{DE.entities[i].ID}");

            e[i].Factor = 0;
            e[i].FactorText.text = "Factor: 0";
            e[i].Owned = false;
            e[i].Price = DE.entities[i].Price;
            e[i].PriceText.text = "Cost: 0";
            e[i]._entityType = DE.entities[i]._entityType;

            i++;
            { /* if (!DE.entities.Contains(entity))
             {
                 entity.name = $"PEntity ({i})";
                 DE.entities.Add(entity);
                 i++;
             }
             else
             {
                 DE.entities.Clear();
                 entity.name = $"PEntity ({i})";
                 DE.entities.Add(entity);
                 i++;
             }   */
            }
        }       
    }

    //Initialize parent object for PurchasableEntity for saving purposes
    public void InitParent()
    {
        Virus[] a = ContentParent.GetComponentsInChildren<Virus>();
        PurchasableEntity[] c = parent.GetComponentsInChildren<PurchasableEntity>();
        
        for (int k = 0; k < a.Length; k++)
        {
            PurchasableStoreDataEntity v = a[k].VirusUpgrades;
            int x = 0;
            foreach (PurchasableEntity e in c)
            {
                PurchasableEntity p = Instantiate(e);
                             
                p.name = $"PurchasableEntity ({x})";
                v.entities.Add(p);
                p.parent = v.gameObject;  
                x++;
            }
        }       
    }
}
