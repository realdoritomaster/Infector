using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetAngle : MonoBehaviour
{
    public Transform Background;
    public Transform Joy;
    public float Angle;
    

    public Quaternion LookAt2D(Transform target, Transform MyTransform, Transform RotateObject, float AngleOffset)
    {
        Vector3 XY = target.position - MyTransform.position;
        float Radians = Mathf.Atan2(XY.y , XY.x);
        Angle = Radians * Mathf.Rad2Deg;
        
        Quaternion Q = Quaternion.AngleAxis((Angle + AngleOffset), Vector3.forward);
        return Q;
    }
}
