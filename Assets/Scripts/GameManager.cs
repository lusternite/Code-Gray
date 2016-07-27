using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using System.Text;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    string[] LevelBestTimes;
    public AudioSource BackGroundMusic;

    void Start()
    {
        LevelBestTimes = new string[8];
        ReadFromFile();
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

        BackGroundMusic.loop = true;
        BackGroundMusic.Play();

        DontDestroyOnLoad(this.gameObject);
    }

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
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
        // Sets the text to the current level number
        if (GetCanvas() != null)
        {
            GetCanvas().SetLevelText("Level: " + Application.loadedLevel.ToString());
            if (LevelBestTimes[Application.loadedLevel - 1] == "0.0")
            {
                GetCanvas().SetBestTimeText("Best: --.--");
            }
            else
            {
                GetCanvas().SetBestTimeText("Best: " + float.Parse(LevelBestTimes[Application.loadedLevel - 1]).ToString("F2"));
            }
        }
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
        else if (Input.GetKeyDown(KeyCode.P))
        {
            GoToNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            Application.LoadLevel("MenuScene");
        }
        // Sets the timer to the time since level was loaded - rounded to 2 d.p
        
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
        Application.LoadLevel("MenuScene");
        print("Loading level");
        print(Application.loadedLevel);
    }

    public void ExitApplication()
    {
        Application.Quit();
    }

    public void UpdateTimes(float time)
    {
        Debug.Log("During");
        float prevBest = float.Parse(LevelBestTimes[Application.loadedLevel - 1]);
        Debug.Log(prevBest);
        Debug.Log(time);
        if (time < prevBest || prevBest == 0.0)
        {
            LevelBestTimes[Application.loadedLevel - 1] = time.ToString("F2");
        }
        WriteToFile();
    }

    void WriteToFile()
    {
        var sr = File.CreateText(Application.dataPath + "\\TextFiles\\BestTimes.txt");
        for (int i = 0; i < Application.levelCount - 1; ++i)
        {
            sr.WriteLine(LevelBestTimes[i]);
        }
        sr.Close();
    }

    void ReadFromFile()
    {
        string line;
        StreamReader theReader = new StreamReader(Application.dataPath + "\\TextFiles\\BestTimes.txt");
        using (theReader)
        {
            int i = 0;
            do
            {
                line = theReader.ReadLine();
                if (line != null)
                {
                    LevelBestTimes[i] = line;
                }
                i++;
            }
            while (line != null);

            theReader.Close();
        }
    }
}
