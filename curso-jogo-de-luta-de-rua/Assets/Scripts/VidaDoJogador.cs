using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaDoJogador : MonoBehaviour
{
    [Header("Verificações")]
    public bool jogadorVivo;

    [Header("Parâmetros")]
    [SerializeField] private float tempoParaGameOver;

    [Header("Controle da Vida")]
    [SerializeField] private int vidaMaxima;
    private int vidaAtual;

    private void Start()
    {
        // Configura a vida do Jogador
        jogadorVivo = true;
        vidaAtual = vidaMaxima;

        UIManager.instance.AtualizarBarraDeVidaDoJogador(vidaMaxima, vidaAtual);
    }

    public void GanharVida(int vidaParaGanhar)
    {
        // Se a vida atual somada com a vida para ganhar não superar o limite
        if (vidaAtual + vidaParaGanhar <= vidaMaxima)
        {
            vidaAtual += vidaParaGanhar;
        }
        
        // Se a vida atual somada com a vida para ganhar superar o limite
        else
        {
            vidaAtual = vidaMaxima;
        }

        // Atualiza o status da Barra de Vida do Jogador
        UIManager.instance.AtualizarBarraDeVidaDoJogador(vidaMaxima, vidaAtual);
    }

    public void LevarDano(int danoParaReceber)
    {
        // Aplica o dano no Jogador
        if (jogadorVivo)
        {
            vidaAtual -= danoParaReceber;

            GetComponent<ControleDoJogador>().RodarAnimacaoDeDano();
            UIManager.instance.AtualizarBarraDeVidaDoJogador(vidaMaxima, vidaAtual);
            SoundManager.instance.jogadorLevandoDano.Play();

            // Se o Jogador morrer
            if (vidaAtual <= 0)
            {
                jogadorVivo = false;
                GetComponent<ControleDoJogador>().RodarAnimacaoDeDerrota();
                StartCoroutine(AtivarGameOver());
            }
        }
    }

    private IEnumerator AtivarGameOver()
    {
        // Espera por X segundos e ativa o Painel de Game Over
        yield return new WaitForSeconds(tempoParaGameOver);
        UIManager.instance.AtivarPainelDeGameOver();
    }
}
