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
    public bool a_Input;
    public bool rb_Input;
    public bool rt_Input;
    public bool jump_Input;
    public bool inventory_Input;
    public bool lockOn_Input;


    public bool d_Pad_Up;
    public bool d_Pad_Down;
    public bool d_Pad_Left;
    public bool d_Pad_Right;

    public bool rollFlag;
    public bool sprintFlag;
    public bool comboFlag;
    public bool lockOnFlag;
    public bool inventoryFlag;
    public float rollInputTimer;

    PlayerControls inputActions;
    CameraHander cameraHander;
    PlayerAttacker playerAttacker;
    PlayerInventory playerInventory;
    PlayerManager playerManager;
    UIManager uiManager;

    Vector2 movementInput;
    Vector2 cameraInput;


    private void Awake()
    {
        playerAttacker = GetComponent<PlayerAttacker>();
        playerInventory = GetComponent<PlayerInventory>();
        playerManager = GetComponent<PlayerManager>();
        uiManager = FindObjectOfType<UIManager>();
        cameraHander = FindObjectOfType<CameraHander>();
    }


    private void OnEnable()
    {
        if (inputActions == null)
        {
            inputActions = new PlayerControls();
            inputActions.PlayerMovement.MovementAction.performed += outputActions => movementInput = outputActions.ReadValue<Vector2>();
            inputActions.PlayerMovement.Camera.performed += outputActions => cameraInput = outputActions.ReadValue<Vector2>();
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
        HandleAttackInput(delta);
        HandleQuickSlotsInput();
        HandleInteractingButtonInput();
        HandleJumpInput();
        HandleInventoryInput();
        HandleLockOnInput();
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
        b_Input = inputActions.PlayerActions.Roll.IsPressed();

        if (b_Input)
        {
            rollInputTimer += delta;
            sprintFlag = true;
        }
        else
        {
            if(rollInputTimer > 0 && rollInputTimer < 0.5f)
            {
                sprintFlag = false;
                rollFlag = true;
            }

            rollInputTimer = 0;
        }
    }

    private void HandleAttackInput(float delta)
    {
        inputActions.PlayerActions.RB.performed += i => rb_Input = true;
        inputActions.PlayerActions.RT.performed += i => rt_Input = true;

        if(rb_Input)
        {
            if(playerManager.canDoCombo)
            {
                comboFlag = true;
                playerAttacker.HandleWeaponCombo(playerInventory.rightWeapon);
                comboFlag = false;
            }
            else
            {
                if (playerManager.isInteracting)
                    return;

                if (playerManager.canDoCombo)
                    return;

                playerAttacker.HandleLightAttack(playerInventory.rightWeapon);
            }
        }

        if (rt_Input)
        {
            playerAttacker.HandleHeavyAttack(playerInventory.rightWeapon);
        }
    }

    private void HandleQuickSlotsInput()
    {
        inputActions.PlayerquickSlots.DPadRight.performed += i => d_Pad_Right = true;
        inputActions.PlayerquickSlots.DPadLeft.performed += i => d_Pad_Left = true;

        if(d_Pad_Right)
        {
            playerInventory.ChangeRightWeapon();
        }else if(d_Pad_Left)
        {
            playerInventory.ChangeLeftWeapon();
        }
    }

    private void HandleInteractingButtonInput()
    {
        inputActions.PlayerActions.A.performed += i => a_Input = true;
    }

    private void HandleJumpInput()
    {
        inputActions.PlayerActions.Jump.performed += ii => jump_Input = true;
    }

    private void HandleInventoryInput()
    {
        inputActions.PlayerActions.Inventory.performed += ii => inventory_Input = true;

        if(inventory_Input)
        {
            inventoryFlag = !inventoryFlag;

            if(inventoryFlag)
            {
                uiManager.OpenSelectWindow();
                uiManager.UpdateUI();
                uiManager.hudWindow.SetActive(false);
            }
            else
            {
                uiManager.CloseSelectWindow();
                uiManager.CloseAllInventoryWindows();
                uiManager.hudWindow.SetActive(true);
            }
        }
    }

    private void HandleLockOnInput()
    {
        inputActions.PlayerActions.LockOn.performed += i => lockOn_Input = true;

        if(lockOn_Input && lockOnFlag==false)
        {
            cameraHander.ClearLockOnTargets();
            lockOn_Input = false;
            cameraHander.HandleLockOn();

            if(cameraHander.nearestLockOnTarget != null)
            {
                lockOnFlag = true;
                cameraHander.currentLockOnTarget = cameraHander.nearestLockOnTarget;
            }
        }else if(lockOn_Input && lockOnFlag)
        {
            lockOn_Input = false;
            lockOnFlag = false;
            cameraHander.ClearLockOnTargets();
        }
    }
}
