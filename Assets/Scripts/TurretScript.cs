using UnityEngine;
using System.Collections;

public class TurretScript : MonoBehaviour {

    public GameObject ButtonTriggerObject = null;
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

        IsTurretOn = Inverted ? false : true;

        if (ButtonTriggerObject != null)
        {
            ButtonTrigger = ButtonTriggerObject.GetComponent<Button>();
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (ButtonTriggerObject != null)
        {
            if (ButtonTrigger.ButtonPressed)
            {
                IsTurretOn = Inverted ? true : false;
            }
            else
            {
                IsTurretOn = Inverted ? false : true;
            }
        }
        if (IsTurretOn || ButtonTriggerObject == null)
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
