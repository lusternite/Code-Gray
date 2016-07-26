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
                //renderer.color = new Color(1, 1, 26 / 255, 1);
                for (int i = 0; i < transform.childCount; i++)
                {
                    transform.GetChild(i).GetComponent<Animator>().Play("ElectricPanelAnim");
                }
            }
            else
            {
                transform.gameObject.tag = "Untagged";
                //renderer.color = new Color(0f, 0f, 0f, 0.2f);
                for (int i = 0; i < transform.childCount; i++)
                {
                    transform.GetChild(i).GetComponent<Animator>().Play("ElectricPanelAnim_Off");
                }
            }
        }
        else
        {
            if (ButtonTrigger.ButtonPressed)
            {
                transform.gameObject.tag = "Untagged";
                //renderer.color = new Color(0f, 0f, 0f, 0.2f);
                for (int i = 0; i < transform.childCount; i++)
                {
                    transform.GetChild(i).GetComponent<Animator>().Play("ElectricPanelAnim_Off");
                }
            }
            else
            {
                transform.gameObject.tag = "Hazard";
                //renderer.color = new Color(1, 1, 26 / 255, 1);
                for (int i = 0; i < transform.childCount; i++)
                {
                    transform.GetChild(i).GetComponent<Animator>().Play("ElectricPanelAnim");
                }
            }
        }
    }
}
