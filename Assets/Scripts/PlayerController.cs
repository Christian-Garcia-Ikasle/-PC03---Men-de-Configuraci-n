using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public float moveSpeed;     
    public Slider mSlider;
    private Vector2 _inputMove;
    public float force;
    private PlayerControls playerControls;
    
    // Start is called before the first frame update
    void Start()
    {
        playerControls = new PlayerControls();
        playerControls.Enable();
        playerControls.Floor.Move.started += ctx => MovePerformed(ctx);
        playerControls.Floor.Move.canceled += ctx => MoveCanceled(ctx);
        playerControls.Floor.Jump.performed += Jump;
        mSlider.value = 0.1f;
        mSlider.maxValue = 1.0f;
    }

    private void MovePerformed(InputAction.CallbackContext callbackContext){
        moveSpeed = mSlider.value;
        CancelInvoke(nameof(Move));
        _inputMove = callbackContext.ReadValue<Vector2>();
        InvokeRepeating(nameof(Move),0,0.01f);
    }

    private void MoveCanceled(InputAction.CallbackContext callbackContext){
        CancelInvoke(nameof(Move));
    }
    private void Move(){
        transform.position += new Vector3(_inputMove.x, 0 ,_inputMove.y) * moveSpeed;
    }
 
    private void Jump(InputAction.CallbackContext callbackContext){
        rb.AddForce(Vector3.up * force , ForceMode.Impulse);
    }

    public void OnEnable(){
        playerControls.Enable();
    }
    public void OnDisable(){
        playerControls.Disable();
    }
}
