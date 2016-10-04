using UnityEngine;
using System.Collections;

public class TurretScript : MonoBehaviour {

    public GameObject[] ButtonTriggerObjects;
    public bool Inverted;
    public float _fTimer = 2.5f;
    public float _fBulletVelocity;
    private Vector2 _fvBulletVelocity;
    float _fTimeSince = 0.0f;

    public GameObject Bullet;

    public bool IsTurretOn;
    public bool XORgate;

    // Use this for initialization
    void Start ()
    {
        IsTurretOn = Inverted ? false : true;
    }
	
	// Update is called once per frame
	void Update ()
    {
        int numPressed = 0;

        if (XORgate)
        {
            for (int i = 0; i < ButtonTriggerObjects.Length; ++i)
            {
                if (ButtonTriggerObjects[i].GetComponent<Button>().ButtonPressed)
                {
                    numPressed += 1;
                }
            }
        }
        else
        {
            for (int i = 0; i < ButtonTriggerObjects.Length; ++i)
            {
                if (ButtonTriggerObjects[i].GetComponent<Button>().ButtonPressed)
                {
                    numPressed += 1;
                    break;
                }
            }
        }

        //for (int i = 0; i < ButtonTriggerObjects.Length; ++i)
        //{
        //    if (ButtonTriggerObjects[i].GetComponent<Button>().ButtonPressed)
        //    {
        //        IsTurretOn = Inverted ? true : false;
        //        break;
        //    }
        //    else
        //    {
        //        IsTurretOn = Inverted ? false : true;
        //    }
        //}

        if (numPressed % 2 == 1)
        {
            IsTurretOn = Inverted ? true : false;
        }
        else
        {
            IsTurretOn = Inverted ? false : true;
        }

        if (IsTurretOn)
        {
            if (Time.time > _fTimeSince + _fTimer)
            {
                Debug.Log("sfdhsgf");
                _fTimeSince = Time.time;
                Vector2 _frontVector = new Vector2(Mathf.Cos(Mathf.Deg2Rad * transform.localEulerAngles.z), Mathf.Sin(Mathf.Deg2Rad * transform.localEulerAngles.z));
                Vector3 _newVector = transform.position;
                Debug.Log(_frontVector);
                //glm::vec2(m_BulletSpeed * glm::cos(currentAngle), m_BulletSpeed * glm::sin(currentAngle)
                _newVector.x += _frontVector.x;
                _newVector.y += _frontVector.y;
                GameObject Yes = Instantiate(Bullet, _newVector, transform.rotation) as GameObject;
                Yes.GetComponent<Rigidbody2D>().velocity = _fBulletVelocity * _frontVector;
            }
        }
    }
}
