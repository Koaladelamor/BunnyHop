using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class InputManager : MonoBehaviour
{
    #region Variables

    private Vector2 leftAxisValue = Vector2.zero;
    private Vector2 rightAxisValue = Vector2.zero;

    private float timeSinceJumpPressed = 0f;
    private bool jumpReleased = true;

    private float timeSinceCheckpointSavePressed = 0f;
    private float timeSinceCheckpointLoadPressed = 0f;

    private float timeSinceMenuPressed = 0f;

    public PlayerInputActions playerInputs;

    public static InputManager Instance;

    #endregion

    #region Main Methods
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else 
        {
            playerInputs = new PlayerInputActions();
            //playerInputs.Character.Enable();
            

            playerInputs.Character.Jump.performed += JumpButtonPressed;
            playerInputs.Character.Jump.canceled += JumpButtonReleased;
            playerInputs.Character.Move.performed += LeftAxisUpdate;
            playerInputs.Character.Turn.performed += RightAxisUpdate;
            playerInputs.Character.Checkpoint_Save.performed += CheckpointSaveButtonPressed;
            playerInputs.Character.Checkpoint_Load.performed += CheckpointLoadButtonPressed;
            playerInputs.Character.Menu.performed += MenuButtonPressed;


            playerInputs.UI.Enable();


            Instance = this;
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
        timeSinceCheckpointLoadPressed += Time.deltaTime;
        timeSinceCheckpointSavePressed += Time.deltaTime;
        timeSinceMenuPressed += Time.deltaTime;

        InputSystem.Update();
    }
    #endregion

    #region Read Input
    private void JumpButtonPressed(InputAction.CallbackContext context)
    {
        //Debug.Log("Space Pressed");
        jumpReleased = false;
        timeSinceJumpPressed = 0f;
    }

    private void CheckpointSaveButtonPressed(InputAction.CallbackContext context)
    {
        //Debug.Log("Checkpoint Save Pressed");
        timeSinceCheckpointSavePressed = 0f;
    }

    private void CheckpointLoadButtonPressed(InputAction.CallbackContext context)
    {
        //Debug.Log("Checkpoint Load Pressed");
        timeSinceCheckpointLoadPressed = 0f;
    }

    private void MenuButtonPressed(InputAction.CallbackContext context)
    {
        //Debug.Log("Menu Pressed");
        timeSinceMenuPressed = 0f;
    }

    private void JumpButtonReleased(InputAction.CallbackContext context)
    {
        jumpReleased = true;
        //Debug.Log("Space Released");
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

    public bool GetJumpButtonReleased()
    {
        return jumpReleased;
    }

    public bool GetCheckpointSaveButtonDown()
    {
        return timeSinceCheckpointSavePressed == 0f;
    }

    public bool GetCheckpointLoadButtonDown()
    {
        return timeSinceCheckpointLoadPressed == 0f;
    }

    public bool GetMenuButtonDown()
    {
        return timeSinceMenuPressed == 0f;
    }
    #endregion

    public void EnablePlayerInput() {
        playerInputs.UI.Disable();
        playerInputs.Character.Enable();
    }

    public void EnableUIInput()
    {
        playerInputs.Character.Disable();
        playerInputs.UI.Enable();
    }

    public void DisableAllInputs()
    {
        playerInputs.Character.Disable();
        playerInputs.UI.Disable();
    }
}
