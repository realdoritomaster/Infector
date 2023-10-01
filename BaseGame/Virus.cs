using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class Virus : MonoBehaviour, IVirus
{
    [field: SerializeField]
    public Prey prey { get; set; }
    [field: SerializeField]
    public Ability ability { get; set; }
    [field: SerializeField]
    public int InfectRate { get; set; }
    [field: SerializeField]
    public float InfectDelay { get; set; }
    [field: SerializeField]
    public float Speed { get; set; }
    [field: SerializeField]
    public string VirusType { get; set; }
    [field: SerializeField]
    public int Health { get; set; }
    [field: SerializeField]
    public Sprite VirusModel { get; set; }

    public PurchasableStoreDataEntity VirusUpgrades;
    public PurchasableEntity PEntity;

    public bool active = false;
    public Text text;

    void Update()
    {
        if (text != null && text.enabled)
        text.text = $"<b>Virus Type</b> <i>{VirusType}</i>\n <b>Infect Rate</b> <i>{InfectRate}</i>/s\n <b>Infect Delay</b> <i>{InfectDelay}s</i>\n <b>Speed</b> <i>{Speed}</i>\n <b><color=red>Health</color></b> <i>{Health}</i>";
    }
}
