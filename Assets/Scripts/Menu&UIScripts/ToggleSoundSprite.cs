using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ToggleSoundSprite : MonoBehaviour
{
    public Text SoundOnText;
    public Text SoundOffText;

    bool SoundOn = true;

	// Use this for initialization
	void Start ()
    {
        SoundOffText.enabled = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void Toggle()
    {
        SoundOn = !SoundOn;
        SoundOnText.enabled = SoundOn;
        SoundOffText.enabled = !SoundOn;
    }
}
