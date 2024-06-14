using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayFootsteps : MonoBehaviour
{
    public void PlayFootstep()
    {
        AkSoundEngine.PostEvent("Play_Footsteps", gameObject);
    }
}