using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    [SerializeField] private GameObject objectToFollow;

    private Transform otherObjectTransform;
    private Transform ownTransform;
    private Vector3 offset;
    private float fixYOffset;
    private float distance;
    
    // Start is called before the first frame update
    void Start()
    {
        otherObjectTransform = objectToFollow.GetComponent<Transform>();
        ownTransform = gameObject.GetComponent<Transform>();
        fixYOffset = ownTransform.position.y - otherObjectTransform.position.y;
        distance = Vector3.Distance(ownTransform.position, otherObjectTransform.position);
        SetOffset();
    }

    public void SetOffset()
    {
        offset = ownTransform.position - otherObjectTransform.position;
        offset.Normalize();
        offset *= distance;
        offset.y = fixYOffset;
    }

    // Update is called once per frame
    void Update()
    {
        ownTransform.position = otherObjectTransform.position + offset;
    }
}
