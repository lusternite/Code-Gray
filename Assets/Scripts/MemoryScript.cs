using UnityEngine;
using System.Collections;

public class MemoryScript : MonoBehaviour
{
    public int health = 1000;
    bool m_deathFlag = false;
    float m_deathTimer = 0.2f;

	// Use this for initialization
	void Start ()
    {
	    
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (m_deathFlag)
        {
            m_deathTimer -= Time.deltaTime;
            if (m_deathTimer < 0.0f)
            {
                Destroy(gameObject);
            }
        }
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

    public void Death()
    {
        m_deathFlag = true;
        GetComponent<Animator>().Play("MemoryDeath");
        GetComponent<BoxCollider2D>().enabled = false;
    }
}
