using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    PlayerControls inputActions;
    new Rigidbody rigidbody;
    Vector2 movementInput;

    [Header("Stats")]
    [SerializeField]
    float movementSpeed = 5;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 vector = new Vector3(movementInput.x * movementSpeed, 0, movementInput.y * movementSpeed);
        rigidbody.velocity = vector;
    }

    private void OnEnable()
    {
        if(inputActions == null)
        {
            inputActions = new PlayerControls();
            inputActions.PlayerMovement.MovementAction.performed += o => movementInput = o.ReadValue<Vector2>();
        }
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }
}
