using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject Follow;
    private GameObject OriginalFollow;
    public float LerpTime;
    public float Offset;
    public VirusMovement VM;
    private Vector3 pos;

    private void Start()
    {
        OriginalFollow = Follow;
    }

    void FixedUpdate()
    {
        SwitchFollow();
       
        pos.z = Offset;
        transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime * LerpTime);
        
    }

    void SwitchFollow()
    {
        if (VM.CellNear && VM.co.gameObject != null)
        {
            Follow = VM.co.gameObject;
            pos = Vector3.Lerp(transform.position, (Follow.transform.position + OriginalFollow.transform.position) / 2, Time.deltaTime * LerpTime);
        } else
        {
            Follow = OriginalFollow;
            pos = Vector3.Lerp(transform.position, Follow.transform.position, Time.deltaTime * LerpTime);
        }
    }
}
