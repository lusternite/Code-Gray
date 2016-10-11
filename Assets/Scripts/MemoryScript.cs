using UnityEngine;
using System.Collections;

public class MemoryScript : MonoBehaviour
{
    public int health = 1000;

	// Use this for initialization
	void Start ()
    {
	    
	}
	
	// Update is called once per frame
	void Update ()
    {

	}

    //void OnCollisionEnter2D(Collision2D col)
    //{
    //    if (col.gameObject.tag == "Hazard")
    //    {
    //        Debug.Log("memory hit by hazard");
    //        health -= 200;
    //    }
    //}

    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    public int GetHealth()
    {
        return health;
    }
}
