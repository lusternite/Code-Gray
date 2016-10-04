using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

    public GameObject[] ButtonTriggerObjects;
    public Sprite DoorClosed;
    public Sprite DoorOpen;
    Button ButtonTrigger;
    public bool Inverted;
    public bool IsDoorOpen;

	// Use this for initialization
	void Start () {
        IsDoorOpen = Inverted ? false : true;
    }
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < ButtonTriggerObjects.Length; ++i)
        {
            if (ButtonTriggerObjects[i].GetComponent<Button>().ButtonPressed)
            {
                IsDoorOpen = Inverted ? true : false;
                break;
            }
            else
            {
                IsDoorOpen = Inverted ? false : true;
            }
        }
        if (IsDoorOpen)
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
}
