using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class CameraRotation : MonoBehaviour
{
    [SerializeField] private GameObject playerFigure;
    private FollowObject _followObject;
    
    // Start is called before the first frame update
    void Start()
    {
        _followObject = GetComponent<FollowObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCameraRotation(InputValue inputValue)
    {
        Vector2 input = inputValue.Get<Vector2>();
        transform.RotateAround(playerFigure.transform.position, new Vector3(0, 1, 0), input.x);
        _followObject.SetOffset();
    }
}
