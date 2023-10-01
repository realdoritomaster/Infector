using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AdUnlock : MonoBehaviour
{
    [SerializeField]
    private int required_watches;
    [SerializeField]
    private int total_watched;
    [SerializeField]
    private Text button_text;
    [SerializeField]
    private int AdDelay;
    [SerializeField]
    private LevelUnlockEntity LUE;
    
    public bool Unlocked;

    private int timer;
    private SavedData data = new SavedData();

    public UnityEvent AllWatched;

    private void OnEnable()
    {
        UIScript.Paused += ApplicationPause;
        LUE.AU = this;
        Unlocked = (bool)data.Get(Unlocked, false, $"Unlocked{ToString()}");        
    }

    private void Start()
    {
        button_text.text = $"<b>OR</b>\nWatch <i>{required_watches - total_watched}</i> ads";
    }

    public void AdWatched(int num)
    {
        if ((total_watched < required_watches - 1) && timer == 0)
        {
            StartCoroutine(ButtonDelay());
            total_watched += num;
            button_text.text = $"<b>OR</b>\nWatch <i>{required_watches - total_watched}</i> ads";          
        }
        else if ((total_watched < required_watches - 1) && timer >= AdDelay) { timer = 0; }
        else
        {
            total_watched += num;
            LUE.InstantUnlock();
            Unlocked = true;
        }
        
    }

    public IEnumerator ButtonDelay()
    {
        yield return new WaitForSeconds(AdDelay);
        timer++;
    }

    public void ApplicationPause(bool pause)
    {
        if (pause)
        {
            data.Add(Unlocked, $"Unlocked{ToString()}");
        }
    }
}
