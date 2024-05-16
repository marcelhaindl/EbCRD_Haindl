using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    [SerializeField] private GameObject objectToFollow;

    private Transform otherObjectTransform;
    private Transform ownTransform;
    private Vector3 offset;
    
    // Start is called before the first frame update
    void Start()
    {
        otherObjectTransform = objectToFollow.GetComponent<Transform>();
        ownTransform = gameObject.GetComponent<Transform>();
        offset = ownTransform.position - otherObjectTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        ownTransform.position = otherObjectTransform.position + offset;
    }
}
