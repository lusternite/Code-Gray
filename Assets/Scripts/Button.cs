using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {

    public bool ButtonPressed;

    public Sprite ButtonOn;
    public Sprite ButtonOff;

    public AudioClip ButtonPressedSound;
    public AudioClip ButtonReleaseSound;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (ButtonPressed == true)
        {
            GetComponent<SpriteRenderer>().sprite = ButtonOn;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = ButtonOff;
        }
	}

    void OnTriggerStay2D()
    {
        ButtonPressed = true;
    }

    void OnTriggerEnter2D()
    {
        AudioSource.PlayClipAtPoint(ButtonPressedSound, transform.position);
    }

    void OnTriggerExit2D()
    {
        AudioSource.PlayClipAtPoint(ButtonReleaseSound, transform.position);
        ButtonPressed = false;
    }
}
