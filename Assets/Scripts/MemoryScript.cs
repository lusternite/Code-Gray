using UnityEngine;
using System.Collections;

public class MemoryScript : MonoBehaviour
{
    public int health = 1000;
    public int InitHealth = 0;
    bool m_deathFlag = false;
    float m_deathTimer = 0.2f;
    public Sprite[] Sprites;

    // Use this for initialization
    void Start ()
    {
        InitHealth = health;

    }
	
	// Update is called once per frame
	void Update ()
    {
        for (int i = 0; i < 9; ++i)
        {
            if (health <= ((InitHealth / 9) * i))
            {
                GetComponent<SpriteRenderer>().sprite = Sprites[8 - i];
                Vector2 newVec;
                newVec.x = 2 - (0.07777777f * i);
                newVec.y = 2 - (0.07777777f * i);
                GetComponent<BoxCollider2D>().size = newVec;
                break;
            }
        }

        if (health != InitHealth)
        {
            GetComponent<Animator>().Stop();
        }
           
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
