using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleDoJogador : MonoBehaviour
{
    private Rigidbody2D oRigidbody2D;

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
        MovimentarJogador();
    }

    private void ReceberInputs()
    {
        inputDeMovimento = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    private void MovimentarJogador()
    {
        direcaoDoMovimento = inputDeMovimento.normalized;
        oRigidbody2D.velocity = direcaoDoMovimento * velocidadeDoJogador;
    }
}
