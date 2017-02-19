using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPositionScript : MonoBehaviour
{
    LevelController levelController;
    PlayerController playerController;

    public GameObject player;
    float finishTimer = 3;
    float moveSpeed = 10;

    public GameObject schoolOneFocus;
    public GameObject schoolTwoFocus;
    public GameObject schoolThreeFocus;
    public GameObject schoolFourFocus;

    public GameObject[] schoolBarriers;

    // Use this for initialization
    void Start ()
    {
        levelController = GameObject.FindGameObjectWithTag("LevelController").GetComponent<LevelController>();
        playerController = player.GetComponent<PlayerController>();
        playerController.canMove = false;
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
            // Call the unfriend function on all of the game objects in the friends list
            foreach(GameObject friend in levelController.friends)
            {
                NPCScript npcScript = friend.GetComponent<NPCScript>();
                npcScript.Unfriend();
            }

            // Disable all the barrier colliders
            foreach (GameObject barrier in schoolBarriers)
            {
                barrier.GetComponent<Collider>().enabled = false;
            }

            player.transform.parent = transform;
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }
	}

    void OnTriggerEnter (Collider otherObject)
    {
        if (otherObject.CompareTag("SchoolOneFinish"))
        {
            playerController.lookTarget = schoolTwoFocus;       // Change the mouse view focus to be school two
        }
        else if (otherObject.CompareTag("SchoolTwoFinish"))
        {
            playerController.lookTarget = schoolThreeFocus;     // Change the mouse view focus to be school two
        }
        else if (otherObject.CompareTag("SchoolThreeFinish"))
        {
            playerController.lookTarget = schoolFourFocus;      // Change the mouse view focus to be school two
        }
        else if (otherObject.CompareTag("SchoolFourFinish"))
        {
            levelController.CallCoroutine(0);                   // After finishing the fourth level, load the menu
        }

        // When arriving at the new school, reset values, enable barrier colliders and disable this object
        if (otherObject.CompareTag("SchoolTwoStart") || otherObject.CompareTag("SchoolThreeStart") || otherObject.CompareTag("SchoolFourStart"))
        {
            foreach (GameObject barrier in schoolBarriers)
            {
                barrier.GetComponent<Collider>().enabled = true;
            }
            levelController.friends.Clear();
            finishTimer = 3;
            levelController.anxiety3 = 0.2f;
            levelController.anxiety = 0;
            player.transform.parent = null;
            playerController.canMove = true;
            player.transform.GetChild(0).GetComponent<CameraScript>().mouseLook = new Vector2(0, 0);
            otherObject.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
