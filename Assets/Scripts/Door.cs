using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

    public GameObject ButtonTriggerObject;
    public Sprite DoorClosed;
    public Sprite DoorOpen;
    Button ButtonTrigger;
    public bool Inverted;

	// Use this for initialization
	void Start () {
        ButtonTrigger = ButtonTriggerObject.GetComponent<Button>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Inverted)
        {
            if (ButtonTrigger.ButtonPressed)
            {
                GetComponent<BoxCollider2D>().enabled = true;
                GetComponent<SpriteRenderer>().enabled = true;
            }
            else
            {
                GetComponent<BoxCollider2D>().enabled = false;
                GetComponent<SpriteRenderer>().enabled = false;
            }
        }
        else
        {
            if (ButtonTrigger.ButtonPressed)
            {
                GetComponent<BoxCollider2D>().enabled = false;
                GetComponent<SpriteRenderer>().enabled = false;
            }
            else
            {
                GetComponent<BoxCollider2D>().enabled = true;
                GetComponent<SpriteRenderer>().enabled = true;
            }
        }
	}
}
