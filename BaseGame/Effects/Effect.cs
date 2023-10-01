using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    public static void Explode(GameObject obj)
    {
        Collider2D collider = obj.GetComponent<Collider2D>();

        collider.enabled = false;
        //Effects Start//

        //Effects End//
        Destroy(obj);
    }
}
