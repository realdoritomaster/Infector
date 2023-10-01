using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateEffects : MonoBehaviour
{
    public GameObject RotateAround;
    public GameObject[] rotatees;
    public float RotateAroundSpeed;
    public Vector3 RotateSpeed;

    void Update()
    {
        foreach (GameObject r in rotatees)
        {
            r.transform.Rotate(RotateSpeed);
            r.transform.RotateAround(RotateAround.transform.position, Vector3.forward, Time.deltaTime * RotateAroundSpeed);
        }
    }
}
