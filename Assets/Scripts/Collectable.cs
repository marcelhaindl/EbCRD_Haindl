using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    void Awake() {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            Destroy(gameObject);
            GameManager.instance.AddCollectedCoin();
            AkSoundEngine.PostEvent("Play_CollectCoin", gameObject);
        }
    }
}
