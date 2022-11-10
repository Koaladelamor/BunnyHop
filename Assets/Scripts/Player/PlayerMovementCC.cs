
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using static UnityEditor.PlayerSettings;
using Debug = UnityEngine.Debug;

public class PlayerMovementCC : MonoBehaviour
{

    [Space]

    private Vector3 finalVelocity = Vector3.zero;

    [System.Serializable]
    public class MovementSettings
    {
        public float MaxSpeed;
        public float CurrentSpeed;
        public float Acceleration;
        public float Deceleration;

        public MovementSettings(float maxSpeed, float accel, float decel)
        {
            MaxSpeed = maxSpeed;
            CurrentSpeed = 0;
            Acceleration = accel;
            Deceleration = decel;
        }
    }

    public Camera playerCam;
    private CharacterController player;

    [Header("Movement")]

    [SerializeField] private float gravity;
    [SerializeField] private float friction;
    [SerializeField] private float jumpForce;
    [SerializeField] private MovementSettings m_GroundSettings = new MovementSettings(7, 14, 10);
    [SerializeField] private MovementSettings m_AirSettings = new MovementSettings(7, 2, 2);
    [SerializeField] private MovementSettings m_StrafeSettings = new MovementSettings(1, 50, 50);
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
        Vector3 desiredMovement = inputMovement.z * playerCam.transform.forward + inputMovement.x * playerCam.transform.right;
        Vector3 orientation = Quaternion.Euler(0f, playerCam.transform.eulerAngles.y, 0f) * new Vector3(desiredMovement.x, 0f, desiredMovement.z);
        //Debug.Log("Direction: " + directionCameraRelative.x + "  " + directionCameraRelative.z);
        //Debug.Log("Input: " + inputMovement.x + "  " + inputMovement.y + "  " + inputMovement.z);

        // Y direction
        desiredMovement.y = -1f;

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
                finalVelocity.y = desiredMovement.y * gravity * Time.deltaTime;

                //Friction: Needs to skip 1 frame on landing
                if (desiredMovement.x == 0.0f && desiredMovement.z == 0.0f)
                {
                    if (m_GroundSettings.CurrentSpeed >= 0)
                    {
                        m_GroundSettings.CurrentSpeed -= Time.deltaTime * 2;
                    }
                }
                // Calculate ground speed
                else if (desiredMovement.z != 0.0f)
                {
                    if (m_GroundSettings.CurrentSpeed <= m_GroundSettings.MaxSpeed)
                    {
                        m_GroundSettings.CurrentSpeed += m_GroundSettings.Acceleration * Time.deltaTime;
                    }
                    else if (m_GroundSettings.CurrentSpeed > m_GroundSettings.MaxSpeed)
                    {
                        m_GroundSettings.CurrentSpeed = m_GroundSettings.MaxSpeed;
                    }
                }

                // XZ movement
                finalVelocity.x = desiredMovement.x * m_GroundSettings.CurrentSpeed;
                finalVelocity.z = desiredMovement.z * m_GroundSettings.CurrentSpeed;

            }
        }
        else
        {
            // Air Velocity

            // Gravity
            finalVelocity.y += desiredMovement.y * gravity * Time.deltaTime;

            // Raycasts to calculate ground and validate jump
            Vector3 playerFeet = transform.position + new Vector3(0, -1f, 0);

            if (Physics.Linecast(playerFeet + new Vector3(0.2f, 0, 0), playerFeet + new Vector3(0.2f, offsetY, 0)) && Manager_Input._INPUT_MANAGER.GetJumpButtonDown())
            {
                validJump = true;
            }

            if (Physics.Linecast(playerFeet + new Vector3(-0.2f, 0, 0), playerFeet + new Vector3(-0.2f, offsetY, 0)) && Manager_Input._INPUT_MANAGER.GetJumpButtonDown())
            {
                validJump = true;
            }
            //Debug.DrawLine(playerFeet + new Vector3(0.2f, 0, 0), playerFeet + new Vector3(0.2f, offsetY, 0));
            //Debug.DrawLine(playerFeet + new Vector3(-0.2f, 0, 0), playerFeet + new Vector3(-0.2f, offsetY, 0));

            // XZ movement
            finalVelocity.x = desiredMovement.x * m_AirSettings.CurrentSpeed;
            finalVelocity.z = desiredMovement.z * m_AirSettings.CurrentSpeed;
        }

        // Physically move player
        player.Move(finalVelocity * Time.deltaTime);
        transform.rotation = Quaternion.LookRotation(orientation * Time.deltaTime);

        //Debug.Log("vel z: " + finalVelocity.z + " vel x: " + finalVelocity.x);
        Debug.Log("vel z: " + finalVelocity.z + " vel x: " + finalVelocity.x + " vel y: " + finalVelocity.y);
        //Debug.Log("Velocity XZ: " + Vector3.SqrMagnitude(new Vector3(player.velocity.x, 0, player.velocity.z)));
    }

    private void ApplyFriction(float t)
    {
        Vector3 vec = finalVelocity; // Equivalent to: VectorCopy();
        float speed;
        float newspeed;
        float control;
        float drop;

        vec.y = 0.0f;
        speed = vec.magnitude;
        drop = 0.0f;

        /* Only if the player is on the ground then apply friction */
        if (player.isGrounded)
        {
            control = speed < m_GroundSettings.Deceleration ? m_GroundSettings.Deceleration : speed;
            drop = control * friction * Time.deltaTime * t;
        }

        newspeed = speed - drop;
        friction = newspeed;
        if (newspeed < 0)
            newspeed = 0;
        if (speed > 0)
            newspeed /= speed;

        finalVelocity.x *= newspeed;
        finalVelocity.z *= newspeed;
    }


}
