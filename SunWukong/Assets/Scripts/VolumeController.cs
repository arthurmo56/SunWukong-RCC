using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeController : MonoBehaviour
{

    //Variável geral de audio
    private AudioSource audioSource;

    //Variável do controle de música
    private float musicVolume = 1f;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        audioSource.volume = musicVolume;
    }

    //metódo que é chamado pelo slider
    public void SetVolume(float vol)
    {
        musicVolume = vol;
    }
}
