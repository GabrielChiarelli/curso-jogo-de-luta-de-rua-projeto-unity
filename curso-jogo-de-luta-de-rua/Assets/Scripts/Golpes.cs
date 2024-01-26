using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golpes : MonoBehaviour
{
    [SerializeField] private int danoDoGolpe;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<VidaDoJogador>() != null)
        {
            other.gameObject.GetComponent<VidaDoJogador>().LevarDano(danoDoGolpe);
        }
    }
}
