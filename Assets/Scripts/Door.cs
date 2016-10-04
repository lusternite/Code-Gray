using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour
{

    public GameObject[] ButtonTriggerObjects;
    public Sprite DoorClosed;
    public Sprite DoorOpen;
    Button ButtonTrigger;
    public bool Inverted;

	// Use this for initialization
	void Start ()
    {
        
	}

    // Update is called once per frame
    void Update()
    {
        int numPressed = 0;
        for (int i = 0; i < ButtonTriggerObjects.Length; ++i)
        {
            if (ButtonTriggerObjects[i].GetComponent<Button>().ButtonPressed)
                numPressed += 1;
        }

        if (numPressed % 2 == 1)
        {
            if (Inverted)
            {
                GetComponent<BoxCollider2D>().enabled = true;
                GetComponent<SpriteRenderer>().sprite = DoorClosed;
            }
            else
            {
                GetComponent<BoxCollider2D>().enabled = false;
                GetComponent<SpriteRenderer>().sprite = DoorOpen;
            }
        }
        else
        {
            if (Inverted)
            {
                GetComponent<BoxCollider2D>().enabled = false;
                GetComponent<SpriteRenderer>().sprite = DoorOpen;
            }
            else
            {
                GetComponent<BoxCollider2D>().enabled = true;
                GetComponent<SpriteRenderer>().sprite = DoorClosed;
            }
        }
    }
 //           if (Inverted)
 //           {
 //               if (ButtonTriggerObjects[i].GetComponent<Button>().ButtonPressed)
 //               {
 //                   GetComponent<BoxCollider2D>().enabled = true;
 //                   //GetComponent<SpriteRenderer>().enabled = true;
 //                   GetComponent<SpriteRenderer>().sprite = DoorClosed;
 //                   break;
 //               }
 //               else
 //               {
 //                   GetComponent<BoxCollider2D>().enabled = false;
 //                   //GetComponent<SpriteRenderer>().enabled = false;
 //                   GetComponent<SpriteRenderer>().sprite = DoorOpen;
 //               }
 //           }
 //           else
 //           {
 //               if (ButtonTriggerObjects[i].GetComponent<Button>().ButtonPressed)
 //               {
 //                   GetComponent<BoxCollider2D>().enabled = false;
 //                   //GetComponent<SpriteRenderer>().enabled = false;
 //                   GetComponent<SpriteRenderer>().sprite = DoorOpen;
 //                   break;
 //               }
 //               else
 //               {
 //                   GetComponent<BoxCollider2D>().enabled = true;
 //                   //GetComponent<SpriteRenderer>().enabled = true;
 //                   GetComponent<SpriteRenderer>().sprite = DoorClosed;
 //               }
 //           }
 //       }
	//}
}
