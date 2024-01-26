using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleDoJogador : MonoBehaviour
{
    [Header("Referências Gerais")]
    private Rigidbody2D oRigidbody2D;
    private Animator oAnimator;

    [Header("Movimento do Jogador")]
    [SerializeField] private float velocidadeDoJogador;
    private Vector2 inputDeMovimento;
    private Vector2 direcaoDoMovimento;

    [Header("Controle do Ataque")]
    [SerializeField] private float tempoMaximoEntreAtaques;
    private float tempoAtualEntreAtaques;
    private bool podeAtacar;

    private void Start()
    {
        oRigidbody2D = GetComponent<Rigidbody2D>();
        oAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        RodarCronometroDosAtaques();
        ReceberInputs();
        RodarAnimacoesEAtaques();
        EspelharJogador();
        MovimentarJogador();
    }

    private void RodarCronometroDosAtaques()
    {
        // Controla o tempo entre um ataque e outro
        tempoAtualEntreAtaques -= Time.deltaTime;
        if (tempoAtualEntreAtaques <= 0)
        {
            podeAtacar = true;
            tempoAtualEntreAtaques = tempoMaximoEntreAtaques;
        }
    }

    private void ReceberInputs()
    {
        // Armazena a direção que o Jogador quer seguir
        inputDeMovimento = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        // Testar dano do Jogador
        if (Input.GetKeyDown(KeyCode.L))
        {
            RodarAnimacaoDeDano();
        }
    }

    private void RodarAnimacoesEAtaques()
    {
        // Rodam as animações de Movimento (Parado e Andando)
        if (inputDeMovimento.magnitude == 0)
        {
            oAnimator.SetTrigger("parado");
        }
        else if (inputDeMovimento.magnitude != 0)
        {
            oAnimator.SetTrigger("andando");
        }

        // Rodam as animações de Ataque (Soco e Chute)
        if (Input.GetKeyDown(KeyCode.J) && podeAtacar)
        {
            oAnimator.SetTrigger("socando");
            podeAtacar = false;
        }

        if (Input.GetKeyDown(KeyCode.K) && podeAtacar)
        {
            oAnimator.SetTrigger("chutando");
            podeAtacar = false;
        }
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
        // Movimenta o Jogador com base na sua direção
        direcaoDoMovimento = inputDeMovimento.normalized;
        oRigidbody2D.velocity = direcaoDoMovimento * velocidadeDoJogador;
    }

    public void RodarAnimacaoDeDano()
    {
        oAnimator.SetTrigger("levando-dano");
        Debug.Log("Jogador levou dano");
    }
}
