using UnityEngine;
using System.Collections;

public enum Type { MEMORY };

public class CollectibleScript : MonoBehaviour
{
    public Type CollectibleType;

	// Use this for initialization
	void Start ()
    {
	    
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    public Type GetCollectibleType()
    {
        return CollectibleType;
    }
}
