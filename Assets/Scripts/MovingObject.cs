using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    [SerializeField] private Vector3 dinstanceToMove;

    [SerializeField] private float velocityFactor = 1f;

    private Vector3 startingPoint;

    private Vector3 endPoint;

    private float passedTime = 0f;
    private bool increaseValue = true;

    // Start is called before the first frame update
    void Start()
    {
        startingPoint = gameObject.transform.position;
        endPoint = startingPoint + dinstanceToMove;
    }

    // Update is called once per frame
    void Update()
    {
        if (increaseValue)
        {
            passedTime += Time.deltaTime * velocityFactor;
        }
        else
        {
            passedTime -= Time.deltaTime * velocityFactor;
        }

        if (passedTime > 1)
        {
            increaseValue = false;
        }
        else if (passedTime < 0)
        {
            increaseValue = true;
        }

        Vector3 result = Vector3.Lerp(startingPoint, endPoint, passedTime);
        gameObject.transform.position = result;
    }
}