using UnityEngine;
using System.Collections;

public class LaserScript : MonoBehaviour
{
    public GameObject[] ButtonTriggerObjects;
    public bool Inverted = false;
    public Transform laserHit;
    public float angleOfRotation = 180.0f;
    public float pointAngle = 0.0f;
    public float rotationSpeed = 10.0f;
    
    private LineRenderer lineRenderer;

    public bool _isLaserOn;
    public bool XORgate;

    public int _iDamage = 15;

    // Use this for initialization
    void Start ()
    {
        _isLaserOn = Inverted ? false : true;

        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = true;
        lineRenderer.useWorldSpace = true;
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

        if (numPressed % 2 == 1)
        {
            _isLaserOn = Inverted ? true : false;
        }
        else
        {
            _isLaserOn = Inverted ? false : true;
        }


        if (_isLaserOn == true)
        {
            lineRenderer.enabled = true;
            lineRenderer.useWorldSpace = true;
            float angle = (angleOfRotation / 2.0f) * Mathf.Sin(Time.timeSinceLevelLoad * rotationSpeed) + pointAngle;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            Vector2 direction = Vector2.up;
            direction = q * direction;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction);
            laserHit.position = hit.point;

            // Check collisions
            if (hit.collider.gameObject.name == "Memory Upright Prefab(Clone)")
            {
                hit.collider.gameObject.GetComponent<MemoryScript>().TakeDamage(_iDamage);
                Debug.Log("laser hit memory");
            }
            if (hit.collider.gameObject.name == "Player")
            {
                hit.collider.gameObject.GetComponent<PlayerBehaviour>().Kill();
            }

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