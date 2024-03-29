﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/**
 * This component moves its object one step up/down,
 * whenever the player clicks a key. 
 * The key is customizable via the editor.
 */
public class ClickMover : MonoBehaviour {
    [Tooltip("Step size in meters")] [SerializeField] float stepSize = 1f;

    [SerializeField]
    InputAction moveUp = new InputAction(type: InputActionType.Button);

    [SerializeField]
    InputAction moveDown = new InputAction(type: InputActionType.Button);

    [SerializeField]
    InputAction moveRight = new InputAction(type: InputActionType.Button);

    [SerializeField]
    InputAction moveLeft = new InputAction(type: InputActionType.Button);

    [SerializeField][Tooltip("Move the player to the location of 'moveToLocation'.")]
    InputAction moveTo; 

    [SerializeField][Tooltip("Determine the location to 'moveTo'.")]
    InputAction moveToLocation;
    void OnValidate() {
        // Provide default bindings for the input actions.
        // Based on answer by DMGregory: https://gamedev.stackexchange.com/a/205345/18261
        if (moveTo == null)
            moveTo = new InputAction(type: InputActionType.Button);
        if (moveTo.bindings.Count == 0)
            moveTo.AddBinding("<Mouse>/leftButton");

        if (moveToLocation == null)
            moveToLocation = new InputAction(type: InputActionType.Value, expectedControlType: "Vector2");
        if (moveToLocation.bindings.Count == 0)
            moveToLocation.AddBinding("<Mouse>/position");
    }

    void OnEnable()  {
        moveUp.Enable();
        moveDown.Enable();
        moveRight.Enable();
        moveLeft.Enable();
        moveTo.Enable();
        moveToLocation.Enable();
    }

    void OnDisable()  {
        moveUp.Disable();
        moveDown.Disable();
        moveRight.Enable();
        moveLeft.Enable();
        moveTo.Disable();
        moveToLocation.Disable();
    }

    void Update() {
        if (moveUp.WasPressedThisFrame()) {
            transform.position += new Vector3(0, stepSize, 0);
        } else if (moveDown.WasPressedThisFrame()) {
            transform.position += new Vector3(0, -stepSize, 0);
        }
            else if (moveRight.WasPressedThisFrame()) {
            transform.position += new Vector3(stepSize, 0, 0);    
        }
        else if (moveLeft.WasPressedThisFrame()) {
            transform.position += new Vector3(-stepSize, 0, 0);
        }
         else if (moveTo.WasPressedThisFrame()) {
            Vector2 mousePositionInScreenCoordinates = moveToLocation.ReadValue<Vector2>();  
            Vector3 mousePositionInWorldCoordinates = Camera.main.ScreenToWorldPoint(mousePositionInScreenCoordinates);
            mousePositionInWorldCoordinates.z = transform.position.z;
            transform.position = mousePositionInWorldCoordinates;
        }
    }
}