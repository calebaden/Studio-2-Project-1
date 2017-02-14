﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCScript : MonoBehaviour
{
    public Material newMaterial;
    Renderer rend;
    public bool isFriend = false;

    // Use this for initialization
    void Start ()
    {
        rend = GetComponent<Renderer>();
	}

    public void ChangeMaterial ()
    {
        rend.material = newMaterial;
        isFriend = true;
    }
}
