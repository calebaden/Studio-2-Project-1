using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    GameController gameController;

    public float moveSpeed;

    private float maxRayDist = 3;

    AudioSource source;

    // Use this for initialization
    void Start ()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        source = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        // Create a new raycast infront of the player to determine whether there is a character to interact with
        RaycastHit hit;
        Ray interactRay = new Ray(transform.position, transform.forward);

         if (Physics.Raycast(interactRay, out hit, maxRayDist))
        {
            if (hit.collider.CompareTag("NPC"))
            {
                // If there is a character in range check if the player uses the interact button
                if (Input.GetButtonDown("Interact"))
                {
                    gameController.anxiety -= 0.25f;
                    Debug.Log("You did it!");       // If the player does interact, lower the players anxiety level
                }
            }
        }

        source.volume = gameController.anxiety;
        
        Movement();
        CharacterRange();
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

    // Function that finds and returns the closest NPC from the player character
    GameObject FindClosestCharacter ()
    {
        GameObject[] nonPlayables = GameObject.FindGameObjectsWithTag("NPC");       // Gathers all the NPC's from the current level into an array
        GameObject closest = null;
        float distance = Mathf.Infinity;

        // For each NPC in the array, compare their distance from the player to find the shortest distance
        foreach (GameObject character in nonPlayables)
        {
            float diff = Vector3.Distance(transform.position, character.transform.position);
            if (distance > diff)
            {
                distance = diff;
                closest = character;
            }
        }
        return closest;
    }

    void CharacterRange ()
    {
        int range = 5;
        bool inRange;

        // Determine the distance between the player and the closest NPC, if it is lower than the maximum range, increase the players anxiety
        if (Vector3.Distance(transform.position, FindClosestCharacter().transform.position) <= range)
        {
            inRange = true;
        }
        else
        {
            inRange = false;
        }

        gameController.IncreaseAnxiety(inRange);
    }
}
