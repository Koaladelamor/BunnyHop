
using Q3Movement;
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
    private class MovementSettings
    {
        public float MaxSpeed;
        public float Acceleration;
        public float Deceleration;

        public MovementSettings(float maxSpeed, float accel, float decel)
        {
            MaxSpeed = maxSpeed;
            Acceleration = accel;
            Deceleration = decel;
        }
    }

    [Header("References")]
    public Camera playerCam;
    private CharacterController player;
    private MouseLook m_mouseLook = new MouseLook();

    [Header("Movement")]
    private Vector3 finalVelocity = Vector3.zero;
    //private Vector3 lastDirectionRecorded = Vector3.zero;
    [SerializeField] private float gravity;
    [SerializeField] private float friction;
    [SerializeField] private float airControl;

    [SerializeField] private MovementSettings m_GroundSettings = new MovementSettings(7, 14, 18);
    [SerializeField] private MovementSettings m_AirSettings = new MovementSettings(7, 8, 5);
    [SerializeField] private MovementSettings m_StrafeSettings = new MovementSettings(1, 50, 50);
    //private Vector3 inputMovement = Vector3.zero;

    [Header("Jump")]
    [SerializeField] private float jumpForce;
    [SerializeField] private bool jumpQueued;
    [SerializeField] private float validateJump_offsetY;
    // Use this for initialization
    void Start()
    {
        jumpQueued = false;
        player = this.GetComponent<CharacterController>();
        //m_mouseLook.Init(transform, playerCam.transform);
    }

    // Update is called once per frame
    void Update()
    {
        // Get input
        Vector3 inputMovement = new Vector3(Manager_Input._INPUT_MANAGER.GetLeftAxis().x, 0, Manager_Input._INPUT_MANAGER.GetLeftAxis().y);
        inputMovement.Normalize();

        //Vector3 desiredMovement = Quaternion.Euler(0f, playerCam.transform.eulerAngles.y, 0f) * new Vector3(inputMovement.x, 0, inputMovement.z);
        //desiredMovement.Normalize();



        //Vector3 orientation = Quaternion.Euler(0f, playerCam.transform.eulerAngles.y, 0f) * new Vector3(desiredMovement.x, 0f, desiredMovement.z);
        //Debug.Log("Direction: " + directionCameraRelative.x + "  " + directionCameraRelative.z);
        //Debug.Log("Input: " + inputMovement.x + "  " + inputMovement.y + "  " + inputMovement.z);

        // Y direction
        //desiredMovement.y = -1f;

        

        if (player.isGrounded)
        {
            // Jump
            if (Manager_Input._INPUT_MANAGER.GetJumpButtonDown() || jumpQueued)
            {
                jumpQueued = false;
                finalVelocity.y = jumpForce;
            }
            else {
                GroundMovement(inputMovement);
            }
        }
        else
        {
            AirMovement(inputMovement);
            JumpQueue();
        }

        // Physically move and rotate player
        //transform.rotation = Quaternion.Euler(0, playerCam.transform.eulerAngles.y, 0);
        //m_mouseLook.LookRotation(transform, playerCam.transform);
        player.Move(finalVelocity * Time.deltaTime);


        /* VELOCITY DEBUG */
        //Debug.Log("vel z: " + finalVelocity.z + " vel x: " + finalVelocity.x);
        //Debug.Log("vel z: " + finalVelocity.z + " vel x: " + finalVelocity.x + " vel y: " + finalVelocity.y);
        //Debug.Log("Velocity XZ: " + Vector3.SqrMagnitude(new Vector3(player.velocity.x, 0, player.velocity.z)));
    }

    private void GroundMovement(Vector3 direction) 
    {
        // Friction
        if (player.isGrounded && !jumpQueued) {
            ApplyFriction(1.0f);
        }
        else if (!player.isGrounded || jumpQueued) {
            ApplyFriction(0.0f);
        }

        // Calculate direction
        Vector3 wishDir = new Vector3(direction.x, 0, direction.z);
        wishDir = transform.TransformDirection(wishDir);
        wishDir.Normalize();

        float wishSpeed = wishDir.magnitude;
        wishSpeed *= m_GroundSettings.MaxSpeed;
        //Debug.Log("wishSpeed: " + wishSpeed);

        Accelerate(wishDir, wishSpeed, m_GroundSettings.Acceleration);

        // Reset gravity velocity
        finalVelocity.y = direction.y * gravity * Time.deltaTime;
    }

    private void AirMovement(Vector3 direction)
    {
        // Calculate direction and speed
        Vector3 wishDir = new Vector3(direction.x, 0, direction.z);
        wishDir = transform.TransformDirection(wishDir);
        wishDir.Normalize();

        float wishSpeed = wishDir.magnitude;
        wishSpeed *= m_AirSettings.MaxSpeed;

        float acceleration = m_AirSettings.Acceleration;

        /*float currentSpeed = Vector3.Dot(finalVelocity, wishDir);
        if (currentSpeed < 0)
        {
            acceleration = m_AirSettings.Deceleration;
        }*/

        //Debug.Log(wishDir.x + " " + wishDir.y + " " + wishDir.z);
        //Debug.Log(currentSpeed);

        // Strafe 
        if (direction.z == 0 && direction.x != 0)
        {
            if (wishSpeed > m_StrafeSettings.MaxSpeed)
            {
                wishSpeed = m_StrafeSettings.MaxSpeed;
            }
            acceleration = m_StrafeSettings.Acceleration;
        }

        Accelerate(wishDir, wishSpeed, acceleration);

        // Gravity
        finalVelocity.y += -gravity * Time.deltaTime;
    }

    private void Accelerate(Vector3 targetDir, float targetSpeed, float acceleration)
    {
        float currentspeed = Vector3.Dot(finalVelocity, targetDir);

        float addspeed = targetSpeed - currentspeed;



        
        if (addspeed <= 0)
        {
            return;
        }

        float accelspeed = acceleration * Time.deltaTime * targetSpeed;
        //Debug.Log("addspeed : " + addspeed);
        
        if (accelspeed > addspeed)
        {
            accelspeed = addspeed;
        }

        Vector3 velocity = finalVelocity;
        velocity.y = 0;
        float currentVelocity = velocity.magnitude;
        Debug.Log("currentVel : " + currentVelocity);
        //Debug.Log("accelspeed : " + accelspeed);
        finalVelocity.x += accelspeed * targetDir.x;
        finalVelocity.z += accelspeed * targetDir.z;

        Debug.Log("finalVel.y : " + finalVelocity.y);

        Vector3 clampedVelocity = Mathf.Clamp(currentVelocity, 0, 250) * transform.forward;

        finalVelocity = new Vector3(clampedVelocity.x, finalVelocity.magnitude - currentVelocity, clampedVelocity.z);
        

    }

    private void ApplyFriction(float t)
    {
        Vector3 velocity = finalVelocity;
        velocity.y = 0;
        float currentSpeed = velocity.magnitude;

        if (currentSpeed != 0)
        {
            float drop = currentSpeed * friction * Time.deltaTime * t;
            finalVelocity *= Mathf.Max(currentSpeed - drop, 0) / currentSpeed; // Scale the velocity based on friction.
        }

        //float newSpeed = currentSpeed - drop;
        /*
        if (newSpeed < 0) { newSpeed = 0; }
        friction = newSpeed;
        if (currentSpeed > 0) { newSpeed /= currentSpeed; }
        //Debug.Log(newSpeed);

        finalVelocity.x *= newSpeed;
        // playerVelocity.y *= newSpeed;
        finalVelocity.z *= newSpeed;
        */
    }

    private void JumpQueue() {
        // Raycasts to calculate ground and validate jump
        Vector3 playerFeet = transform.position + new Vector3(0, -1f, 0);

        if (Physics.Linecast(playerFeet + new Vector3(0.2f, 0, 0), playerFeet + new Vector3(0.2f, validateJump_offsetY, 0)) && Manager_Input._INPUT_MANAGER.GetJumpButtonDown())
        {
            jumpQueued = true;
        }

        if (Physics.Linecast(playerFeet + new Vector3(-0.2f, 0, 0), playerFeet + new Vector3(-0.2f, validateJump_offsetY, 0)) && Manager_Input._INPUT_MANAGER.GetJumpButtonDown())
        {
            jumpQueued = true;
        }
        //Debug.DrawLine(playerFeet + new Vector3(0.2f, 0, 0), playerFeet + new Vector3(0.2f, offsetY, 0));
        //Debug.DrawLine(playerFeet + new Vector3(-0.2f, 0, 0), playerFeet + new Vector3(-0.2f, offsetY, 0));
    }

    private void OnGUI()
    {
        GUI.TextArea(new Rect(600, 75, 130, 20), "Speed XZ  " + new Vector3(finalVelocity.x, 0, finalVelocity.z).sqrMagnitude.ToString());
    }
    
}
