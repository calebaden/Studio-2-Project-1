using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class CameraScript : MonoBehaviour
{
    LevelController levelController;

    public float slerpSpeed;

    Vector2 _mouseAbsolute;
    Vector2 _smoothMouse;

    public Vector2 mouseLook;
    Vector2 smoothV;
    public float sensitivity;
    public float smoothing;

    GameObject player;
    
    // Use this for initialization
    void Start()
    {
        levelController = GameObject.FindGameObjectWithTag("LevelController").GetComponent<LevelController>();
        player = transform.parent.gameObject;
        mouseLook = new Vector2(0, 0);
    }

    private void LateUpdate()
    {
        if (player.GetComponent<PlayerController>().canMove)
        {
            MouseMovement();
        }
        else
        {
            CameraLock();
        }
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Grayscale>().effectAmount = levelController.anxiety;
        GetComponent<MotionBlur>().blurAmount = levelController.anxiety;
        GetComponent<MotionBlur>().blurAmount = Mathf.Clamp01(GetComponent<MotionBlur>().blurAmount);
        GetComponent<Vortex>().angle = levelController.anxiety * 100;
        GetComponent<Vortex>().angle = Mathf.Clamp(GetComponent<Vortex>().angle, 0, 100);
    }

    void MouseMovement ()
    {
        // Ensure the cursor is always locked when set
        Cursor.lockState = CursorLockMode.Locked;

        var md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
        smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
        mouseLook += smoothV * Time.deltaTime;

        transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        player.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, player.transform.up);
    }

    // Function that locks the players view to the target object
    void CameraLock ()
    {
        Quaternion targetRotation = Quaternion.LookRotation(player.GetComponent<PlayerController>().lookTarget.transform.position - transform.position);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, slerpSpeed * Time.deltaTime);
    }
}
