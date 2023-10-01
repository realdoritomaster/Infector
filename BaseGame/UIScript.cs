using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.Experimental.Rendering.Universal;
using System;

public class UIScript : MonoBehaviour
{
    public GameObject Menu;
    public Volume volume;
    public SetGameRules rules;
    [SerializeField]
    private Ads AD;

    public delegate void UIEvents(bool active);
    public static event UIEvents Paused, Started;

    void Start()
    {
        Time.timeScale = 0;
    }

    public void Continue() //Unique method
    {
        if (Started != null)
            Started(true);
        Paused(false);

        Menu.SetActive(false);
        rules._timeScale = rules._timeScale;
    }

    public void ToMenu() //Unique method
    {
        if (Started != null)
            Started(false);
        Paused(true);

        Menu.SetActive(true);
        rules._timeScale = 0;

        int rand = UnityEngine.Random.Range(1, 4);
        if (rand == 1)
        {
            AD.PlayAd();
        }
    }

    public void ToScene(string level)
    {
        SceneManager.LoadScene(level);
        if (SceneManager.GetActiveScene().name == "SampleScene")
            Paused(true);
    }

    public void Close(GameObject UI)
    {
        UI.SetActive(false);
    }

    public void Open(GameObject UI)
    {
        UI.SetActive(true);
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
            Paused(true);
    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus)
            Paused(false);
    }

    private void OnApplicationQuit()
    {
        Paused(true);
    }


}
