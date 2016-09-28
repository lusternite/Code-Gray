using UnityEngine;
using System.Collections;

public class LaserScript : MonoBehaviour
{
    public Transform laserHit;
    public float angleOfRotation = 180.0f;
    public float pointAngle = 0.0f;
    public float rotationSpeed = 10.0f;
    
    private LineRenderer lineRenderer;

    // Use this for initialization
    void Start ()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = true;
        lineRenderer.useWorldSpace = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
        float angle = (angleOfRotation / 2.0f) * Mathf.Sin(Time.time * rotationSpeed) + pointAngle;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        Vector2 direction = Vector2.up;
        direction = q * direction;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction);
        laserHit.position = hit.point;
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, laserHit.position);

    }
}
