using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Manager_Input : MonoBehaviour
{
    #region Variables
    private Vector2 leftAxisValue = Vector2.zero;
    private Vector2 rightAxisValue = Vector2.zero;
    private float timeSinceJumpPressed = 0f;

    public PlayerInputActions playerInputs;

    public static Manager_Input _INPUT_MANAGER;
    #endregion

    #region Main Methods
    private void Awake()
    {
        if (_INPUT_MANAGER != null && _INPUT_MANAGER != this)
        {
            Destroy(this);
        }
        else 
        {
            playerInputs = new PlayerInputActions();
            playerInputs.Character.Enable();

            playerInputs.Character.Jump.performed += JumpButtonPressed;
            playerInputs.Character.Move.performed += LeftAxisUpdate;
            playerInputs.Character.Turn.performed += RightAxisUpdate;

            _INPUT_MANAGER = this;
            DontDestroyOnLoad(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timeSinceJumpPressed += Time.deltaTime;

        InputSystem.Update();
    }
    #endregion

    #region Read Input
    private void JumpButtonPressed(InputAction.CallbackContext context)
    {
        //Debug.Log("Space Pressed");
        timeSinceJumpPressed = 0f;
    }

    private void LeftAxisUpdate(InputAction.CallbackContext context)
    {
        leftAxisValue = context.ReadValue<Vector2>(); 
        //Debug.Log("Magnitude: " + leftAxisValue.magnitude); 
        //Debug.Log("Normalize: " + leftAxisValue.normalized);
        //return leftAxisValue;
    }

    private void RightAxisUpdate(InputAction.CallbackContext context)
    {
        rightAxisValue = context.ReadValue<Vector2>();
        //Debug.Log("Magnitude: " + leftAxisValue.magnitude); 
        //Debug.Log("Normalize: " + leftAxisValue.normalized);
        //return leftAxisValue;
    }

    #endregion

    #region Getters
    public Vector2 GetLeftAxis() 
    {
        return leftAxisValue;
    }

    public Vector2 GetRightAxis()
    {
        return rightAxisValue;
    }

    public bool GetJumpButtonDown() 
    {
        return timeSinceJumpPressed == 0f;
    }
    #endregion
}
