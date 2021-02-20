using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : SingletonBase<UIManager>
{
    public Text u_waveText;
    public Text u_resourceText;
    public Text u_healthText;
    public Slider baseHpBar;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGameInfo();
    }

    void UpdateGameInfo()
    {
        u_waveText.text = "Wave: "+GameManager.Instance.waveCount.ToString();
        u_resourceText.text = "Resource: " + GameManager.Instance.resourceCount.ToString();
        u_healthText.text = "Health: " + GameManager.Instance.healthCount.ToString();
        baseHpBar.value = GameManager.Instance.healthCount / 1000;
    }
}
