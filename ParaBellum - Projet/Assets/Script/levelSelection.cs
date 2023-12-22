using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelSelection : MonoBehaviour
{

    public GameObject levelSelectionWindow;
     private void Start()
    {
        // Désactiver la fenêtre de sélection de niveau au démarrage
        levelSelectionWindow.SetActive(false);
    }

    public void ButNiveau1()
    {
        SceneManager.LoadScene("SampleScene 1");
    }

    public void ButNiveau2()
    {
        SceneManager.LoadScene("Chateau");
    }

    public void ButNiveau3()
    {
        SceneManager.LoadScene("WW2");
    }
}
