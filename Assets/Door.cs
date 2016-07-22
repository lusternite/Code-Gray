using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

    public GameObject ButtonTriggerObject;
    Button ButtonTrigger;

	// Use this for initialization
	void Start () {
        ButtonTrigger = ButtonTriggerObject.GetComponent<Button>();
	}
	
	// Update is called once per frame
	void Update () {
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
