using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using CallbackContext = UnityEngine.InputSystem.InputAction.CallbackContext;

public class SimpleMovement : MonoBehaviour
{
    [SerializeField] private Camera camera;
   
    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    public void Move(CallbackContext context)
    {
        Vector2 inputValue = context.ReadValue<Vector2>();
        Vector3 newForward = Vector3.ProjectOnPlane(camera.transform.forward, transform.up);
        Vector3 newRight = camera.transform.right;
        transform.Translate(newForward * inputValue.y + newRight * inputValue.x);
        
    }
}
