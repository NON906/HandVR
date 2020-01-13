using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class Menu : MonoBehaviour
{
    void Start()
    {
        XRSettings.enabled = false;
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void BackButton()
    {
        XRSettings.enabled = true;
        SceneManager.LoadScene("Main");
    }

    public void LicenseButton()
    {
        SceneManager.LoadScene("License");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            BackButton();
        }
    }
}
