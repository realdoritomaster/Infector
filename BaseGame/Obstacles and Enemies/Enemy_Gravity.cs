using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class Enemy_Gravity : Enemy
{
    public float _gravityStrength;
    public float Drag;
    private Vector3 Vec;
    public bool _enableDrag;

    private void OnEnable()
    {
        SetGameRules.TimeChallengeUpdate += EnemyBehavior;
    }

    private void Start()
    {
        Vec = _startLocation.position;
    }

    public override void EnemyBehavior()
    {
        //runs in update()
        Vector3 direction = (target.gameObject.transform.position - gameObject.transform.position).normalized;
        inertia += direction * _gravityStrength;
        Vec += _enableDrag ? ((direction + (inertia / Drag)) / 300) * Speed * Time.deltaTime : ((direction + (inertia)) / 300) * Speed * Time.deltaTime;
        Vec.z = offset;
        gameObject.transform.position = Vec;
    }

    private void OnDisable()
    {
        SetGameRules.TimeChallengeUpdate -= EnemyBehavior;
    }
}
