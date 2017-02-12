using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public float anxiety;

    int currentLevel = 0;

    public float timeRemaining;
    public float levelTime;
    private bool isPaused;

	// Use this for initialization
	void Start ()
    {
        DontDestroyOnLoad(gameObject);
        Cursor.lockState = CursorLockMode.Locked;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButtonDown("Submit"))
        {
            StartCoroutine(ChangeLevel());
        }

        // If the current level is not the main menu...
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                //StartCoroutine(ChangeLevel());
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

    // Function that increases the players anxiety level depending on the bool parameter
    public void IncreaseAnxiety (bool inRange)
    {
        float increaseAmount = 0.5f;
        float decreaseAmount = 0.25f;

        if (inRange)
        {
            anxiety = Mathf.Lerp(anxiety, 1, increaseAmount * Time.deltaTime);
        }
        else
        {
            anxiety = Mathf.Lerp(anxiety, 0, decreaseAmount * Time.deltaTime);
        }

        anxiety = Mathf.Clamp01(anxiety);
    }

    IEnumerator ChangeLevel ()
    {
        float fadeTime = GetComponent<SceneFadeScript>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
