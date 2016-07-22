using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    private static UIManager instance;
    public Text timer;
    public Text level;
    public Text memories;

    void Start()
    {
        if (!instance)
        {
            instance = this;
            print("UI Manager Script");
        }
        else
        {
            Destroy(this.gameObject);
            print("Destroyed duplicate");
        }
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetTimerText(string text)
    {
        timer.text = text;
    }

    public void SetLevelText(string text)
    {
        level.text = text;
    }

    public void SetMemoriesText(string text)
    {
        memories.text = text;
    }
}
