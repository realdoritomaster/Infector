using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Analytics;
using UnityEngine.Monetization;

public class Ads : MonoBehaviour
{
    private string store_id = "3601465";

    private string rewarded_ad = "rewardedVideo";
    private string video_ad = "video";

    public delegate void AdEvents();
    public static event AdEvents FinishedAd, SkippedAd, ClosedAd;

    private void OnEnable()
    {
        if (Advertisement.isSupported)
        Advertisement.Initialize(store_id, false);
    }

    public void PlayAd()
    {      
        StartCoroutine(ShowAd());
    }

    IEnumerator ShowAd()
    {
        //ready to play?
        while (!Advertisement.IsReady("rewardedVideo"))
        {
            yield return new WaitForSeconds(.1f);
        }
        Advertisement.Show();
    }
}
