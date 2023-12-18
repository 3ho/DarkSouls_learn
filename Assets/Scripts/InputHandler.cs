using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public float horizontal;
    public float vertical;
    public float moveAmount;
    public float mouseX;
    public float mouseY;

    public bool b_Input;
    public bool rollFlag;

    PlayerControls inputActions;
    CameraHander cameraHander;

    Vector2 movementInput;
    Vector2 cameraInput;

    private void Awake()
    {
        //cameraHander = CameraHander.singleton;
        cameraHander = FindObjectOfType<CameraHander>();
    }

    private void FixedUpdate()
    {
        float delta = Time.deltaTime;

        if (cameraHander != null)
        {
            cameraHander.FollowTarget(delta);
            cameraHander.HandCameraRotation(delta, mouseX, mouseY);
        }
    }

    private void OnEnable()
    {
        if (inputActions == null)
        {
            inputActions = new PlayerControls();
            inputActions.PlayerMovement.MovementAction.performed += outputActions => movementInput = outputActions.ReadValue<Vector2>();
            inputActions.PlayerMovement.Camera.performed += outputActions => cameraInput = outputActions.ReadValue<Vector2>();

            //xiaorenping自增
            #region 
            inputActions.PlayerActions.Roll.started += outputActions => rollFlag = true;
            #endregion
        }
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    public void TickInput(float delta)
    {
        MoveInput(delta);
        HandleRollInput(delta);
    }

    public void MoveInput(float delta)
    {
        horizontal = movementInput.x;
        vertical = movementInput.y;
        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));

        mouseX = cameraInput.x;
        mouseY = cameraInput.y;
    }

    private void HandleRollInput(float delta)
    {
        b_Input = inputActions.PlayerActions.Roll.phase == UnityEngine.InputSystem.InputActionPhase.Started;

        if (b_Input)
        {
            rollFlag = true;
        }
    }
}
