using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPositionScript : MonoBehaviour
{
    public GameObject player;
    public float finishTimer;
    public float moveSpeed;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (finishTimer > 0)
        {
            finishTimer -= Time.deltaTime;
        }
        else
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }

        player.transform.position = Vector3.Lerp(player.transform.position, transform.position, moveSpeed * Time.deltaTime);
	}

    void OnTriggerEnter (Collider otherObject)
    {
        LevelController levelController = player.GetComponent<LevelController>();

        if (otherObject.CompareTag("LevelTrigger"))
        {
            if (levelController.levelNum != 4)
            {
                levelController.CallCoroutine(1);
            }
            else
            {
                levelController.CallCoroutine(-4);
            }
        }
    }
}
