using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextAnimationLocal : MonoBehaviour
{
    public bool AnimationEnabled = false;
    public InfectCell infect;
    public TextAnimationMain TextMain;

    private MeshRenderer renderer;
    public Animator anim;
    

    public void Instantiated()
    {
        anim = gameObject.GetComponent<Animator>();
        renderer = gameObject.GetComponent<MeshRenderer>(); //instantiating variables

        renderer.enabled = AnimationEnabled; // match text with own cell gameobject

        anim.SetBool("Plus", AnimationEnabled);
    }

    void Update()
    {
        if (anim != null && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= .90f)
        {
            TextMain.instList.Remove(gameObject);
            
            DestroyImmediate(gameObject);  
            
        }
            
    }
}
