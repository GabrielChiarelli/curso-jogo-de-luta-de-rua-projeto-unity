using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaDeLuta : MonoBehaviour
{
    [Header("Verificações")]
    private bool podeVerificarJogador;
    private bool podeSpawnar;

    [Header("Cronômetro do Spawn")]
    [SerializeField] private float tempoMaximoEntreSpawns;
    private float tempoAtualEntreSpawns;

    [Header("Controle do Spawn")]
    [SerializeField] private Transform[] pontosDeSpawn;
    [SerializeField] private GameObject[] inimigosParaSpawnar;
    private int inimigosSpawnados;
    private int inimigoAtual;

    private void Start()
    {
        podeVerificarJogador = true;
        podeSpawnar = false;

        inimigosSpawnados = 0;
        inimigoAtual = 0;
    }

    private void Update()
    {
        if (podeSpawnar && inimigosSpawnados < inimigosParaSpawnar.Length)
        {
            RodarCronometroDoSpawn();
        }
    }

    private void RodarCronometroDoSpawn()
    {
        // Controla a quantidade de Inimigos spawnados por segundo

        tempoAtualEntreSpawns -= Time.deltaTime;
        if (tempoAtualEntreSpawns <= 0)
        {
            SpawnarInimigo();
            tempoAtualEntreSpawns = tempoMaximoEntreSpawns;
        }
    }

    private void SpawnarInimigo()
    {
        // Escolhe um novo local de spawn e um novo Inimigo
        Transform pontoAleatorio = pontosDeSpawn[Random.Range(0, pontosDeSpawn.Length)];
        GameObject novoInimigo = inimigosParaSpawnar[inimigoAtual];

        // Spawna o novo Inimigo no local escolhido anteriormente
        Instantiate(novoInimigo, pontoAleatorio.position, pontoAleatorio.rotation);
        inimigoAtual += 1;
        inimigosSpawnados++;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (podeVerificarJogador)
        {
            if (other.gameObject.GetComponent<ControleDoJogador>() != null)
            {
                podeSpawnar = true;
                podeVerificarJogador = false;
            }
        }
    }
}
