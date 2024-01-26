using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaDoJogador : MonoBehaviour
{
    [Header("Verificações")]
    public bool jogadorVivo;

    [Header("Controle da Vida")]
    [SerializeField] private int vidaMaxima;
    private int vidaAtual;

    private void Start()
    {
        // Configura a vida do Jogador
        jogadorVivo = true;
        vidaAtual = vidaMaxima;
    }

    public void LevarDano(int danoParaReceber)
    {
        // Aplica o dano no Jogador
        if (jogadorVivo)
        {
            vidaAtual -= danoParaReceber;

            GetComponent<ControleDoJogador>().RodarAnimacaoDeDano();

            if (vidaAtual <= 0)
            {
                jogadorVivo = false;
                Debug.Log("Game Over");
            }
        }
    }
}
