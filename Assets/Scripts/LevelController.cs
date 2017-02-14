using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    GameController gameController;
    UIController uiController;

    public GameObject endPosition;
    public int levelNum;
    private bool isPaused;

    // Use this for initialization
    void Start ()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        uiController = GameObject.FindGameObjectWithTag("UIController").GetComponent<UIController>();
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
            if (Input.GetButtonDown("Cancel"))
            {
                Application.Quit();
            }
        }
        else
        {
            // When the player reaches minimum anxiety, finish the level
            if (gameController.anxiety <= -0.5f)
            {
                endPosition.SetActive(true);
            }

            if (isPaused)
            {
                if (Input.GetButtonDown("Submit"))
                {
                    uiController.PauseMenuImage(0);
                    Time.timeScale = 1;
                    isPaused = false;
                }
                if (Input.GetButtonDown("Cancel"))
                {
                    Time.timeScale = 1;
                    isPaused = false;
                    SceneManager.LoadScene(0);
                }
            }
            else
            {
                if (Input.GetButtonDown("Cancel"))
                {
                    uiController.PauseMenuImage(1);
                    isPaused = true;
                    Time.timeScale = 0;
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
