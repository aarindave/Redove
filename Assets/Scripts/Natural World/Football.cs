using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class Football : MonoBehaviour
{
    private Rigidbody _rigidBody;
    [SerializeField]
    private XROrigin xrOrigin;
    
    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }
    
    /// <summary>
    /// The soccer ball will kick in the direction that the player is facing if the ball is on the ground.
    /// </summary>
    public void Kick()
    {
        // Terminates if the ball is not on the ground.
        if (!IsGrounded())
        {
            return;
        }
        // Creates constant to 
        const float forceMultiplier = 5.0f;
        const float forwardForceDampeningFactor = 1.0f;
        const float upForceDampeningFactor = 0.75f;
        // Sets a direction vector representing overall force.
        Vector3 direction = xrOrigin.transform.forward * forwardForceDampeningFactor
                            + xrOrigin.transform.up * upForceDampeningFactor;
        // Adds a multiplied force to the football.
        _rigidBody.AddForce(direction * forceMultiplier, ForceMode.Impulse);
    }
       
    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, 0.1f);
    }
}