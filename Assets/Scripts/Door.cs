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
                //GetComponent<SpriteRenderer>().enabled = true;
                GetComponent<SpriteRenderer>().sprite = DoorClosed;
            }
            else
            {
                GetComponent<BoxCollider2D>().enabled = false;
                //GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<SpriteRenderer>().sprite = DoorOpen;
            }
        }
        else
        {
            if (ButtonTrigger.ButtonPressed)
            {
                GetComponent<BoxCollider2D>().enabled = false;
                //GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<SpriteRenderer>().sprite = DoorOpen;
            }
            else
            {
                GetComponent<BoxCollider2D>().enabled = true;
                //GetComponent<SpriteRenderer>().enabled = true;
                GetComponent<SpriteRenderer>().sprite = DoorClosed;
            }
        }
	}
}
