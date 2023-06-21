using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NewGameController : MonoBehaviour
{
    public Dropdown levelSelector;
    private int level;

    public void SetDropdownIndex(int index)
    {
        levelSelector.value = index;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + (levelSelector.value + 1));
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
