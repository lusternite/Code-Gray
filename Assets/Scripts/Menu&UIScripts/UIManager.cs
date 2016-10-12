using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    private static UIManager instance;
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

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }

    GameManager GetGameManager()
    {
        GameObject gameManager = GameObject.Find("GameManager");
        if (gameManager != null)
        {
            return gameManager.GetComponent<GameManager>();
        }
        return null;
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