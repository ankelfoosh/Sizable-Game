using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public bool active = false;

    public GameObject mainMenu;
    public GameObject nextLevel;
    public GameObject restart;

    public Player_Move playerMove;

    [SerializeField] private string menuScene = "Main Menu";
    [SerializeField] private string restartScene = "LevelName";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            playerMove.canMove = false;
            playerMove.rb.gravityScale = 0f;
            playerMove.rb.velocity = new Vector2(0f, 0f);
            mainMenu.SetActive(true);
            nextLevel.SetActive(true);
            restart.SetActive(true);
        }
        else if (!active)
        {
            mainMenu.SetActive(false);
            restart.SetActive(false);
        }
    }

    public void NextLevelButtonClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void MenuButtonClick()
    {
        SceneManager.LoadScene(menuScene);
    }

    public void RestartButtonClick()
    {
        SceneManager.LoadScene(restartScene);
    }
}
