using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Image interact;

    bool isImageActive = false;

	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void DisplayInteractImg (bool isHit)
    {
        if (isHit && !isImageActive)
        {
            Debug.Log("Image On");
            interact.enabled = true;
        }
        else if (!isHit && isImageActive)
        {
            Debug.Log("Image Off");
            interact.enabled = false;
        }
    }
}
