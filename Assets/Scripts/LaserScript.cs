using UnityEngine;
using System.Collections;

public class LaserScript : MonoBehaviour
{
    public GameObject ButtonTriggerObject;
    Button ButtonTrigger;
    public bool Inverted = false;
    public Transform laserHit;
    public float angleOfRotation = 180.0f;
    public float pointAngle = 0.0f;
    public float rotationSpeed = 10.0f;
    
    private LineRenderer lineRenderer;

    public bool _isLaserOn;

    // Use this for initialization
    void Start ()
    {
        if (Inverted == true)
        {
            _isLaserOn = false;
        }
        else
        {
            _isLaserOn = true;
        }

        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = true;
        lineRenderer.useWorldSpace = true;

        ButtonTrigger = ButtonTriggerObject.GetComponent<Button>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (ButtonTrigger.ButtonPressed)
        {
            if (Inverted == true)
            {
                _isLaserOn = true;
            }
            else
            {
                _isLaserOn = false;
            }
        }
        else
        {
            if (Inverted == true)
            {
                _isLaserOn = false;
            }
            else
            {
                _isLaserOn = true;
            }
        }

        if (_isLaserOn == true)
        {
            lineRenderer.enabled = true;
            lineRenderer.useWorldSpace = true;
            float angle = (angleOfRotation / 2.0f) * Mathf.Sin(Time.time * rotationSpeed) + pointAngle;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            Vector2 direction = Vector2.up;
            direction = q * direction;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction);
            laserHit.position = hit.point;
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, laserHit.position);
        }
        else
        {
            lineRenderer.enabled = false;
            lineRenderer.useWorldSpace = false;
        }
    }
}
