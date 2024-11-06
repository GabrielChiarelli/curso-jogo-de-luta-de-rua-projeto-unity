using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] private Animator oAnimatorDaTransicao;

    [Header("UI do Game Over")]
    [SerializeField] private GameObject painelDeGameOver;

    [Header("UI do Jogador")]
    [SerializeField] private Slider barraDeVidaDoJogador;

    [Header("UI do Inimigo")]
    [SerializeField] private GameObject painelDoInimigo;
    [SerializeField] private Slider barraDeVidaDoInimigoAtual;
    [SerializeField] private TMP_Text textoDoNomeDoInimigoAtual;

    [Header("UI da Área de Luta")]
    [SerializeField] private GameObject painelDaAreaDeLuta;
    [SerializeField] private float tempoParaDesativarPainelDaAreaDeLuta = 4.5f;
    
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        DesativarPainelDoInimigo();
        ClarearImagemDeTransicao();
        painelDaAreaDeLuta.SetActive(false);
    }

    private void ClarearImagemDeTransicao()
    {
        oAnimatorDaTransicao.Play("imagem-de-transicao-clareando");
    }

    public void EscurecerImagemDeTransicao()
    {
        oAnimatorDaTransicao.Play("imagem-de-transicao-escurecendo");
    }

    public void AtualizarBarraDeVidaDoJogador(int valorMaximo, int valorAtual)
    {
        barraDeVidaDoJogador.maxValue = valorMaximo;
        barraDeVidaDoJogador.value = valorAtual;
    }

    public void AtivarPainelDoInimigo()
    {
        painelDoInimigo.SetActive(true);
    }

    public void DesativarPainelDoInimigo()
    {
        painelDoInimigo.SetActive(false);
    }

    public void AtualizarBarraDeVidaDoInimigoAtual(int valorMaximo, int valorAtual, string nomeDoInimigo)
    {
        barraDeVidaDoInimigoAtual.maxValue = valorMaximo;
        barraDeVidaDoInimigoAtual.value = valorAtual;

        textoDoNomeDoInimigoAtual.text = nomeDoInimigo;

        AtivarPainelDoInimigo();
    }

    public void AtivarPainelDeGameOver()
    {
        painelDeGameOver.SetActive(true);
    }

    public void AtivarPainelDaAreaDeLuta()
    {
        painelDaAreaDeLuta.SetActive(true);
        StartCoroutine(DesativarPainelDaAreaDeLutaCoroutine());
    }

    private IEnumerator DesativarPainelDaAreaDeLutaCoroutine()
    {
        yield return new WaitForSeconds(tempoParaDesativarPainelDaAreaDeLuta);
        painelDaAreaDeLuta.SetActive(false);
    }
}
