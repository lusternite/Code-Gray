﻿using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {

    public bool ButtonPressed;
    public Sprite ButtonOn;
    public Sprite ButtonOff;

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

    void OnTriggerExit2D()
    {
        ButtonPressed = false;
    }
}
