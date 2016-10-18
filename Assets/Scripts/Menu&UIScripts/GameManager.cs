﻿using UnityEngine;
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
    public AudioClip MenuMusic;
    public AudioClip LevelMusicEasy;
    public AudioClip LevelMusicMedium;
    public AudioClip LevelMusicHard;
    bool isPaused = false;
    bool soundOn = true;
    int highestlevel;
    GameObject[] levelSelectDoors;

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
        //GetCanvas().gameObject.SetActive(false);
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
        if (Application.loadedLevel == 1)
        {
            int levelsUnlocked;
            StreamReader theReader = new StreamReader(Application.dataPath + "\\TextFiles\\LevelUnlocked.txt");
            using (theReader)
            {
                levelsUnlocked = int.Parse(theReader.ReadLine());
                theReader.Close();
            }

            for (int i = 2; i <= levelsUnlocked; ++i)
            {
                GameObject door = GameObject.Find("Door " + i.ToString());
                if (door == null)
                {
                    Debug.Log("nope");
                }
                door.GetComponent<LevelDoorScript>().Unlock();
            }
        }

        if (Application.loadedLevel > highestlevel)
        {
            highestlevel = Application.loadedLevel;
            var sr = File.CreateText(Application.dataPath + "\\TextFiles\\LevelUnlocked.txt");
            sr.WriteLine(highestlevel);
            sr.Close();
        }

        if (Application.loadedLevelName == "LevelSelect" || Application.loadedLevelName == "MainMenu")
        {
            UICanvas.gameObject.SetActive(false);
        }
        else
        {
            UICanvas.gameObject.SetActive(true);
        }

        GameObject player = GameObject.Find("Player");
        if (player != null)
        {
            UICanvas.GetComponent<UIManager>().SetMemories(player.GetComponent<PlayerBehaviour>().GetMaxClones());
        }

        //Changing music based on level
        if (Application.loadedLevel < 2)
        {
            if (BackGroundMusic.clip != MenuMusic)
            {
                BackGroundMusic.Stop();
                BackGroundMusic.clip = MenuMusic;
                BackGroundMusic.Play();
            }
        }
        else if (Application.loadedLevel < 9)
        {
            if (BackGroundMusic.clip != LevelMusicEasy)
            {
                BackGroundMusic.Stop();
                BackGroundMusic.clip = LevelMusicEasy;
                BackGroundMusic.Play();

            }
        }
        else if (Application.loadedLevel < 16)
        {
            if (BackGroundMusic.clip != LevelMusicMedium)
            {
                BackGroundMusic.Stop();
                BackGroundMusic.clip = LevelMusicMedium;
                BackGroundMusic.Play();

            }
        }
        else if (BackGroundMusic.clip != LevelMusicHard)
        {
            BackGroundMusic.Stop();
            BackGroundMusic.clip = LevelMusicHard;
            BackGroundMusic.Play();
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

        // Sets the timer to the time since level was loaded - rounded to 2 d.p
    }

    public void TogglePause()
    {
        if (Application.loadedLevelName != "LevelSelect" && Application.loadedLevelName != "MainMenu")
        {
            isPaused = !isPaused;
            Time.timeScale = isPaused ? 0 : 1;
            PauseMenuCanvas.SetActive(isPaused);
            PauseMenuCanvas.GetComponent<PauseMenu>().Reset();
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
