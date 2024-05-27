using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateCoinsCanvas : MonoBehaviour
{
    private Text text;
    private bool win = false;

    [SerializeField] private GameObject winLooseText;
    
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Coins: " + GameManager.instance.GetCollectedCoins();
        if (GameManager.instance.GetCollectedCoins() >= 10)
        {
            if (win == false)
            {
                Text winText = winLooseText.GetComponent<Text>();
                winText.text = "You Win!";
                AkSoundEngine.PostEvent("Play_Win", gameObject);
                AkSoundEngine.SetState("GameState", "Win");
                win = true;
            }
        }
    }
}
