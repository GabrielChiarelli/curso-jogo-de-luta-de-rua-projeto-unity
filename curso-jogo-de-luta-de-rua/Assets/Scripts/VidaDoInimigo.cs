using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaDoInimigo : MonoBehaviour
{
    [Header("Referências Gerais")]
    private AreaDeLuta minhaAreaDeLuta;

    [Header("Verificações")]
    public bool inimigoVivo;

    [Header("Parâmetros")]
    [SerializeField] private string nomeDoInimigo;

    [Header("Controle da Vida")]
    [SerializeField] private int vidaMaxima;
    private int vidaAtual;
    [SerializeField] private float tempoParaSumir;

    [Header("Drop ao Morrer")]
    [SerializeField] private int chanceDeDroparComida;
    [SerializeField] private GameObject[] comidasParaDropar;

    private void Start()
    {
        // Configura a vida do Inimigo
        inimigoVivo = true;
        vidaAtual = vidaMaxima;
    }

    public void ReceberMinhaAreaDeLuta(AreaDeLuta novaAreaDeLuta)
    {
        minhaAreaDeLuta = novaAreaDeLuta;
    }

    public void LevarDano(int danoParaReceber)
    {
        // Aplica o dano no Inimigo
        if (inimigoVivo)
        {
            vidaAtual -= danoParaReceber;
            GetComponent<ControleDoInimigo>().RodarAnimacaoDeDano();
            UIManager.instance.AtualizarBarraDeVidaDoInimigoAtual(vidaMaxima, vidaAtual, nomeDoInimigo);
            SoundManager.instance.inimigoLevandoDano.Play();

            if (vidaAtual <= 0)
            {
                inimigoVivo = false;
                GetComponent<ControleDoInimigo>().RodarAnimacaoDeDerrota();
                SpawnarComida();
                UIManager.instance.DesativarPainelDoInimigo();

                minhaAreaDeLuta.IncrementarContagemDeInimigosDerrotados();

                Destroy(this.gameObject, tempoParaSumir);
            }
        }
    }

    private void SpawnarComida()
    {
        // Sorteia a chance de o Inimigo dropar a comida
        int numeroAleatorio = Random.Range(0, 101);
        //Debug.Log(numeroAleatorio);

        // Roda se a chance estiver dentro do limite
        if (numeroAleatorio <= chanceDeDroparComida)
        {
            // Sorteiam uma comida e criam uma cópia dela na cena
            GameObject comidaEscolhida = comidasParaDropar[Random.Range(0, comidasParaDropar.Length)];
            Instantiate(comidaEscolhida, transform.position, transform.rotation);
        }
    }
}
