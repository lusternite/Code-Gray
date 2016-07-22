using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    //public AudioSource BackGroundMusic;
    //public Text TimerText;

	// Use this for initialization
	void Start()
    {
        if (!instance)
        {
            instance = this;
            print("Game Manager Created");
        }
        else
        {
            Destroy(this.gameObject);
            print("Destroyed duplicate");
        }
        DontDestroyOnLoad(this.gameObject);  
        //AudioClip myAudioClip;
        //myAudioClip = (AudioClip)Resources.Load("");
        //BackGroundMusic.clip = myAudioClip;
        //BackGroundMusic.loop = true;
        //BackGroundMusic.Play();
	}

    // Returns the UIManager script that is attached to the canvas
    UIManager GetCanvas()
    {
        GameObject gameManager = GameObject.Find("Canvas");
        if (gameManager != null)
        {
            return gameManager.GetComponent<UIManager>();
        }
        return null;
    }

    void OnLevelWasLoaded()
    {
        // Sets the text to the current level number
        if (GetCanvas() != null) GetCanvas().SetLevelText("Level: " + Application.loadedLevel.ToString());
    }

    public void RestartLevel()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

	// Update is called once per frame
	void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            RestartLevel();
        }
        // Sets the timer to the time since level was loaded - rounded to 2 d.p
        if (GetCanvas() != null) GetCanvas().SetTimerText(Time.timeSinceLevelLoad.ToString("F2"));
	}

    public void GoToNextLevel()
    {
        if (Application.loadedLevel < Application.levelCount - 1)
        {
            Debug.Log(Application.loadedLevel);
            Debug.Log(Application.levelCount);
            Application.LoadLevel(Application.loadedLevel + 1);
        }
        else 
        {
            GoToMenu();
        }
    }

    public void GoToMenu()
    {
        Application.LoadLevel("MenuScene");
        print("Loading level");
        print(Application.loadedLevel);
    }

    public void ExitApplication()
    {
        Application.Quit();
    }
}
