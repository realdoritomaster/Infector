using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

[ExecuteInEditMode]
public class Microbe : MonoBehaviour
{
    public MicrobeMain main;
    public int TotalDNA;

    public int StartDNA;

    public int TotalRNA;

    public int StartRNA;
    public MicrobeType MT;

    void Start()
    {   
        MT = (MicrobeType)Random.Range(0, 3);

        gameObject.name = MT.ToString();
        switch (MT)
        {
            
            case MicrobeType.Bacteria:
                TotalDNA = Random.Range(1, main.Max_Bacteria * Mathf.RoundToInt(Mathf.Pow(LevelEntity.Level, .5f)));
                TotalRNA = Random.Range(0, 2);
                StartDNA = TotalDNA;
                StartRNA = TotalRNA;
                break;                  
            case MicrobeType.Cell:      
                TotalDNA = Random.Range(1, main.Max_Cells * Mathf.RoundToInt(Mathf.Pow(LevelEntity.Level, .5f)));
                StartDNA = TotalDNA;
                
                break;                  
            case MicrobeType.Virus:     
                TotalDNA = Random.Range(1, main.Max_Viruses * Mathf.RoundToInt(Mathf.Pow(LevelEntity.Level, .5f)));
                TotalRNA = Random.Range(0, 2);
                StartDNA = TotalDNA;
                StartRNA = TotalRNA;
                break;
        }
        
        
    }

}
