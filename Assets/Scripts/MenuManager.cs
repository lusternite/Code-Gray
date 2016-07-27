using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

    public bool _MenuState;
    Vector3 StartPosition;

    void Start()
    {
        _MenuState = true;
        StartPosition = transform.position;
        Debug.Log("same");
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        SetInput();
    }

    void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            if (_MenuState == true) 
            {
                _MenuState = false;
                Debug.Log("false");
            }
            else
            {
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

    void SetInput()
    {
        if (_MenuState == true)
        {
            transform.position = new Vector3(459, 282, 0);
        }
        else{
            transform.position = new Vector3(460, 224, 0);
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
