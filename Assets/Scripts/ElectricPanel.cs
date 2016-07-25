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
                renderer.color = new Color(255f, 255f, 26f, 255f);
            }
            else
            {
                transform.gameObject.tag = "Untagged";
                renderer.color = new Color(0f, 0f, 0f, 255f);
            }
        }
        else
        {
            if (ButtonTrigger.ButtonPressed)
            {
                transform.gameObject.tag = "Untagged";
                renderer.color = new Color(0f, 0f, 0f, 255f);
            }
            else
            {
                transform.gameObject.tag = "Hazard";
                renderer.color = new Color(255f, 255f, 26f, 255f);
            }
        }
    }
}
