using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] private GameObject painelDoInimigo;
    [SerializeField] private Slider barraDeVidaDoInimigoAtual;
    [SerializeField] private TMP_Text textoDoNomeDoInimigoAtual;
    
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        DesativarPainelDoInimigo();
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
}
