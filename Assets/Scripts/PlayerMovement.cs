using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.InputSystem;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

internal enum MovementType
{
    TransformBased,
    PhysicsBased
}

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _velocity = 1f;

    [SerializeField] private ForceMode _selectedForceMode;

    [SerializeField] private MovementType _movementType;

    [SerializeField] private float _jumpForce = 100f;

    [SerializeField] private float maxSpeed = 850f;

    [SerializeField] private Animator animator;
    [SerializeField] private float jumpingMaxHeight = 3f;
    [SerializeField] private float fallFactor = 0.9f;
    [SerializeField] private GameObject barkingHouse = null;

    private bool isOnGround = false;
    private bool isJumping = false;

    private Vector3 _movementInput3D;

    private Rigidbody _rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody>();
        StartCoroutine(CheckForGround());
    }

    // Update is called once per frame
    void Update()
    {
        PerformMovement();
    }

    void PerformMovement()
    {
        if (_movementInput3D != Vector3.zero)
        {
            transform.forward = Camera.main.transform.forward;
            Quaternion cameraAlignedForwardRotation = transform.rotation;
            transform.forward = _movementInput3D;
            transform.rotation *= cameraAlignedForwardRotation;
        }

        if (_movementInput3D == Vector3.zero)
        {
            animator.SetBool("isWalking", false);
            return;
        }

        if (_movementType == MovementType.TransformBased)
        {
            float movementStrength = Vector3.Magnitude(_movementInput3D);
            transform.Translate(new Vector3(0, 0, -1) * (_velocity * movementStrength));
            if (barkingHouse != null)
            {
                float distanceBetweenPlayerAndBarkingHouse = Vector3.Distance(gameObject.transform.position,
                    barkingHouse.gameObject.transform.position);
                AkSoundEngine.SetRTPCValue("BarkDistance", 50 - distanceBetweenPlayerAndBarkingHouse);
            }

            animator.SetBool("isWalking", true);
        }
        else
        {
            if (_rigidbody.velocity.magnitude * 100 < maxSpeed)
            {
                _rigidbody.AddForce(_movementInput3D * _velocity, _selectedForceMode);
            }
        }

        
    }

    void OnMovement(InputValue inputValue)
    {
        Vector2 movementInput = inputValue.Get<Vector2>();
        _movementInput3D = new Vector3(-movementInput.x, 0f, -movementInput.y);
    }

    void OnJump(InputValue inputValue)
    {
        animator.SetBool("isJumping", true);
        
        if (isJumping || !isOnGround)
            return;

        AkSoundEngine.PostEvent("Play_Jump", gameObject);
        StartCoroutine(JumpControlFlow());
    }

    private IEnumerator JumpControlFlow()
    {
        isJumping = true;
        float jumpHeight = transform.position.y + jumpingMaxHeight;

        _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Force);
        while (transform.position.y < jumpHeight)
        {
            yield return null;
        }

        _rigidbody.AddForce(Vector3.up * (_jumpForce * -1 * fallFactor), ForceMode.Force);
        isJumping = false;
    }

    private IEnumerator CheckForGround()
    {
        RaycastHit hit;
        while (true)
        {
            bool raycastSuccess = Physics.Raycast(transform.position, transform.up * -1, out hit);
            if (raycastSuccess && hit.collider.gameObject.CompareTag("Ground") && hit.distance <= 0.6)
            {
                if (!isOnGround)
                {
                    animator.SetBool("isJumping", false);
                }
                isJumping = false;
                StopCoroutine(JumpControlFlow());
                isOnGround = true;
            }
            else
            {
                isOnGround = false;
            }

            yield return null;
        }
    }
}