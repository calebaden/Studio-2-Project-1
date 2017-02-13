using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public LevelController levelController;

    public float anxiety;
    public float confidence;

    // Use this for initialization
    void Start ()
    {
        DontDestroyOnLoad(gameObject);
        Cursor.lockState = CursorLockMode.Locked;
    }
	
	// Update is called once per frame
	void Update ()
    {
        
    }

    // Function that increases the players anxiety level depending on the bool parameter
    public void IncreaseAnxiety (bool inRange)
    {
        float increaseAmount = 0.5f;
        float decreaseAmount = 0.25f;

        if (inRange)
        {
            anxiety = Mathf.Lerp(anxiety, 1, increaseAmount * Time.deltaTime);
        }
        else
        {
            anxiety = Mathf.Lerp(anxiety, 0, decreaseAmount * Time.deltaTime);
        }

        anxiety = Mathf.Clamp01(anxiety);
    }
}
