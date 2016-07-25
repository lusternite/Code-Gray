using UnityEngine;
using System.Collections;

public class ElectricPanel : MonoBehaviour
{

    public GameObject ButtonTriggerObject;
    Button ButtonTrigger;
    public SpriteRenderer renderer;
    public bool Inverted;

    // Use this for initialization
    void Start()
    {
        ButtonTrigger = ButtonTriggerObject.GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Inverted)
        {
            if (ButtonTrigger.ButtonPressed)
            {
                transform.gameObject.tag = "Hazard";
                renderer.color = new Color(1, 1, 26 / 255, 1);
            }
            else
            {
                transform.gameObject.tag = "Untagged";
                renderer.color = new Color(0f, 0f, 0f, 0.2f);
            }
        }
        else
        {
            if (ButtonTrigger.ButtonPressed)
            {
                transform.gameObject.tag = "Untagged";
                renderer.color = new Color(0f, 0f, 0f, 0.2f);
            }
            else
            {
                transform.gameObject.tag = "Hazard";
                renderer.color = new Color(1, 1, 26 / 255, 1);
            }
        }
    }
}
