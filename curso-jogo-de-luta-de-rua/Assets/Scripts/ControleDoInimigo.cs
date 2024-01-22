using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleDoInimigo : MonoBehaviour
{
    [Header("Referências Gerais")]
    private Rigidbody2D oRigidbody2D;
    private GameObject oJogador;

    [Header("Movimento do Inimigo")]
    [SerializeField] private float velocidadeDoInimigo;
    private Vector2 direcaoDoMovimento;

    private void Start()
    {
        oRigidbody2D = GetComponent<Rigidbody2D>();
        oJogador = FindObjectOfType<ControleDoJogador>().gameObject;
    }

    private void Update()
    {
        EspelharInimigo();
        SeguirJogador();
    }

    private void EspelharInimigo()
    {
        // Faz o Inimigo olhar na fireção do Jogador (Esquerda / Direita)
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
        // Armazena a posição do Jogador e se movimenta à ela
        direcaoDoMovimento = (oJogador.transform.position - transform.position).normalized;
        oRigidbody2D.velocity = direcaoDoMovimento * velocidadeDoInimigo;
    }
}
