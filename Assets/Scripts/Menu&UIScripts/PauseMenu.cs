using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour
{
    private static PauseMenu instance;
    public GameObject ResumeButtonReference;
    public GameObject ToggleSoundButtonReference;
    public GameObject MenuButtonReference;
    int CurrentlySelectedOption = 0;

    // Use this for initialization
    void Start ()
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
        this.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S) || Input.GetAxis("Vertical") < 0.0f)
        {
            CurrentlySelectedOption += 1;
            CurrentlySelectedOption = CurrentlySelectedOption % 3;
        }

        else if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W) || Input.GetAxis("Vertical") > 0.0f)
        {
            CurrentlySelectedOption -= 1;
            if (CurrentlySelectedOption < 0)
            {
                CurrentlySelectedOption = 2;
            }
        }
        else if (Input.GetKeyUp(KeyCode.Space) || Input.GetAxis("Vertical") > 0.0f)
        {
            CurrentlySelectedOption -= 1;
            if (CurrentlySelectedOption < 0)
            {
                CurrentlySelectedOption = 2;
            }
        }
    }

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void SetState(bool state)
    {
        Debug.Log(state);
        this.gameObject.SetActive(state);
    }


}
