using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    GameController gameController;
    GameObject player;
    AudioSource globalSource;
    AudioSource playerSource;
    public AudioClip chatterSound;

	// Use this for initialization
	void Start ()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        player = GameObject.FindGameObjectWithTag("Player");
        globalSource = GetComponent<AudioSource>();
        playerSource = player.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        playerSource.volume = gameController.anxiety;
    }

    public void PlayChatterSound ()
    {
        globalSource.PlayOneShot(chatterSound);
    }
}
