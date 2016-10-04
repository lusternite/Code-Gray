using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour
{

    public Vector3 _Velocity;

    // Use this for initialization
    void Start()
    {
        //GetComponent<Rigidbody2D>().velocity = _Velocity;
    }

    // Update is called once per frame
    void Update()
    {
        MoveBullet();
    }

    void MoveBullet()
    {
        //ansform.position = transform.position + _Velocity;
    }

    void OnCollisionEnter2D(Collision2D Col)
    {
        Destroy(gameObject);
    }
}
