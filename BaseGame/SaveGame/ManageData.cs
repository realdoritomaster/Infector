using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageData : MonoBehaviour
{
    public void DeleteAllSavedData()
    {
        PlayerPrefs.DeleteAll();
    }
}
