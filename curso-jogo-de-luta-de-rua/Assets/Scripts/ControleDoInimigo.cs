using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleDoInimigo : MonoBehaviour
{
    private Rigidbody2D oRigidbody2D;
    private GameObject oJogador;

    [SerializeField] private float velocidadeDoInimigo;
    private Vector2 direcaoDoMovimento;

    private void Start()
    {
        oRigidbody2D = GetComponent<Rigidbody2D>();
        oJogador = FindObjectOfType<ControleDoJogador>().gameObject;
    }

    private void Update()
    {
        SeguirJogador();
    }

    private void SeguirJogador()
    {
        direcaoDoMovimento = (oJogador.transform.position - transform.position).normalized;
        oRigidbody2D.velocity = direcaoDoMovimento * velocidadeDoInimigo;
    }
}
