using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    LevelController levelController;
    public Image pauseMenu;
    public Image progressBar;
    public bool isImageActive = false;
    public Texture interactTexture;
    bool isCreditsActive = false;
    public Text creditsText;
    public Image creditsImage;

    float w = 5;
    float h = 5;

    void Start()
    {
        levelController = GameObject.FindGameObjectWithTag("LevelController").GetComponent<LevelController>();

        if (levelController.levelNum == 0)
        {
            creditsText.enabled = false;
            creditsImage.enabled = false;
        }
    }

    void Update()
    {
        if (levelController.levelNum == 0)
        {
            if (Input.GetButtonDown("Alternate"))
            {
                if (!isCreditsActive)
                {
                    creditsText.enabled = true;
                    creditsImage.enabled = true;
                    isCreditsActive = true;
                }
                else
                {
                    creditsText.enabled = false;
                    creditsImage.enabled = false;
                    isCreditsActive = false;
                }
            }
        }
    }

    private void OnGUI()
    {
        // Draw the interact texture in the middle of the screen when isImageActive is true
        if (isImageActive)
        {
            GUI.DrawTexture(new Rect((Screen.width - w) / 2, (Screen.height - h) / 2, w, h), interactTexture);
        }
    }

    public void PauseMenuImage (float alpha)
    {
        pauseMenu.color = new Color(1, 1, 1, alpha);
    }

}
