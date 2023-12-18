using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    InputHandler inputHandler;
    Animator anim;
    CameraHander cameraHander;
    PlayerLocomotion playerLocomotion;

    public bool isInteracting;
    public bool isSprinting;

    private void Awake()
    {
        //cameraHander = CameraHander.singleton;
        cameraHander = FindObjectOfType<CameraHander>();
    }


    // Start is called before the first frame update
    void Start()
    {
        inputHandler = GetComponent<InputHandler>();
        anim = GetComponentInChildren<Animator>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
    }

    // Update is called once per frame
    void Update()
    {
        float delta = Time.deltaTime;
        isInteracting = anim.GetBool("isInteracting");

        inputHandler.TickInput(delta);
        playerLocomotion.HandleMovement(delta);
        playerLocomotion.HandleRollingAndSprinting(delta);
    }

    private void FixedUpdate()
    {
        float delta = Time.deltaTime;

        if (cameraHander != null)
        {
            cameraHander.FollowTarget(delta);
            cameraHander.HandCameraRotation(delta, inputHandler.mouseX, inputHandler.mouseY);
        }
    }

    private void LateUpdate()
    {
        inputHandler.rollFlag = false;
        inputHandler.sprintFlag = false;
    }
}
