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

    [Header("Limites da Movimentação")]
    [SerializeField] private float xMinimo;
    [SerializeField] private float xMaximo;
    [SerializeField] private float yMinimo;
    [SerializeField] private float yMaximo;

    [Header("Controle do Ataque")]
    [SerializeField] private float tempoMaximoEntreAtaques;
    private float tempoAtualEntreAtaques;
    private bool podeAtacar;

    [Header("Controle do Dano")]
    [SerializeField] private float tempoMaximoDoDano;
    private float tempoAtualDoDano;
    private bool levouDano;

    private void Start()
    {
        oRigidbody2D = GetComponent<Rigidbody2D>();
        oAnimator = GetComponent<Animator>();

        tempoAtualDoDano = tempoMaximoDoDano;
    }

    private void Update()
    {
        if (GetComponent<VidaDoJogador>().jogadorVivo)
        {
            RodarCronometroDosAtaques();

            if (!levouDano)
            {
                ReceberInputs();
                RodarAnimacoesEAtaques();
                EspelharJogador();
                MovimentarJogador();
            }
            else
            {
                RodarCronometroDoDano();
            }
        }
        else
        {
            RodarAnimacaoDeDerrota();
        }
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

    private void RodarCronometroDoDano()
    {
        // Controla o congelar do Jogador ao levar dano
        tempoAtualDoDano -= Time.deltaTime;
        RodarAnimacaoDeDano();

        if (tempoAtualDoDano <= 0)
        {
            levouDano = false;
            tempoAtualDoDano = tempoMaximoDoDano;
        }
    }

    private void ReceberInputs()
    {
        // Armazena a direção que o Jogador quer seguir
        inputDeMovimento = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        // Testar dano do Jogador
        /*if (Input.GetKeyDown(KeyCode.L))
        {
            RodarAnimacaoDeDano();
        }*/
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
            SoundManager.instance.impactoSoco.Play();
        }

        if (Input.GetKeyDown(KeyCode.K) && podeAtacar)
        {
            oAnimator.SetTrigger("chutando");
            podeAtacar = false;
            SoundManager.instance.impactoChute.Play();
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

        // Limita a movimentação horizontal do Jogador
        oRigidbody2D.position = new Vector2(Mathf.Clamp(oRigidbody2D.position.x, xMinimo, xMaximo), oRigidbody2D.position.y);

        // Limita a movimentação vertical do Jogador
        oRigidbody2D.position = new Vector2(oRigidbody2D.position.x, Mathf.Clamp(oRigidbody2D.position.y, yMinimo, yMaximo));
    }

    public void RodarAnimacaoDeDano()
    {
        // Roda a animação de dano e zera a velocidade do Jogador
        oAnimator.SetTrigger("levando-dano");
        levouDano = true;

        oRigidbody2D.velocity = Vector2.zero;
    }

    public void RodarAnimacaoDeDerrota()
    {
        oAnimator.Play("jogador-derrotado");
    }
}
