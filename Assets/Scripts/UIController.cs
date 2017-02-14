using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Image pauseMenu;
    public bool isImageActive = false;
    public Texture interactTexture;

    float w = 100;
    float h = 100;

    private void OnGUI()
    {
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
