using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image memoryImage;
    private static UIManager instance;
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

    public void SetMemories(int memories)
    {
        for (int i = 0; i < memories; ++i)
        {
            GameObject.Instantiate(memoryImage, new Vector3((i * 1), 0, 0), new Quaternion());
        }
    }
}