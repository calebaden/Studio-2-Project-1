using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    LevelController levelController;
    UIController uiController;
    AudioController audioController;
    GameObject npc = null;
    public GameObject lookTarget;
    Transform mCam;

    public float baseMoveSpeed;
    public float moveSpeed;
    private float maxRayDist = 2;
    public bool canMove = true;

    // Use this for initialization
    void Start ()
    {
        levelController = GameObject.FindGameObjectWithTag("LevelController").GetComponent<LevelController>();
        uiController = GameObject.FindGameObjectWithTag("UIController").GetComponent<UIController>();
        audioController = GameObject.FindGameObjectWithTag("AudioController").GetComponent<AudioController>();
        mCam = transform.GetChild(0);
    }
	
	// Update is called once per frame
	void Update ()
    {
        // Create a new raycast infront of the player to determine whether there is a character to interact with
        RaycastHit hit;
        Ray interactRay = new Ray(mCam.position, mCam.forward);
        Debug.DrawRay(mCam.position, mCam.forward);

        if (Physics.Raycast(interactRay, out hit, maxRayDist) && hit.collider.CompareTag("NPC"))                             // Check if the raycast hit an NPC within the maximum range
        {
            NPCScript npcScript = hit.collider.GetComponent<NPCScript>();
            npc = hit.collider.gameObject;
            uiController.isImageActive = true;

            if (Input.GetButtonDown("Interact") && !npcScript.isFriend)
            {
                audioController.PlayChatterSound();
            }

            if (Input.GetButton("Interact") && !npcScript.isFriend)                                         // If there is a character in range check if the player uses the interact button
            {
                npcScript.Befriend();
                uiController.progressBar.fillAmount = npcScript.interactProgress;
            }

            if (Input.GetButtonUp("Interact"))
            {
                if (!npcScript.isFriend)
                {
                    npcScript.interactProgress = 0;
                }
                uiController.progressBar.fillAmount = 0;
            }

            if (npcScript.isFriend)
            {
                uiController.progressBar.fillAmount = 0;
            }
        }
        else if (npc != null)
        {
            if (!npc.GetComponent<NPCScript>().isFriend)
            {
                npc.GetComponent<NPCScript>().interactProgress = 0;
            }
            uiController.isImageActive = false;
            uiController.progressBar.fillAmount = 0;
            npc = null;
        }

        if (canMove)
        {
            Movement();
        }
        
        levelController.anxiety2 = CharacterRange();
        moveSpeed = (baseMoveSpeed * (-levelController.anxiety + 1) + 1);
	}

    // Function that controls the players movement
    void Movement ()
    {
        // Gather the horizontal and vertical axis inputs into variables
        float horizontal = (Input.GetAxis("Horizontal") * moveSpeed) * Time.deltaTime;
        float vertical = (Input.GetAxis("Vertical") * moveSpeed) * Time.deltaTime;

        // Translate the players position by the horizontal and vertical variables
        transform.Translate(horizontal, 0, vertical);
    }

    // Function that finds and returns the closest non-friended NPC to the player character
    GameObject FindClosestCharacter ()
    {
        GameObject[] nonPlayables = GameObject.FindGameObjectsWithTag("NPC");       // Gathers all the NPC's from the current level into an array
        GameObject closest = null;
        float distance = Mathf.Infinity;

        // For each NPC in the array, compare their distance from the player to find the shortest distance
        foreach (GameObject character in nonPlayables)
        {
            if (!character.GetComponent<NPCScript>().isFriend)
            {
                float diff = Vector3.Distance(transform.position, character.transform.position);
                if (distance > diff)
                {
                    distance = diff;
                    closest = character;
                }
            }
        }
        return closest;
    }

    // Function that returns a float determined by the distance between the player and the closest non-friended npc
    float CharacterRange ()
    {
        int range = 8;
        float anxietyAmount = 0;

        if (FindClosestCharacter() != null)
        {
            // Determine the distance between the player and the closest NPC, if it is lower than the maximum range, increase the players anxiety
            if (Vector3.Distance(transform.position, FindClosestCharacter().transform.position) <= range)
            {
                anxietyAmount = 0.8f - Vector3.Distance(transform.position, FindClosestCharacter().transform.position) / 20;
            }
            else
            {
                anxietyAmount = 0;
            }
        }
        return anxietyAmount;
    }
}
