using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraQueSegue : MonoBehaviour
{
    public static CameraQueSegue instance;

    [Header("Referências ao Jogador")]
    private GameObject oJogador;
    private Vector3 posicaoDoJogador;

    [Header("Limites da Movimentação")]
    [SerializeField] private float xMinimo;
    [SerializeField] private float xMaximo;

    [Header("Quando na Área de Luta")]
    [SerializeField] private float velocidadeParaVoltarAoJogador;
    private bool podeSeguirJogador;
    private bool voltandoParaJogador;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        oJogador = FindObjectOfType<ControleDoJogador>().gameObject;

        velocidadeParaVoltarAoJogador = 5f;
        podeSeguirJogador = true;
        voltandoParaJogador = false;
    }

    private void Update()
    {
        GetPosicaoDoJogador();

        if (podeSeguirJogador)
        {
            SeguirJogador();
        }

        if (voltandoParaJogador)
        {
            VoltarParaJogador();
        }
    }

    private void GetPosicaoDoJogador()
    {
        posicaoDoJogador = oJogador.transform.position;
    }

    private void SeguirJogador()
    {
        // Deixa sua posição X igual à do Jogador e impede que saia da tela
        transform.position = new Vector3(posicaoDoJogador.x, transform.position.y, transform.position.z);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, xMinimo, xMaximo), transform.position.y, transform.position.z);
    }

    public void TravarCamera()
    {
        podeSeguirJogador = false;
        FindObjectOfType<ControleDoJogador>().AtualizarLimiteXQuandoCameraTravada();
    }

    public void DestravarCamera()
    {
        voltandoParaJogador = true;
        StartCoroutine(VoltarASeguirJogadorCoroutine());
    }

    public void VoltarParaJogador()
    {
        Vector3 posicaoCentralizada = new Vector3(posicaoDoJogador.x, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, posicaoCentralizada, velocidadeParaVoltarAoJogador * Time.deltaTime);
    }

    private IEnumerator VoltarASeguirJogadorCoroutine()
    {
        FindObjectOfType<ControleDoJogador>().AtualizarLimiteXQuandoCameraDestravada();
        yield return new WaitForSeconds(1f);
        podeSeguirJogador = true;
        voltandoParaJogador = false;
    }
}
