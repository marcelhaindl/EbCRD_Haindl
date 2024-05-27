using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateHealthCanvas : MonoBehaviour
{
    private Text text;
    
    private bool loose = false;
    
    [SerializeField] private GameObject winLooseText;
    
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Health: " + GameManager.instance.GetHealth();
        if (GameManager.instance.GetHealth() <= 0)
        {
            if (loose == false)
            {
                Text looseText = winLooseText.GetComponent<Text>();
                looseText.text = "You Loose!";
                AkSoundEngine.PostEvent("Play_Loose", gameObject);
                AkSoundEngine.SetState("GameState", "Loose");
                loose = true;
            }
        }
    }
}
