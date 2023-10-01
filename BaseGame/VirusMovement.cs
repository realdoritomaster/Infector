using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class VirusMovement : MonoBehaviour
{
    public Joystick joy;
    public float speed;
    public float LerpSpeed;
    public float SlerpSpeed;
    public GetAngle A;
    public Transform Child;
    public GameObject InfectButton;
    private Vector3 vec;
    private PlayerVFX _pVFX;
    public Virus virus;

    [HideInInspector]
    public bool CellNear;
    [HideInInspector]
    public bool AttachedToCell;
    [HideInInspector]
    public Collider2D co; //selected cell
    [HideInInspector]
    public Vector3 Topos;

    void Start()
    {
        Topos = transform.position;
        _pVFX = gameObject.GetComponent<PlayerVFX>();
    }

    void FixedUpdate()
    {
        Movement();
        Rotation();
        _pVFX.Effects();
    }


    void Rotation()
    {
        Quaternion preQ = new Quaternion();
        Quaternion look = new Quaternion();

        if (CellNear == false && Mathf.Abs(joy.Horizontal) > 0 && Mathf.Abs(joy.Vertical) > 0)
        {
            look = A.LookAt2D(A.Joy, A.Background, Child, -90);            
            preQ = look;
        }
        else if (CellNear == true && Mathf.Abs(joy.Horizontal) > 0 && Mathf.Abs(joy.Vertical) > 0)
        {
            look = A.LookAt2D(co.gameObject.transform, transform, Child, 90);            
            preQ = look;
        }
        else
        {
            Child.rotation = Quaternion.Slerp(Child.rotation, preQ, Time.deltaTime * SlerpSpeed);
        }

        Child.rotation = Quaternion.Slerp(Child.rotation, look, Time.deltaTime * SlerpSpeed);
    }

    void Movement()
    {
        int factor = virus.VirusUpgrades.entities.Count > 0 ? virus.VirusUpgrades.entities[0].Factor : 0;
        
        if (AttachedToCell == false)
        {
            Topos += (((transform.up * joy.Vertical) + (transform.right * joy.Horizontal)) * Time.deltaTime) * (speed + factor);          
        }
        transform.position = Vector3.Lerp(transform.position, Topos, Time.deltaTime * LerpSpeed);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool started = false;

        if (collision.gameObject.tag == "Cell" && started == false)
        {
            started = true;
            CellNear = true;            
            co = collision;
            
            StartCoroutine(Sizeup());         
        } else if (collision.gameObject.tag == "Cell" && started == true && co == null)
        {
            CellNear = true;
            co = collision;

            StartCoroutine(Sizeup());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision == co)
        CellNear = false;   
    }

    private IEnumerator Sizeup() // sizeup the infect button
    {
        vec = new Vector3(0, 0, 0);
        while (vec.magnitude <= new Vector3(2, 2, 2).magnitude)
        {
            vec.x += Time.deltaTime * 7;
            vec.y += Time.deltaTime * 7;
            vec.z += Time.deltaTime * 7;
            InfectButton.transform.localScale = vec;
            yield return new WaitForEndOfFrame();
        }
        
    }
}
