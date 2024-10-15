using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipalManager : MonoBehaviour
{
    //variaveis
    [SerializeField] private string nomeMapa;
    [SerializeField] private string nomeMenu;
    [SerializeField] private GameObject painelMenuInicial;
    [SerializeField] private GameObject painelOptions;
    [SerializeField] private GameObject painelAudio;
    [SerializeField] private GameObject painelControles;
    [SerializeField] private GameObject painelCreditos;

    public void Jogar()
    {
        SceneManager.LoadScene(nomeMapa);
    }

    public void Menu()
    {
        SceneManager.LoadScene(nomeMenu);
    }

    public void AbrirOptions()
    {
        painelMenuInicial.SetActive(false);
        painelOptions.SetActive(true);
    }

    public void FecharOptions()
    {
        painelOptions.SetActive(false);
        painelMenuInicial.SetActive(true);
    }

    public void AbrirAudio()
    {
        painelOptions.SetActive(false);
        painelAudio.SetActive(true);
    }

    public void FecharAudio()
    {
        painelOptions.SetActive(true);
        painelAudio.SetActive(false);
    }

    public void AbrirControles()
    {
        painelOptions.SetActive(false);
        painelControles.SetActive(true);
    }

    public void FecharControles()
    {
        painelOptions.SetActive(true);
        painelControles.SetActive(false);
    }

    public void AbrirCreditos()
    {
        painelMenuInicial.SetActive(false);
        painelCreditos.SetActive(true);
    }

    public void FecharCreditos()
    {
        painelMenuInicial.SetActive(true);
        painelCreditos.SetActive(false);
    }

    public void Quit()
    {

        Debug.Log("Sair do Jogo");

        //o metodo so funciona dps que o jogo ta todo compilado
        Application.Quit();
    }

}
