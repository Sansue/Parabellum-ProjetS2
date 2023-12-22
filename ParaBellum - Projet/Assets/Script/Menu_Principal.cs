using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Principal : MonoBehaviour
{

    public void BouttonQuitter()
    {
        Application.Quit();
        Debug.Log("Jeu fermé");
    }

    public void Jouer()
    {
        SceneManager.LoadScene("SampleScene 1");
    }
}

