using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.Universal;

public class NewGameController : MonoBehaviour
{
    public SaveData saveData;

    public Dropdown levelSelector;
    private int level;

    public GameObject title;
    public GameObject ball;
    private bool optionsOpened;

    // Ball Color editing

    public GameObject optionsMenu;
    public GameObject titleMenu;

    public Light2D ballLight;

    public string redHex;
    public string orangeHex;
    public string yellowHex;
    public string limeHex;
    public string greenHex;
    public string lightBlueHex;
    public string blueHex;
    public string darkBlueHex;
    public string purpleHex;
    public string magentaHex;
    public string pinkHex;
    public string whiteHex;

    public string currentHex;

    public Color curHex;

    public void SetDropdownIndex(int index)
    {
        levelSelector.value = index;

        ColorUtility.TryParseHtmlString(currentHex, out curHex);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + (levelSelector.value + 1));
    }

    public void Options()
    {
        titleMenu.SetActive(false);
        optionsMenu.SetActive(true);
        title.SetActive(false);
        ball.SetActive(true);
        optionsOpened = true;
    }

    public void ExitOptions()
    {
        titleMenu.SetActive(true);
        optionsMenu.SetActive(false);
        title.SetActive(true);
        ball.SetActive(false);
        optionsOpened = false;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    // Ball customization (BIG)

    public void Red()
    {
        currentHex = redHex;
        SetGlobalLightColor(currentHex);
        saveData.currentColor = 0;
    }

    public void Orange()
    {
        currentHex = orangeHex;
        SetGlobalLightColor(currentHex);
        saveData.currentColor = 1;
    }

    public void Yellow()
    {
        currentHex = yellowHex;
        SetGlobalLightColor(currentHex);
        saveData.currentColor = 2;
    }

    public void Lime()
    {
        currentHex = limeHex;
        SetGlobalLightColor(currentHex);
        saveData.currentColor = 3;
    }

    public void Green()
    {
        currentHex = greenHex;
        SetGlobalLightColor(currentHex);
        saveData.currentColor = 4;
    }

    public void LightBlue()
    {
        currentHex = lightBlueHex;
        SetGlobalLightColor(currentHex);
        saveData.currentColor = 5;
    }

    public void Blue()
    {
        currentHex = blueHex;
        SetGlobalLightColor(currentHex);
        saveData.currentColor = 6;
    }

    public void DarkBlue()
    {
        currentHex = darkBlueHex;
        SetGlobalLightColor(currentHex);
        saveData.currentColor = 7;
    }

    public void Purple()
    {
        currentHex = purpleHex;
        SetGlobalLightColor(currentHex);
        saveData.currentColor = 8;
    }

    public void Magenta()
    {
        currentHex = magentaHex;
        SetGlobalLightColor(currentHex);
        saveData.currentColor = 9;
    }

    public void Pink()
    {
        currentHex = pinkHex;
        SetGlobalLightColor(currentHex);
        saveData.currentColor = 10;
    }

    public void White()
    {
        currentHex = whiteHex;
        SetGlobalLightColor(currentHex);
        saveData.currentColor = 11;
    }

    private void SetGlobalLightColor(string hex)
    {
        if (ColorUtility.TryParseHtmlString(hex, out curHex))
        {
            ballLight.color = curHex;
        }
        else
        {
            Debug.LogError("Invaled Hex Color");
        }
    }
}
