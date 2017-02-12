using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class CameraScript : MonoBehaviour
{
    GameController gameController;

    Vector2 mouseLook;
    Vector2 smoothV;
    public float defaultSensitivity;
    public float sensitivityMult;
    public float smoothing;

    public float lookDownSpeedCoEf = 0.5f;
    public float lookDownSpeed;
    public float maxLookDownAngle = -45f;

    GameObject player;

    public float grayScaleAmount;

    // Use this for initialization
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        player = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        float sensitivity = (defaultSensitivity - gameController.anxiety) * sensitivityMult;
        lookDownSpeed = gameController.anxiety * lookDownSpeedCoEf;

        Vector2 md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
        smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);

        /*if (mouseLook.y >= maxLookDownAngle)
        {
            smoothV.y -= lookDownSpeed;
        }*/

        mouseLook += smoothV * Time.deltaTime;

        transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        player.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, player.transform.up);

        GetComponent<Grayscale>().effectAmount = gameController.anxiety;
    }
}
