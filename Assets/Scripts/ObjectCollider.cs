using UnityEngine;
using System.Collections;

public class ObjectCollider : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerStay2D(Collider2D Col)
    {
        //Debug.Log("Triggered");
        if (Col.gameObject.tag == "Object")
        {
            transform.parent.GetComponent<PlayerBehaviour>().CanJump = true;
        }
    }

    void OnTriggerExit2D(Collider2D Col)
    {
        //Debug.Log("Triggered");
        if (Col.gameObject.tag == "Object")
        {
            transform.parent.GetComponent<PlayerBehaviour>().CanJump = false;
        }
    }
}
