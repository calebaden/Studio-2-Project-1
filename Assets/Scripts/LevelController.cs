using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    GameController gameController;

    public GameObject player;
    public GameObject endPosition;

    public int levelNum;

    private bool isPaused;

    public float levelTimer;

    // Use this for initialization
    void Start ()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        // If the current level is not the main menu...
        if (levelNum == 0)
        {
            if (Input.GetButtonDown("Submit"))
            {
                StartCoroutine(ChangeLevel(1));
            }
        }
        else
        {
            if (levelTimer > 0)
            {
                //levelTimer -= Time.deltaTime;
            }
            else
            {
                endPosition.SetActive(true);
            }

            if (Input.GetButtonDown("Cancel"))
            {
                if (!isPaused)
                {
                    Time.timeScale = 0;
                    Cursor.lockState = CursorLockMode.None;
                    isPaused = true;
                }
                else
                {
                    Time.timeScale = 1;
                    Cursor.lockState = CursorLockMode.Locked;
                    isPaused = false;
                }
            }
        }
    }

    public void CallCoroutine (int index)
    {
        StartCoroutine(ChangeLevel(index));
    }

    // Coroutine that calls the fade function from the fade script and then loads the next scene
    IEnumerator ChangeLevel(int amount)
    {
        float fadeTime = gameController.gameObject.GetComponent<SceneFadeScript>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + amount);
    }
}
