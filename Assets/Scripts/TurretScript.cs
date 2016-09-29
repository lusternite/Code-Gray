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
                Vector3 _newVector = transform.position;
                _newVector.x = _newVector.x + 1.0f;
                GameObject Yes = Instantiate(Bullet, _newVector, transform.rotation) as GameObject;
                Yes.GetComponent<Rigidbody2D>().velocity = _fvBulletVelocity;
            }
        }
    }
}
