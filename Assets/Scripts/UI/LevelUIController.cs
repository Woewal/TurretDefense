using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUIController : MonoBehaviour
{

    [SerializeField] Text scrapText;

    public static LevelUIController instance;

    private void Start()
    {
        instance = this;
    }

    public void SetScrap(int newAmount)
    {
        scrapText.text = string.Format("Scrap: {0}", newAmount);
    }
}
