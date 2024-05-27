using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AkSwitch : MonoBehaviour
{
    [SerializeField] private String switchGroup;
    
    [SerializeField] private String switchState;

    [SerializeField] private GameObject otherGameObject;

    private uint playingID;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(otherGameObject.tag))
        {
            AkSoundEngine.SetSwitch(switchGroup, switchState, otherGameObject);
            playingID = AkSoundEngine.PostEvent("Play_BallRoll", other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(otherGameObject.tag))
        {
            AkSoundEngine.StopPlayingID(playingID, 0, AkCurveInterpolation.AkCurveInterpolation_Constant);
        }

    }
}
