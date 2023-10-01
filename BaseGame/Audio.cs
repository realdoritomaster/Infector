using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Audio : MonoBehaviour
{
    [Range(0,100)] 
    public float Volume;

    [SerializeField]
    private AudioSource Source;

    [SerializeField]
    private AudioClip Destroyed, Collected, Hurt;

    [SerializeField]
    private Slider slider;

    private void OnEnable()
    {
        InfectCell.OnCellDestroyed += PlayDestroyed;
        InfectCell.OnDNACollected += PlayCollected;
        InfectCell.DamageTaken += Damaged;
    }

    void Start()
    {
        Source = gameObject.GetComponent<AudioSource>();
        Volume = 100;
    }

    private void Update()
    {
        if (gameObject.activeInHierarchy)
            OnChange();
    }

    public void OnChange()
    {
        Volume = slider.value;
    }

    public void PlayCollected(int amount)
    {
        Source.PlayOneShot(Collected, Volume);
    }

    public void PlayDestroyed(int amount)
    {
        Source.PlayOneShot(Destroyed, Volume);
        
    }

    public void Damaged(int amount)
    {
        Source.PlayOneShot(Hurt, Volume);
    }

    private void OnDisable()
    {
        InfectCell.OnCellDestroyed -= PlayDestroyed;
        InfectCell.OnDNACollected -= PlayCollected;
        InfectCell.DamageTaken -= Damaged;
    }

}
