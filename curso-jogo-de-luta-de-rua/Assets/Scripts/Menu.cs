using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private string nomeDaPrimeiraFase;

    private void Start()
    {
        SoundManager.instance.TocarMusicaDeFundo();
    }

    public void IniciarJogo()
    {
        SceneManager.LoadScene(nomeDaPrimeiraFase);
    }

    public void SairDoJogo()
    {
        //Debug.Log("Saiu do Jogo");
        Application.Quit();
    }
}
