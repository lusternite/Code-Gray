using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

    public bool _MenuState = true;

    public Sprite begin;
    public Sprite begin_off;
    public Sprite exit;
    public Sprite exit_off;

    public Image begin_button;
    public Image exit_button;

    // Update is called once per frame
    void Update()
    {
        GetInput();
    }

    void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            if (_MenuState == true) 
            {
                begin_button.sprite = begin_off;
                exit_button.sprite = exit;
                _MenuState = false;
                Debug.Log("false");
            }
            else
            {
                begin_button.GetComponent<Image>().sprite = begin;
                exit_button.GetComponent<Image>().sprite = exit_off;
                _MenuState = true;
                Debug.Log("true");
            }
            
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_MenuState == true) 
            {
                StartGame();
            }
            else
            {
                ExitGame();
            }
        }
    }

    void StartGame()
    {
        Application.LoadLevel("Level1");
    }

    void ExitGame()
    {
        Application.Quit();
    }
}
