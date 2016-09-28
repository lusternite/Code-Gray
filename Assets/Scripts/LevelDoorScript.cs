using UnityEngine;
using System.Collections;

public class LevelDoorScript : MonoBehaviour
{
    public int levelToGoTo = 0;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	public void GoToLevel()
    {
        if (levelToGoTo == 0)
        {
            Application.LoadLevel(Application.loadedLevel + 1);
        }
        else
        {
            Application.LoadLevel(levelToGoTo);
        }
    }
}
