
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

    [System.Serializable]
    private class MovementSettings
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
    private Rigidbody rb;

    [Header("Movement")]
    private Vector3 desiredMovement = Vector3.zero;
    private Vector3 finalVelocity = Vector3.zero;
    private Vector3 lastDirectionRecorded = Vector3.zero;
    [SerializeField] private float gravity;
    [SerializeField] private float friction;

    [SerializeField] private MovementSettings m_GroundSettings = new MovementSettings(7, 14, 18);
    [SerializeField] private MovementSettings m_AirSettings = new MovementSettings(7, 8, 5);
    [SerializeField] private MovementSettings m_StrafeSettings = new MovementSettings(1, 50, 50);

    [Header("Jump")]
    [SerializeField] private float jumpForce;
    private bool jump;
    private bool validJump;
    [SerializeField] private float validateJump_offsetY;

    


    // Use this for initialization
    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        // Get input
        desiredMovement = new Vector3(Manager_Input._INPUT_MANAGER.GetLeftAxis().x, 0, Manager_Input._INPUT_MANAGER.GetLeftAxis().y);
        //float directionX = Manager_Input._INPUT_MANAGER.GetLeftAxis().x;
        //float directionY = Manager_Input._INPUT_MANAGER.GetLeftAxis().y;
        //Vector3 directionXZ = direction * playerCam.transform.forward;
        desiredMovement.Normalize();
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

        if (Manager_Input._INPUT_MANAGER.GetJumpButtonDown() || validJump) 
        {
            jump = true;
            Debug.Log("JUMP");
        }


    }

    private void FixedUpdate()
    {

        Vector3 newPosition = transform.position + (m_GroundSettings.CurrentSpeed * Time.deltaTime * (playerCam.transform.forward * desiredMovement.z + playerCam.transform.right * desiredMovement.x));
        rb.MovePosition(newPosition);
        //Debug.Log(newPosition.x + "  " + newPosition.y + "  " + newPosition.z);

        if (jump) {
            jump = false;
            rb.AddForce(new Vector3(0, jumpForce * Time.deltaTime, 0), ForceMode.Force);
        }
    }

}
