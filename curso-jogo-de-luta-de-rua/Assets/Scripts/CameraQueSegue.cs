using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraQueSegue : MonoBehaviour
{
    [Header("Referências ao Jogador")]
    private GameObject oJogador;
    private Vector3 posicaoDoJogador;

    private void Start()
    {
        oJogador = FindObjectOfType<ControleDoJogador>().gameObject;
    }

    private void Update()
    {
        SeguirJogador();
    }

    private void SeguirJogador()
    {
        // Armazena a posição do Jogador
        posicaoDoJogador = oJogador.transform.position;

        // Deixa suas posição X igual à do Jogador
        transform.position = new Vector3(posicaoDoJogador.x, transform.position.y, transform.position.z);
    }
}
