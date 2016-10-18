using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour
{

    public Vector3 _Velocity;
    bool DeathFlag = false;
    float DeathTimer = 0.3f;

    // Use this for initialization
    void Start()
    {
        //GetComponent<Rigidbody2D>().velocity = _Velocity;
    }

    // Update is called once per frame
    void Update()
    {
        if (DeathFlag)
        {
            DeathTimer -= Time.deltaTime;
            if (DeathTimer < 0.0f)
            {
                Destroy(gameObject);
            }
        }
    }

    void MoveBullet()
    {
        //ansform.position = transform.position + _Velocity;
    }

    void OnCollisionEnter2D(Collision2D Col)
    {
        DeathFlag = true;
        GetComponent<Rigidbody2D>().Sleep();
        GetComponent<CircleCollider2D>().enabled = false;
        GetComponent<Animator>().Play("BulletExplode");
        //Destroy(gameObject);
    }
}
