using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [Header("Efeitos Sonoros")]
    public AudioSource impactoChute;
    public AudioSource impactoSoco;
    public AudioSource pegarComida;
    public AudioSource inimigoLevandoDano;
    public AudioSource jogadorLevandoDano;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        
    }
}
