using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPositionScript : MonoBehaviour
{
    LevelController levelController;

    public GameObject player;
    public float finishTimer;
    public float moveSpeed;

	// Use this for initialization
	void Start ()
    {
        levelController = GameObject.FindGameObjectWithTag("LevelController").GetComponent<LevelController>();
        player.GetComponent<PlayerController>().canMove = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (finishTimer > 0)
        {
            player.transform.position = Vector3.Lerp(player.transform.position, transform.position, moveSpeed * Time.deltaTime);
            finishTimer -= Time.deltaTime;
        }
        else
        {
            player.transform.parent = transform;
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }

        
	}

    void OnTriggerEnter (Collider otherObject)
    {
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
