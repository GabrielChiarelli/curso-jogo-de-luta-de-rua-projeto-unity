using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaDoInimigo : MonoBehaviour
{
    [Header("Verificações")]
    public bool inimigoVivo;

    [Header("Controle da Vida")]
    [SerializeField] private int vidaMaxima;
    private int vidaAtual;

    private void Start()
    {
        // Configura a vida do Inimigo
        inimigoVivo = true;
        vidaAtual = vidaMaxima;
    }

    public void LevarDano(int danoParaReceber)
    {
        // Aplica o dano no Inimigo
        if (inimigoVivo)
        {
            vidaAtual -= danoParaReceber;

            if (vidaAtual <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
