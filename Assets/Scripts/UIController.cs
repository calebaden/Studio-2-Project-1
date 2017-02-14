using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Image interact;
    public bool isImageActive = false;
    public Texture interactTexture;

    private void OnGUI()
    {
        if (isImageActive)
        {
            GUI.DrawTexture(new Rect(300, 200, 50, 50), interactTexture);
        }
    }
}
