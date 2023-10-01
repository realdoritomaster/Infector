using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SavedData : MonoBehaviour
{
    public void Add<T>(T item, string name)
    {
        switch (Type.GetTypeCode(typeof(T)))
        { 
            case TypeCode.Int32:
                Debug.Log("Added Pref");
                PlayerPrefs.SetInt(name, Convert.ToInt32(item));
                PlayerPrefs.Save();
                break;

            case TypeCode.Single:
                PlayerPrefs.SetFloat(name, Convert.ToSingle(item));
                PlayerPrefs.Save();
                break;

            case TypeCode.Boolean:
                int i = Convert.ToBoolean(item) ? 1 : 0;
                PlayerPrefs.SetInt(name, i);
                PlayerPrefs.Save();
                break;
        }
    }

    public object Get<T>(T type, string name)
    {
        switch (Type.GetTypeCode(typeof(T)))
        {
            case TypeCode.Int32:
                //Debug.Log("Copied pref");
                return PlayerPrefs.GetInt(name);

            case TypeCode.Single:
                return PlayerPrefs.GetFloat(name);

            case TypeCode.Boolean:
                return PlayerPrefs.GetInt(name) == 1;
                
        }
        return true;
    }

    public object Get<T>(T type, T defaultValue, string name)
    {
        switch (Type.GetTypeCode(typeof(T)))
        {
            case TypeCode.Int32:
                //Debug.Log("Copied pref");
                return PlayerPrefs.GetInt(name, Convert.ToInt32(defaultValue));

            case TypeCode.Single:
                return PlayerPrefs.GetFloat(name, Convert.ToSingle(defaultValue));

            case TypeCode.Boolean:
                return PlayerPrefs.GetInt(name, Convert.ToInt32(defaultValue)) == 1;

        }
        return true;
    }

    public void Remove(string item)
    {
        PlayerPrefs.DeleteKey(item);
    }
}
