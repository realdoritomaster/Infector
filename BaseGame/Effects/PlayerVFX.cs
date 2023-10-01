using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVFX : MonoBehaviour
{
    private VirusMovement virus;
    public float VFXSpeed;
    Coroutine cor;
    private bool IsPlaying;
    public Color _setColor;
    public Color _OriginalColor;

    void Start()
    {
        virus = gameObject.GetComponent<VirusMovement>();
    }

    public void Effects()
    {
        virus.Child.localScale = virus.CellNear ? new Vector3(1.5f, 1.5f, 1) : new Vector3(1, 1, 1);
        virus.InfectButton.SetActive(virus.CellNear);

        VFXm();
        //cor = StartCoroutine(VFX());
        
    }

    public void VFXm()
    {
        SpriteRenderer[] renderers = gameObject.GetComponentsInChildren<SpriteRenderer>();
        if (virus.CellNear)
        {
            foreach (SpriteRenderer i in renderers)
                i.color = Color.Lerp(i.color, _setColor, Time.deltaTime * VFXSpeed);
        }
        else
        {
            foreach (SpriteRenderer i in renderers)
            {
                i.color = Color.Lerp(i.color, _OriginalColor, Time.deltaTime * VFXSpeed);
            }
        }
    }

   /* public IEnumerator VFX()
    {
        SpriteRenderer[] renderers = gameObject.GetComponentsInChildren<SpriteRenderer>();
        
        if (virus.CellNear)
        {
            while (virus.CellNear)
            {
                foreach (SpriteRenderer i in renderers)
                    i.color = Color.Lerp(i.color, Color.red, Time.deltaTime);

                yield return null;
            }
        }
        else
        {
            while (virus.CellNear == false)
            {
                foreach (SpriteRenderer i in renderers)
                {
                    i.color = Color.Lerp(i.color, Color.white, Time.deltaTime);

                    if (i.color == Color.white)
                    {
                        StopCoroutine(cor);

                    }

                }

                yield return null;
            }
        }   
    }*/
}
