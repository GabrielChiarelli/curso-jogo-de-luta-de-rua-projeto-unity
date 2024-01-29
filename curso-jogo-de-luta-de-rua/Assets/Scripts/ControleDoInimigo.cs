using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleDoInimigo : MonoBehaviour
{
    [Header("Referências Gerais")]
    private Rigidbody2D oRigidbody2D;
    private Animator oAnimator;
    private GameObject oJogador;

    [Header("Movimento do Inimigo")]
    [SerializeField] private float velocidadeDoInimigo;
    private Vector2 direcaoDoMovimento;

    [Header("Controle do Ataque")]
    [SerializeField] private float tempoMaximoEntreAtaques;
    private float tempoAtualEntreAtaques;
    private bool podeAtacar;

    [SerializeField] private float distanciaParaAtacar;
    [SerializeField] private int quantidadeDeAtaquesDoInimigo;
    private int ataqueAtualDoInimigo;

    private void Start()
    {
        oRigidbody2D = GetComponent<Rigidbody2D>();
        oAnimator = GetComponent<Animator>();
        oJogador = FindObjectOfType<ControleDoJogador>().gameObject;
    }

    private void Update()
    {
        if (GetComponent<VidaDoInimigo>().inimigoVivo)
        {
            RodarCronometroDosAtaques();
            EspelharInimigo();
            SeguirJogador();
        }
        else
        {
            RodarAnimacaoDeDerrota();
        }
    }

    private void RodarCronometroDosAtaques()
    {
        // Limita a quantidade de ataques consecutivos que o Inimigo pode realizar
        tempoAtualEntreAtaques -= Time.deltaTime;
        if (tempoAtualEntreAtaques <= 0)
        {
            podeAtacar = true;
            tempoAtualEntreAtaques = tempoMaximoEntreAtaques;
        }
    }

    private void EspelharInimigo()
    {
        // Faz o Inimigo olhar na direção do Jogador (Esquerda / Direita)
        if (oJogador.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (oJogador.transform.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    private void SeguirJogador()
    {
        // Armazena a posição do Jogador e se movimenta a ele
        if (Vector2.Distance(transform.position, oJogador.transform.position) > distanciaParaAtacar)
        {
            direcaoDoMovimento = (oJogador.transform.position - transform.position).normalized;
            oRigidbody2D.velocity = direcaoDoMovimento * velocidadeDoInimigo;

            oAnimator.SetTrigger("andando");
        }
        // Deixa de se movimentar e ataca o Jogador
        else
        {
            oRigidbody2D.velocity = Vector2.zero;
            oAnimator.SetTrigger("parado");

            SortearAtaque();
        }
    }

    private void SortearAtaque()
    {
        // Sorteia um dos ataques disponíveis e inicia esse ataque
        ataqueAtualDoInimigo = Random.Range(0, quantidadeDeAtaquesDoInimigo);

        if (podeAtacar)
            IniciarAtaque();
    }

    private void IniciarAtaque()
    {
        // Muda o seu ataque para o ataque sorteado
        if (ataqueAtualDoInimigo == 0)
        {
            oAnimator.SetTrigger("socando-fraco");
        }
        else if (ataqueAtualDoInimigo == 1)
        {
            oAnimator.SetTrigger("socando-forte");
        }

        podeAtacar = false;
    }

    public void RodarAnimacaoDeDano()
    {
        oAnimator.SetTrigger("levando-dano");
    }

    public void RodarAnimacaoDeDerrota()
    {
        oAnimator.Play("inimigo-derrotado");
        oRigidbody2D.velocity = Vector2.zero;
    }
}
