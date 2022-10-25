
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using Debug = UnityEngine.Debug;

public class PlayerMovementCC : MonoBehaviour
{

    [Space]

    private Vector3 finalVelocity = Vector3.zero;

    public Camera playerCam;
    private CharacterController player;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float currentSpeed;
    [SerializeField] private float acceleration;
    [SerializeField] private float gravity;
    [SerializeField] private float jumpForce;
    private bool validJump;

    [SerializeField] private float offsetY;
    // Use this for initialization
    void Start()
    {
        validJump = false;
        player = this.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get input
        Vector3 inputMovement = new Vector3(Manager_Input._INPUT_MANAGER.GetLeftAxis().x, 0, Manager_Input._INPUT_MANAGER.GetLeftAxis().y);
        inputMovement.Normalize();

        //ALEX MOVEMENT
        Vector3 directionCameraRelative = inputMovement.z * playerCam.transform.forward + inputMovement.x * playerCam.transform.right;
        Vector3 orientation = Quaternion.Euler(0f, playerCam.transform.eulerAngles.y, 0f) * new Vector3(directionCameraRelative.x, 0f, directionCameraRelative.z);
        Debug.Log("Direction: " + directionCameraRelative.x + "  " + directionCameraRelative.z);


        // Y direction
        directionCameraRelative.y = -1f;

        if (player.isGrounded)
        {

            // Jump
            if (Manager_Input._INPUT_MANAGER.GetJumpButtonDown() || validJump)
            {
                validJump = false;
                finalVelocity.y = jumpForce;
            }
            else
            {
                // On Ground
                finalVelocity.y = directionCameraRelative.y * gravity * Time.deltaTime;

                //Friction
                if (directionCameraRelative.x == 0.0f && directionCameraRelative.z == 0.0f)
                {
                    if (currentSpeed > 0)
                    {
                        currentSpeed -= Time.deltaTime * 8;
                    }
                }
                // Calculate ground speed
                else if (directionCameraRelative.z != 0.0f)
                {
                    if (currentSpeed <= maxSpeed)
                    {
                        currentSpeed += acceleration * Time.deltaTime;
                    }
                    else if (currentSpeed > maxSpeed)
                    {
                        currentSpeed = maxSpeed;
                    }
                }

            }
        }
        else
        {
            // Strafe Jump


            // Gravity
            finalVelocity.y += directionCameraRelative.y * gravity * Time.deltaTime;

            // Raycasts to calculate ground and validate jump
            Vector3 playerFeet = transform.position + new Vector3(0, -1f, 0);

            //Debug.DrawLine(playerFeet + new Vector3(0.2f, 0, 0), playerFeet + new Vector3(0.2f, offsetY, 0));
            //Debug.DrawLine(playerFeet + new Vector3(-0.2f, 0, 0), playerFeet + new Vector3(-0.2f, offsetY, 0));

            if (Physics.Linecast(playerFeet + new Vector3(0.2f, 0, 0), playerFeet + new Vector3(0.2f, offsetY, 0)) && Manager_Input._INPUT_MANAGER.GetJumpButtonDown())
            {
                validJump = true;
            };

            if (Physics.Linecast(playerFeet + new Vector3(-0.2f, 0, 0), playerFeet + new Vector3(-0.2f, offsetY, 0)) && Manager_Input._INPUT_MANAGER.GetJumpButtonDown())
            {
                validJump = true;
            };

        }

        // XZ movement
        finalVelocity.x = directionCameraRelative.x * currentSpeed;
        finalVelocity.z = directionCameraRelative.z * currentSpeed;



        // Physically move player
        player.Move(finalVelocity * Time.deltaTime);
        transform.rotation = Quaternion.LookRotation(orientation * Time.deltaTime);

        //Debug.Log("vel z: " + finalVelocity.z + " vel x: " + finalVelocity.x);
        //Debug.Log("Velocity XZ: " + Vector3.SqrMagnitude(new Vector3(player.velocity.x, 0, player.velocity.z)));
    }


}
