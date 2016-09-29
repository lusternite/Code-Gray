using UnityEngine;
using System.Collections;

public class TurretScript : MonoBehaviour {

    public GameObject ButtonTriggerObject;
    Button ButtonTrigger;
    public bool Inverted;
    public float _fTimer = 2.5f;
    public float _fBulletVelocity;
    private Vector2 _fvBulletVelocity;
    float _fTimeSince = 0.0f;

    public GameObject Bullet;

    public bool IsTurretOn;

    // Use this for initialization
    void Start () {
   
        if (Inverted == true)
        {
            IsTurretOn = false;
        }
        else
        {
            IsTurretOn = true;
        }
        ButtonTrigger = ButtonTriggerObject.GetComponent<Button>();

        Debug.Log(_fvBulletVelocity);
    }
	
	// Update is called once per frame
	void Update () {
        if (Inverted)
        {
            if (ButtonTrigger.ButtonPressed)
            {
                IsTurretOn = true;
            }
            else
            {
                IsTurretOn = false;
            }
        }
        else
        {
            if (ButtonTrigger.ButtonPressed)
            {
                IsTurretOn = false;
            }
            else
            {
                IsTurretOn = true;
            }
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
