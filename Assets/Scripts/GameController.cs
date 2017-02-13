using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public float anxiety;
    public float anxiety2;
    public float anxiety3;

    // Use this for initialization
    void Start ()
    {
        DontDestroyOnLoad(gameObject);
        Cursor.lockState = CursorLockMode.Locked;
    }
	
	// Update is called once per frame
	void Update ()
    {
        anxiety = anxiety2 + anxiety3;
    }
}
