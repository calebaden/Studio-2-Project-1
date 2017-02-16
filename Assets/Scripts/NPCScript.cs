using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCScript : MonoBehaviour
{
    LevelController levelController;
    public Material newMaterial;
    Renderer rend;
    public bool isFriend = false;
    public float interactProgress;
    public float interactAnxiety = 0.1f;
    Color charColor;

    // Use this for initialization
    void Start ()
    {
        levelController = GameObject.FindGameObjectWithTag("LevelController").GetComponent<LevelController>();
        rend = GetComponent<Renderer>();
    }

    void Update ()
    {
        if (interactProgress >= 1 && !isFriend)
        {
            levelController.anxiety3 -= interactAnxiety;
            levelController.friends.Add(gameObject);
            isFriend = true;
        }

        charColor = new Color(0, 0, interactProgress);
        rend.material.color = charColor;
    }

    public void Befriend ()
    {
        if (interactProgress < 1)
        {
            interactProgress += Time.deltaTime;
        }
    }

    public void Unfriend ()
    {
        if (interactProgress > 0)
        {
            interactProgress -= Time.deltaTime;
        }
    }
}
