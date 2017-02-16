using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    LevelController levelController;
    GameObject player;
    AudioSource globalSource;
    AudioSource playerSource;
    public AudioClip chatterSound;

	// Use this for initialization
	void Start ()
    {
        levelController = GameObject.FindGameObjectWithTag("LevelController").GetComponent<LevelController>();
        player = GameObject.FindGameObjectWithTag("Player");
        globalSource = GetComponent<AudioSource>();
        playerSource = player.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        playerSource.volume = levelController.anxiety;
    }

    public void PlayChatterSound ()
    {
        globalSource.PlayOneShot(chatterSound);
    }
}
