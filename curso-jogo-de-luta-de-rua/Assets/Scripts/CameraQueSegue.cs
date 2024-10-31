using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraQueSegue : MonoBehaviour
{
    [Header("Referências ao Jogador")]
    private GameObject oJogador;
    private Vector3 posicaoDoJogador;

    [Header("Limites da Movimentação")]
    [SerializeField] private float xMinimo;
    [SerializeField] private float xMaximo;

    private bool podeSeguirJogador;

    private void Start()
    {
        oJogador = FindObjectOfType<ControleDoJogador>().gameObject;

        podeSeguirJogador = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            TravarCamera();
        }
        else if (Input.GetKeyDown(KeyCode.V))
        {
            DestravarCamera();
        }

        if (podeSeguirJogador)
        {
            SeguirJogador();
        }
    }

    private void SeguirJogador()
    {
        // Armazena a posição do Jogador
        posicaoDoJogador = oJogador.transform.position;

        // Deixa sua posição X igual à do Jogador e impede que saia da tela
        transform.position = new Vector3(posicaoDoJogador.x, transform.position.y, transform.position.z);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, xMinimo, xMaximo), transform.position.y, transform.position.z);
    }

    public void TravarCamera()
    {
        podeSeguirJogador = false;
    }

    public void DestravarCamera()
    {
        podeSeguirJogador = true;
    }
}
