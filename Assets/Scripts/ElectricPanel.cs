using UnityEngine;
using System.Collections;

public class ElectricPanel : MonoBehaviour
{

    public GameObject ButtonTriggerObject;
    Button ButtonTrigger;

    // Use this for initialization
    void Start()
    {
        ButtonTrigger = ButtonTriggerObject.GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ButtonTrigger.ButtonPressed)
        {
            transform.gameObject.tag = "Untagged";
        }
        else
        {
            transform.gameObject.tag = "Hazard";
        }
    }
}
