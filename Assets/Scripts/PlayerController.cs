﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    GameController gameController;
    UIController uiController;
    AudioSource source;

    public float moveSpeed;
    private float maxRayDist = 3;
    public bool canMove = true;
    private float interactAnxiety = 0.2f;

    // Use this for initialization
    void Start ()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        uiController = GameObject.FindGameObjectWithTag("UIController").GetComponent<UIController>();
        source = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        // Create a new raycast infront of the player to determine whether there is a character to interact with
        RaycastHit hit;
        Ray interactRay = new Ray(transform.position, transform.forward);

         if (Physics.Raycast(interactRay, out hit, maxRayDist))                             // Check if the raycast hit any colliders within the maximum range
        {
            if (hit.collider.CompareTag("NPC"))                                             // Check if the racast hit is an NPC
            {
                if (Input.GetButtonDown("Interact"))                                        // If there is a character in range check if the player uses the interact button
                {
                    gameController.anxiety3 -= interactAnxiety;                              // When the player interacts with an NPC negate anxiety
                    //Play conversation sound
                }
            }
        }

        source.volume = gameController.anxiety;                                             // Set the audio source volume to equal the anxiety variable
        
        if (canMove)
        {
            Movement();
        }
        
        gameController.anxiety2 = CharacterRange();
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

    float CharacterRange ()
    {
        int range = 8;
        float anxietyAmount;

        // Determine the distance between the player and the closest NPC, if it is lower than the maximum range, increase the players anxiety
        if (Vector3.Distance(transform.position, FindClosestCharacter().transform.position) <= range)
        {
            anxietyAmount = 0.4f - Vector3.Distance(transform.position, FindClosestCharacter().transform.position) / 20;
        }
        else
        {
            anxietyAmount = 0;
        }

        return anxietyAmount;
    }
}
