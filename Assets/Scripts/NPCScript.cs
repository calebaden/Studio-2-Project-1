using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCScript : MonoBehaviour
{
    LevelController levelController;
    Material newMaterial;
    Renderer rend;
    public bool isFriend = false;
    public float interactProgress;
    float interactAnxiety = 0.1f;
    float unfriendSpeed = 0.5f;
    Color charColor;

    // Use this for initialization
    void Start ()
    {
        levelController = GameObject.FindGameObjectWithTag("LevelController").GetComponent<LevelController>();
        rend = GetComponent<Renderer>();
        newMaterial = rend.materials[1];
    }

    void Update ()
    {
        // When becoming a friend of the player, reduce anxiety by the interact anxiety variable, add this object to the friends list and set is friend to true
        if (interactProgress >= 1 && !isFriend)
        {
            levelController.anxiety3 -= interactAnxiety;
            levelController.friends.Add(gameObject);
            isFriend = true;
        }

        // Set the new materials colour to equal a new color where the blue value is equal to interact progress
        charColor = new Color(0, 0, interactProgress);
        newMaterial.color = charColor;
    }

    // Function that increases interact progress as long as it is below 1
    public void Befriend ()
    {
        if (interactProgress < 1)
        {
            interactProgress += Time.deltaTime;
        }
    }

    // Function that decreases interact progress as long as it is above 0
    public void Unfriend ()
    {
        if (interactProgress > 0)
        {
            interactProgress -= unfriendSpeed * Time.deltaTime;
        }
    }
}
