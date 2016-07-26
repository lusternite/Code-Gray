using UnityEngine;
using System.Collections;

public class ElectricPanel : MonoBehaviour
{

    public GameObject ButtonTriggerObject;
    Button ButtonTrigger;
    public SpriteRenderer renderer;
    public bool Inverted;
    public AudioSource ElectricBuzzSound;

    // Use this for initialization
    void Start()
    {
        ButtonTrigger = ButtonTriggerObject.GetComponent<Button>();
        ElectricBuzzSound.loop = true;
        ElectricBuzzSound.Play();
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
                if (!ElectricBuzzSound.isPlaying)
                {
                    ElectricBuzzSound.loop = true;
                    ElectricBuzzSound.Play();
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
                if (ElectricBuzzSound.isPlaying)
                {
                    ElectricBuzzSound.Stop();
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
                if (ElectricBuzzSound.isPlaying)
                {
                    ElectricBuzzSound.Stop();
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
                if (!ElectricBuzzSound.isPlaying)
                {
                    ElectricBuzzSound.loop = true;
                    ElectricBuzzSound.Play();
                }
            }
        }
    }
}
