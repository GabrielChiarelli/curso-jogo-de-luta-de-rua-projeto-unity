using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FimDaFase : MonoBehaviour
{
    [SerializeField] private float tempoParaEscurecer;
    [SerializeField] private float tempoParaCarregarNovaFase;

    [SerializeField] private string nomeDaProximaFase;

    private IEnumerator EscurecerTela()
    {
        // Espera por X segundos e escurece a tela
        yield return new WaitForSeconds(tempoParaEscurecer);
        UIManager.instance.EscurecerImagemDeTransicao();

        // Espera por X segundos e carrega a fase seguinte
        yield return new WaitForSeconds(tempoParaCarregarNovaFase);
        SceneManager.LoadScene(nomeDaProximaFase);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Se o Jogador colidir, verifica se derrotou todos os inimigos; se os tiver derrotado, carregará a próxima fase
        if (other.gameObject.GetComponent<ControleDoJogador>() != null)
        {
            ControleDoInimigo[] inimigosNaFase = FindObjectsOfType<ControleDoInimigo>();
            if (inimigosNaFase.Length == 0)
            {
                StartCoroutine(EscurecerTela());
            }
        }
    }
}
