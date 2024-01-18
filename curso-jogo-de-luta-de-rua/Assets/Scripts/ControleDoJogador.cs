using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleDoJogador : MonoBehaviour
{
    [Header("Referências Gerais")]
    private Rigidbody2D oRigidbody2D;

    [Header("Movimento do Jogador")]
    [SerializeField] private float velocidadeDoJogador;
    private Vector2 inputDeMovimento;
    private Vector2 direcaoDoMovimento;

    private void Start()
    {
        oRigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        ReceberInputs();
        EspelharJogador();
        MovimentarJogador();
    }

    private void ReceberInputs()
    {
        // Armazena a direção que o Jogador quer seguir
        inputDeMovimento = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    private void EspelharJogador()
    {
        // Faz o Jogador olhar para a direção que está andando (direita / esquerda)
        if (inputDeMovimento.x == 1)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (inputDeMovimento.x == -1)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }

    private void MovimentarJogador()
    {
        // Movimentar o Jogador com base na sua direção
        direcaoDoMovimento = inputDeMovimento.normalized;
        oRigidbody2D.velocity = direcaoDoMovimento * velocidadeDoJogador;
    }
}
