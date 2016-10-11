using UnityEngine;
using System.Collections;

public class FireScript : MonoBehaviour {

    public int _iFireDamage = 3;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerStay2D(Collider2D Col)
    {
        if (Col.gameObject.name == "Memory Upright Prefab(Clone)" )
        {
            Col.gameObject.GetComponent<MemoryScript>().TakeDamage(_iFireDamage);
            Debug.Log("laser hit memory");
        }
    }
}
