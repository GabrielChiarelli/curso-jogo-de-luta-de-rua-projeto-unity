using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaDoJogador : MonoBehaviour
{
    public bool jogadorVivo;

    [SerializeField] private int vidaMaxima;
    private int vidaAtual;

    private void Start()
    {
        jogadorVivo = true;
        vidaAtual = vidaMaxima;
    }

    public void LevarDano(int danoParaReceber)
    {
        if (jogadorVivo)
        {
            vidaAtual -= danoParaReceber;

            if (vidaAtual <= 0)
            {
                jogadorVivo = false;
                Debug.Log("Game Over");
            }
        }
    }
}
