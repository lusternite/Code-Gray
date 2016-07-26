using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {

    public bool ButtonPressed;

    public Sprite ButtonOn;
    public Sprite ButtonOff;

    public AudioSource ButtonPressedSound;
    public AudioSource ButtonReleaseSound;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerStay2D()
    {
        ButtonPressed = true;
        
    }

    void OnTriggerEnter2D()
    {
       ButtonPressedSound.Play();
    }

    void OnTriggerExit2D()
    {
        ButtonReleaseSound.Play();
        ButtonPressed = false;

    }
}
