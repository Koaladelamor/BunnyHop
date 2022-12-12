using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterCamera : MonoBehaviour
{
    [SerializeField] private bool canRotate = true;

    [SerializeField] private float mouseX;
    [SerializeField] private float mouseY;
    [SerializeField] private Vector2 mouseDelta = Vector2.zero;

    [SerializeField] private Transform playerCamera;

    [SerializeField] private float rotationX;
    [SerializeField] private float rotationY;

    [SerializeField] private float sensX;
    [SerializeField] private float sensY;

    [SerializeField] private float cameraLerp;
    [SerializeField] private float smoothFactor;
    [SerializeField] private float smoothing;

    private Vector2 rightAxisInput;



    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    /*private void Update()
    {
        
    }*/

    private void LateUpdate()
    {
        if (!canRotate) { return; }

        rightAxisInput = InputManager.Instance.GetRightAxis();
        //mouseX = rightAxisInput.x * sensY * Time.deltaTime;
        //mouseY = rightAxisInput.y * sensX * Time.deltaTime;

        mouseDelta = new Vector2(rightAxisInput.y, rightAxisInput.x);
        //mouseDir.Normalize();

        mouseDelta = Vector2.Scale(mouseDelta, new Vector2(sensX * smoothFactor, sensY * smoothFactor));

        //Debug.Log("mouseX: " + mouseX + " mouseY: " + mouseY);
        //Debug.Log("euler x: " + transform.eulerAngles.x);

        mouseDelta = Vector2.Lerp(mouseDelta, new Vector2(mouseDelta.x * smoothing, mouseDelta.y * smoothing), cameraLerp * Time.deltaTime);

        rotationX -= mouseDelta.x;
        rotationY += mouseDelta.y;

        rotationX = Mathf.Clamp(rotationX, -85f, 85f);


        //transform.rotation = Quaternion.Euler(rotationX, rotationY, 0);
        playerCamera.transform.localEulerAngles = new Vector3(rotationX, rotationY, 0);
        transform.localEulerAngles = new Vector3(0, rotationY, 0);
        //player.rotation = Quaternion.Euler(0, rotationY, 0);
        //transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0);
        //transform.eulerAngles = new Vector3(rotationX, rotationY, 0);
        //player.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, rotationY, 0), cameraLerp * Time.deltaTime);

    }

    public void SetCanRotate(bool _canRotate) { canRotate = _canRotate; }

}
