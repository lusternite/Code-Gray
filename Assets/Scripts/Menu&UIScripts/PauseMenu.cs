using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    private static PauseMenu instance;
    public GameObject ResumeButtonReference;
    public GameObject ToggleSoundButtonReference;
    public GameObject MenuButtonReference;
    public int CurrentlySelectedOption = 0;
    public Sprite resumeOn;
    public Sprite resumeOff;
    public Sprite toggleOn;
    public Sprite toggleOff;
    public Sprite menuOn;
    public Sprite menuOff;

    GameObject[] buttons;
    Sprite[] sprites;

    // Use this for initialization
    void Start ()
    {
        ResumeButtonReference.GetComponent<Image>().sprite = resumeOn;
        ToggleSoundButtonReference.GetComponent<Image>().sprite = toggleOff;
        MenuButtonReference.GetComponent<Image>().sprite = menuOff;

        buttons = new GameObject[3];
        buttons[0] = ResumeButtonReference;
        buttons[1] = ToggleSoundButtonReference;
        buttons[2] = MenuButtonReference;

        sprites = new Sprite[6];
        sprites[0] = resumeOn;
        sprites[1] = resumeOff;
        sprites[2] = toggleOn;
        sprites[3] = toggleOff;
        sprites[4] = menuOn;
        sprites[5] = menuOff;

        Debug.Log(buttons[0]);

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
        this.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S) || Input.GetAxis("Vertical") < -0.1f)
        {
            buttons[CurrentlySelectedOption].GetComponent<Image>().sprite = sprites[CurrentlySelectedOption * 2 + 1];
            CurrentlySelectedOption += 1;
            CurrentlySelectedOption = CurrentlySelectedOption % 3;
            buttons[CurrentlySelectedOption].GetComponent<Image>().sprite = sprites[CurrentlySelectedOption * 2];
        }

        else if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W) || Input.GetAxis("Vertical") > 0.1f)
        {
            buttons[CurrentlySelectedOption].GetComponent<Image>().sprite = sprites[CurrentlySelectedOption * 2 + 1];
            CurrentlySelectedOption -= 1;
            if (CurrentlySelectedOption < 0)
            {
                CurrentlySelectedOption = 2;
            }
            buttons[CurrentlySelectedOption].GetComponent<Image>().sprite = sprites[CurrentlySelectedOption * 2];
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            GameManager manager = GameObject.Find("GameManager").GetComponent<GameManager>();
            if (CurrentlySelectedOption == 0)
            {
                manager.TogglePause();
            }
            if (CurrentlySelectedOption == 1)
            {
                manager.ToggleSound();
            }
            if (CurrentlySelectedOption == 2)
            {
                manager.TogglePause();
                manager.GoToMenu();
            }
        }
    }

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void Reset()
    {
        CurrentlySelectedOption = 0;
        ResumeButtonReference.GetComponent<Image>().sprite = resumeOn;
        ToggleSoundButtonReference.GetComponent<Image>().sprite = toggleOff;
        MenuButtonReference.GetComponent<Image>().sprite = menuOff;
    }
}
