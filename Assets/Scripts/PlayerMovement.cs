using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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

    [SerializeField] private float _jumpForce = 0.5f;
    
    private Vector3 _movementInput3D;

    private Rigidbody _rigidbody;


    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        PerformMovement();
    }

    void PerformMovement()
    {
        if (_movementType == MovementType.TransformBased)
        {
            gameObject.transform.position += _movementInput3D * _velocity;
        }
        else
        {
            _rigidbody.AddForce(_movementInput3D, _selectedForceMode);
        }
    }

    void OnMovement(InputValue inputValue)
    {
        Vector2 movementInput = inputValue.Get<Vector2>();
        _movementInput3D = new Vector3(-movementInput.x, 0f, -movementInput.y);
    }

    void OnJump(InputValue inputValue)
    {
        Vector3 jumpForceVector = new Vector3(0f, _jumpForce, 0f);
        _rigidbody.AddForce(jumpForceVector, ForceMode.Impulse);
    }
}