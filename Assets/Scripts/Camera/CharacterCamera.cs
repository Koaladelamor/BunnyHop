using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCamera : MonoBehaviour
{

    [SerializeField] private float mouseX;
    [SerializeField] private float mouseY;

    [SerializeField] private Transform player;

    [SerializeField] private float rotationX;
    [SerializeField] private float rotationY;

    [SerializeField] private float sensX;
    [SerializeField] private float sensY;

    private Vector2 rightAxisInput;



    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        rightAxisInput = Manager_Input._INPUT_MANAGER.GetRightAxis();
    }

    private void LateUpdate()
    {
        mouseX = rightAxisInput.x * sensY * Time.deltaTime;
        mouseY = rightAxisInput.y * sensX * Time.deltaTime;

        //Debug.Log("mouseX: " + mouseX + " mouseY: " + mouseY);
        //Debug.Log("euler x: " + transform.eulerAngles.x);

        rotationX -= mouseY;
        rotationY += mouseX;

        rotationX = Mathf.Clamp(rotationX, -85f, 85f);


        transform.rotation = Quaternion.Euler(rotationX, rotationY, 0);
        //player.rotation = Quaternion.Euler(0, rotationY, 0);

    }

}
