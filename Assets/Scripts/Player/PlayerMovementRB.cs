
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using Debug = UnityEngine.Debug;

public class PlayerMovementRB : MonoBehaviour
{

    [Space]
    
    private Vector3 direction = Vector3.zero;

    public Camera playerCam;
    private Rigidbody rb;

    [SerializeField] private float maxSpeed;
    [SerializeField] private float currentSpeed;

    [SerializeField] private float acceleration;
    [SerializeField] private float desiredRotationSpeed;

    [SerializeField] private float jumpForce;
    [SerializeField] private bool jump;

    // Use this for initialization
    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        // Get input
        direction = new Vector3(Manager_Input._INPUT_MANAGER.GetLeftAxis().x, 0, Manager_Input._INPUT_MANAGER.GetLeftAxis().y);
        //float directionX = Manager_Input._INPUT_MANAGER.GetLeftAxis().x;
        //float directionY = Manager_Input._INPUT_MANAGER.GetLeftAxis().y;
        //Vector3 directionXZ = direction * playerCam.transform.forward;
        direction.Normalize();
        //Debug.Log(direction.x + "  " + direction.z);

        //ALEX MOVEMENT
        //Vector3 directionCameraRelative = Input.GetAxis("Vertical") * playerCam.transform.forward + Input.GetAxis("Horizontal") * playerCam.transform.right;
        //Vector3 directionAlex = Quaternion.Euler(0f, playerCam.transform.eulerAngles.y, 0f) * new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        /* Calculate direction, final velocity and current speed */

        /*if (direction.x == 0.0f && direction.z == 0.0f)
        {
            currentSpeed = 0f;
        }
        if (currentSpeed <= maxSpeed)
        {
            currentSpeed += acceleration * Time.deltaTime;
        }
        else {
            currentSpeed = maxSpeed;
        }
        */

        if (Manager_Input._INPUT_MANAGER.GetJumpButtonDown()) 
        {
            jump = true;
            Debug.Log("JUMP");
        }


    }

    private void FixedUpdate()
    {

        Vector3 newPosition = transform.position + (currentSpeed * Time.deltaTime * (playerCam.transform.forward * direction.z + playerCam.transform.right * direction.x));
        rb.MovePosition(newPosition);
        //Debug.Log(newPosition.x + "  " + newPosition.y + "  " + newPosition.z);

        if (jump) {
            jump = false;
            rb.AddForce(new Vector3(0, jumpForce * Time.deltaTime, 0), ForceMode.Force);
        }
    }

}
