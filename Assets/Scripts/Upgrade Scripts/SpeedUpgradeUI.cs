using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedUpgradeUI : MonoBehaviour
{
    public SpeedUpgrade script;

    public GameObject upgradeImage;

    Text txt;

    private void Start()
    {
        txt = GetComponent<Text>();
        upgradeImage.SetActive(false);
    }

    private void Update()
    {
        if (script.currentBuyableUpgrade == 1)
        {
            txt.text = "";
        }
        else if (script.currentBuyableUpgrade == 2)
        {
            upgradeImage.SetActive(true);
            txt.text = "I";
        }
        else if (script.currentBuyableUpgrade == 3)
        {
            txt.text = "II";
        }
        else if (script.currentBuyableUpgrade == 4)
        {
            txt.text = "III";
        }
    }
}
