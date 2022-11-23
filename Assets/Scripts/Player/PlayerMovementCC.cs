
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

    [Header("References")]
    public Camera playerCam;
    private CharacterController player;

    [Header("Movement")]
    private Vector3 finalVelocity = Vector3.zero;
    private Vector3 lastDirectionRecorded = Vector3.zero;
    [SerializeField] private float gravity;
    [SerializeField] private float friction;
    
    [SerializeField] private MovementSettings m_GroundSettings = new MovementSettings(7, 14, 18);
    [SerializeField] private MovementSettings m_AirSettings = new MovementSettings(9, 8, 5);
    [SerializeField] private MovementSettings m_StrafeSettings = new MovementSettings(1, 50, 50);

    [Header("Jump")]
    [SerializeField] private float jumpForce;
    private bool validJump;
    [SerializeField] private float validateJump_offsetY;
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

                // Keep ground speed on air
                m_AirSettings.CurrentSpeed = m_GroundSettings.CurrentSpeed;
            }
            else
            {
                // On Ground
                finalVelocity.y = desiredMovement.y * gravity * Time.deltaTime;

                if (desiredMovement.x != 0 || desiredMovement.z != 0) // If there's input movement
                {
                    // Calculate ground speed
                    if (m_GroundSettings.CurrentSpeed <= m_GroundSettings.MaxSpeed)
                    {
                        m_GroundSettings.CurrentSpeed += m_GroundSettings.Acceleration * Time.deltaTime;
                    }
                    else if (m_GroundSettings.CurrentSpeed > m_GroundSettings.MaxSpeed)
                    {
                        m_GroundSettings.CurrentSpeed = m_GroundSettings.MaxSpeed;
                    }

                    // Record last direction while input != 0
                    lastDirectionRecorded.x = desiredMovement.x;
                    lastDirectionRecorded.z = desiredMovement.z;

                    // XZ movement
                    finalVelocity.x = desiredMovement.x * m_GroundSettings.CurrentSpeed;
                    finalVelocity.z = desiredMovement.z * m_GroundSettings.CurrentSpeed;
                }
                else if (desiredMovement.x == 0 && desiredMovement.z == 0 && m_GroundSettings.CurrentSpeed > 0)  // If not input movement
                {
                    // Friction
                    m_GroundSettings.CurrentSpeed -= m_GroundSettings.Deceleration * Time.deltaTime;

                    // XZ movement
                    finalVelocity.x = lastDirectionRecorded.x * m_GroundSettings.CurrentSpeed;
                    finalVelocity.z = lastDirectionRecorded.z * m_GroundSettings.CurrentSpeed;
                }

            }
        }
        else
        {
            // Calculate air speed
            if (m_AirSettings.CurrentSpeed <= m_AirSettings.MaxSpeed)
            {
                m_AirSettings.CurrentSpeed += m_AirSettings.Acceleration * Time.deltaTime;
            }
            else if (m_AirSettings.CurrentSpeed > m_AirSettings.MaxSpeed)
            {
                m_AirSettings.CurrentSpeed = m_AirSettings.MaxSpeed;
            }

            // Gravity
            finalVelocity.y += desiredMovement.y * gravity * Time.deltaTime;

            // Raycasts to calculate ground and validate jump
            Vector3 playerFeet = transform.position + new Vector3(0, -1f, 0);

            if (Physics.Linecast(playerFeet + new Vector3(0.2f, 0, 0), playerFeet + new Vector3(0.2f, validateJump_offsetY, 0)) && Manager_Input._INPUT_MANAGER.GetJumpButtonDown())
            {
                validJump = true;
            }

            if (Physics.Linecast(playerFeet + new Vector3(-0.2f, 0, 0), playerFeet + new Vector3(-0.2f, validateJump_offsetY, 0)) && Manager_Input._INPUT_MANAGER.GetJumpButtonDown())
            {
                validJump = true;
            }
            //Debug.DrawLine(playerFeet + new Vector3(0.2f, 0, 0), playerFeet + new Vector3(0.2f, offsetY, 0));
            //Debug.DrawLine(playerFeet + new Vector3(-0.2f, 0, 0), playerFeet + new Vector3(-0.2f, offsetY, 0));

            // XZ movement
            finalVelocity.x = desiredMovement.x * m_AirSettings.CurrentSpeed;
            finalVelocity.z = desiredMovement.z * m_AirSettings.CurrentSpeed;
        }


        // Physically move and rotate player
        player.Move(finalVelocity * Time.deltaTime);
        //transform.rotation = Quaternion.Euler(orientation * Time.deltaTime);



        //Debug.Log("vel z: " + finalVelocity.z + " vel x: " + finalVelocity.x);
        Debug.Log("vel z: " + finalVelocity.z + " vel x: " + finalVelocity.x + " vel y: " + finalVelocity.y);
        //Debug.Log("Velocity XZ: " + Vector3.SqrMagnitude(new Vector3(player.velocity.x, 0, player.velocity.z)));
    }

    private void OnGUI()
    {
        GUI.TextArea(new Rect(600, 10, 120, 20), "Speed XZ  " + new Vector3(finalVelocity.x, 0, finalVelocity.z).sqrMagnitude.ToString());
    }
    
}
