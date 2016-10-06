using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;

public class LevelDoorScript : MonoBehaviour
{
    public int levelToGoTo = 0;
    public bool isExitDoor = false;
    public bool isStartContinueDoor = false;

    // Use this for initialization
    void Start ()
    {
	
	}
	
	// Update is called once per frame
	public void GoToLevel()
    {
        if (isExitDoor)
        {
            Application.Quit();
        }
        else if (isStartContinueDoor)
        {

            int i = GameObject.Find("GameManager").GetComponent<GameManager>().GetHighestLevel();
            if (i <= 2)
            {
                Application.LoadLevel(2);
            }
            else
            {
                Application.LoadLevel(i);
            }
        }
        else if (levelToGoTo == 0)
        {
            Application.LoadLevel(Application.loadedLevel + 1);
        }
        else
        {
            Application.LoadLevel(levelToGoTo);
        }
    }
}
