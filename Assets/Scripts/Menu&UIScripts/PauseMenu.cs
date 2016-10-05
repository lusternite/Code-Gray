using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour
{
    private static PauseMenu instance;

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
