using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    UIController uiController;
    PlayerController playerController;
    public List<GameObject> friends;

    public GameObject endPosition;
    public int levelNum;
    private bool isPaused;

    public float anxiety;
    public float anxiety2;
    public float anxiety3;
    float lerpSpeed = 2f;

    // Use this for initialization
    void Start ()
    {
        uiController = GameObject.FindGameObjectWithTag("UIController").GetComponent<UIController>();
        Cursor.lockState = CursorLockMode.Locked;

        if (levelNum != 0)
        {
            playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        anxiety = Mathf.Lerp(anxiety, anxiety2 + anxiety3, lerpSpeed * Time.deltaTime);

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
            if (anxiety <= -0.5f && playerController.canMove)
            {
                playerController.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionY;
                endPosition.SetActive(true);
                playerController.canMove = false;
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
    IEnumerator ChangeLevel(int buildIndex)
    {
        float fadeTime = GetComponent<SceneFadeScript>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(buildIndex);
    }
}
