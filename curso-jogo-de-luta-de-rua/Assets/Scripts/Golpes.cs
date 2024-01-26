using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golpes : MonoBehaviour
{
    [SerializeField] private int danoDoGolpe;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Roda se colidir com o Jogador
        if (other.gameObject.GetComponent<VidaDoJogador>() != null)
        {
            other.gameObject.GetComponent<VidaDoJogador>().LevarDano(danoDoGolpe);
        }
        
        // Roda se colidir com o Inimigo
        else if (other.gameObject.GetComponent<VidaDoInimigo>() != null)
        {
            other.gameObject.GetComponent<VidaDoInimigo>().LevarDano(danoDoGolpe);
        }
    }
}
