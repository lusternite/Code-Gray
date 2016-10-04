using UnityEngine;
using System.Collections;

public class LaserScript : MonoBehaviour
{
    public GameObject ButtonTriggerObject = null;
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
        _isLaserOn = Inverted ? false : true;

        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = true;
        lineRenderer.useWorldSpace = true;

        if (ButtonTriggerObject != null)
        {
            ButtonTrigger = ButtonTriggerObject.GetComponent<Button>();
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (ButtonTriggerObject != null)
        {
            if (ButtonTrigger.ButtonPressed)
            {
                _isLaserOn = Inverted ? true : false;
            }
            else
            {
                _isLaserOn = Inverted ? false : true;
            }
        }
        if (_isLaserOn == true || ButtonTriggerObject == null)
        {
            lineRenderer.enabled = true;
            lineRenderer.useWorldSpace = true;
            float angle = (angleOfRotation / 2.0f) * Mathf.Sin(Time.time * rotationSpeed) + pointAngle;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            Vector2 direction = Vector2.up;
            direction = q * direction;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction);
            laserHit.position = hit.point;

            // Check collisions
            if (hit.collider.gameObject.name == "Memory Upright Prefab(Clone)")
            {
                hit.collider.gameObject.GetComponent<MemoryScript>().TakeDamage(15);
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