using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public int Speed;
    public Vector3 inertia;
    public Transform _startLocation;
    public GameObject target;
    public int offset;

    public abstract void EnemyBehavior();

}
