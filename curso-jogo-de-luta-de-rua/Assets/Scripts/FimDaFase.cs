using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FimDaFase : MonoBehaviour
{
    [SerializeField] private string nomeDaProximaFase;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<ControleDoJogador>() != null)
        {
            ControleDoInimigo[] inimigosNaFase = FindObjectsOfType<ControleDoInimigo>();
            if (inimigosNaFase.Length == 0)
            {
                SceneManager.LoadScene(nomeDaProximaFase);
            }
        }
    }
}
