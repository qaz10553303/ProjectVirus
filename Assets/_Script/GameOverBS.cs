using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverBS : MonoBehaviour
{
    public Button _playagainButton, _mainmenuButton;
    // Start is called before the first frame update
    void Start()
    {
        _playagainButton.onClick.AddListener(TaskOnClickPa);
        _mainmenuButton.onClick.AddListener(TaskOnClickMM);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TaskOnClickPa()
    {
        SceneManager.LoadScene("MainGameplay");
    }
void TaskOnClickMM()
    {
        SceneManager.LoadScene("StartMenu");
    }
}
