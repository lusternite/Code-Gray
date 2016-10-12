using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using System.Text;

public class GameManager : MonoBehaviour
{
    public GameObject PauseMenuCanvas;
    public GameObject UICanvas;
    private static GameManager instance;
    public AudioSource BackGroundMusic;
    bool isPaused = false;
    bool soundOn = true;
    int highestlevel;

    void Start()
    {
        StreamReader theReader = new StreamReader(Application.dataPath + "\\TextFiles\\LevelUnlocked.txt");
        using (theReader)
        {
            highestlevel = int.Parse(theReader.ReadLine());
            theReader.Close();
        }

        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        BackGroundMusic.loop = true;
        BackGroundMusic.enabled = true;
        BackGroundMusic.Play();

        DontDestroyOnLoad(this.gameObject);
        GetCanvas().gameObject.SetActive(false);
    }

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void ToggleSound()
    {
        soundOn = !soundOn;
        if (soundOn)
        {
            BackGroundMusic.UnPause();
        }
        else
        {
            BackGroundMusic.Pause();
        }
    }

    // Returns the UIManager script that is attached to the canvas
    UIManager GetCanvas()
    {
        GameObject UICanvas = GameObject.Find("Canvas");
        if (UICanvas != null)
        {
            return UICanvas.GetComponent<UIManager>();
        }
        return null;
    }

    void OnLevelWasLoaded()
    {
        if (Application.loadedLevel > highestlevel)
        {
            highestlevel = Application.loadedLevel;
            var sr = File.CreateText(Application.dataPath + "\\TextFiles\\LevelUnlocked.txt");
            sr.WriteLine(highestlevel);
            sr.Close();
        }

        UICanvas.gameObject.SetActive(Application.loadedLevelName == "LevelSelect" ? false : true);

        // Sets the text to the current level number
        if (GetCanvas() != null)
        {
            GetCanvas().SetLevelText("Level: " + (Application.loadedLevel - 1).ToString());
        }
    }

    public void RestartLevel()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

	// Update is called once per frame
	void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartLevel();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
        else if (Input.GetKeyDown(KeyCode.N))
        {
            GoToNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.M))
        {
            Application.LoadLevel("MenuScene");
        }
        // Sets the timer to the time since level was loaded - rounded to 2 d.p
	}

    public void TogglePause()
    {
        if (Application.loadedLevelName != "LevelSelect" && Application.loadedLevelName != "MainMenu")
        {
            isPaused = !isPaused;
            Time.timeScale = isPaused ? 0 : 1;
            PauseMenuCanvas.SetActive(isPaused);
        }
    }

    public void GoToNextLevel()
    {
        if (Application.loadedLevel < Application.levelCount - 1)
        {
            //Debug.Log(Application.loadedLevel);
            //Debug.Log(Application.levelCount);
            Application.LoadLevel(Application.loadedLevel + 1);
        }
        else 
        {
            GoToMenu();
        }
    }

    public void GoToMenu()
    {
        Application.LoadLevel("LevelSelect");
        print("Loading level");
        print(Application.loadedLevel);
    }

    public void ExitApplication()
    {
        Application.Quit();
    }

    public int GetHighestLevel()
    {
        return highestlevel;
    }
}
