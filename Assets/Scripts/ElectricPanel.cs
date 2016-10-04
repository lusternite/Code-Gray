using UnityEngine;
using System.Collections;

public class ElectricPanel : MonoBehaviour
{

    public GameObject[] ButtonTriggerObjects;
    Button ButtonTrigger;
    //public SpriteRenderer renderer;
    public bool Inverted;
    public bool IsVert;

    public bool IsElectricOn;
    public bool XORgate;
    // public AudioSource ElectricBuzzSound;

    // Use this for initialization
    void Start()
    {
        IsElectricOn = Inverted ? false : true;
        //ButtonTrigger = ButtonTriggerObject.GetComponent<Button>();
        //ElectricBuzzSound.loop = true;
        // ElectricBuzzSound.Play();
    }

    // Update is called once per frame
    void Update()
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
            IsElectricOn = Inverted ? true : false;
        }
        else
        {
            IsElectricOn = Inverted ? false : true;
        }






        if (IsElectricOn)
        {
            transform.gameObject.tag = "Hazard";
        }
        else
        {
            transform.gameObject.tag = "Untagged";
        }
        //if (Inverted)
        //{
        //    if (ButtonTrigger.ButtonPressed)
        //    {
        //        transform.gameObject.tag = "Hazard";
        //        //renderer.color = new Color(1, 1, 26 / 255, 1);
        //        for (int i = 0; i < transform.childCount; i++)
        //        {
        //            if (IsVert)
        //            {
        //                transform.GetChild(i).GetComponent<Animator>().Play("ElectricPanelVert_On");
        //            }
        //            else
        //            {
        //                transform.GetChild(i).GetComponent<Animator>().Play("ElectricPanelAnim");
        //            }
        //        }
        //    }
        //    else
        //    {
        //        transform.gameObject.tag = "Untagged";
        //        //renderer.color = new Color(0f, 0f, 0f, 0.2f);
        //        for (int i = 0; i < transform.childCount; i++)
        //        {
        //            if (IsVert)
        //            {
        //                transform.GetChild(i).GetComponent<Animator>().Play("ElectricPanelVert_Off");
        //            }
        //            else
        //            {
        //                transform.GetChild(i).GetComponent<Animator>().Play("ElectricPanelAnim_Off");
        //            }
        //        }
        //    }
        //}
        //else
        //{
        //    if (ButtonTrigger.ButtonPressed)
        //    {
        //        transform.gameObject.tag = "Untagged";
        //        //renderer.color = new Color(0f, 0f, 0f, 0.2f);
        //        for (int i = 0; i < transform.childCount; i++)
        //        {
        //            if (IsVert)
        //            {
        //                transform.GetChild(i).GetComponent<Animator>().Play("ElectricPanelVert_Off");
        //            }
        //            else
        //            {
        //                transform.GetChild(i).GetComponent<Animator>().Play("ElectricPanelAnim_Off");
        //            }
        //        }
        //    }
        //    else
        //    {
        //        transform.gameObject.tag = "Hazard";
        //        //renderer.color = new Color(1, 1, 26 / 255, 1);
        //        for (int i = 0; i < transform.childCount; i++)
        //        {
        //            if (IsVert)
        //            {
        //                transform.GetChild(i).GetComponent<Animator>().Play("ElectricPanelVert_On");
        //            }
        //            else
        //            {
        //                transform.GetChild(i).GetComponent<Animator>().Play("ElectricPanelAnim");
        //            }
        //        }
        //    }
        //}
    }
}
