﻿using UnityEngine;
using System.Collections;

/// <summary>
/// Third person character controller.
/// Processes player input and sends the input as "Move Vector" to the ThirdPersonMotor.
/// </summary>
public class ThirdPersonCharacterController : MonoBehaviour
{
    private static CharacterController characterController;
    private static ThirdPersonCharacterController instance;

    public float moveVectorDeadZone = 0.1f; // The character will move only if any of the "x" and "z" properties of the MoveVector is greated then the dead zone;

    /// <summary>
    /// Gets the character controller.
    /// </summary>
    /// <value>The character controller.</value>
    public static CharacterController CharacterController
    {
        get
        {
            return characterController;
        }
    }

    /// <summary>
    /// Gets a reference to this instance.
    /// </summary>
    /// <value>The instance.</value>
    public static ThirdPersonCharacterController Instance
    {
        get
        {
            return instance;
        }
    }
    
    #region Unity Events
    
    private void Awake()
    {
        characterController = this.GetComponent<CharacterController>();
        instance = this;
    }
    
    private void Update()
    {
        if (ThirdPersonCameraController.Camera != null)
        {
            ThirdPersonCharacterMotor.Instance.MoveVector = this.GetMoveVectorFromInput();
            ThirdPersonCharacterMotor.Instance.UpdateMotor();
        }
    }
    
    #endregion Unity Events

    private Vector3 GetMoveVectorFromInput()
    {
        Vector3 moveVector = Vector3.zero;

        float xAxis = Input.GetAxis("Horizontal");
        if (Mathf.Abs(xAxis) > this.moveVectorDeadZone)
        {
            moveVector.x += xAxis;
        }

        float zAxis = Input.GetAxis("Vertical");
        if (Mathf.Abs(zAxis) > this.moveVectorDeadZone)
        {
            moveVector.z += zAxis;
        }

        return moveVector;
    }
}
