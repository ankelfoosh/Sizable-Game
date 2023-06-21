using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    private bool active = false;

    public GameObject mainMenu;
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
            restart.SetActive(true);

            if (Input.GetKeyDown("escape"))
            {
                playerMove.canMove = true;
                active = false;
            }
        }
        else if (!active)
        {
            mainMenu.SetActive(false);
            restart.SetActive(false);

            if (Input.GetKeyDown("escape"))
            {
                active = true;
            }
        }
    }

    public void MenuButtonClick()
    {
        SceneManager.LoadScene(menuScene);
    }

    public void RestartButtonClick()
    {
        SceneManager.LoadScene(restartScene);
    }

    public void CloseMenu()
    {
        active = false;
    }
}
