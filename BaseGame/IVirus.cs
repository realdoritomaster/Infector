using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Prey
{
    Bacteria,
    Cells,
    Viruses
}
public enum Ability
{
    Duplication,
    Speed_Factor,
    Infect_Factor,
    None
}

public interface IVirus
{

    Prey prey { get; set; }
    Ability ability { get; set; }
    int InfectRate { get; set; }
    int Health { get; set; }
    float InfectDelay { get; set; }
    Sprite VirusModel { get; set; }
    
    float Speed { get; set; }
    string VirusType { get; set; }
}


