using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    public Button _startButton, _loadButton, _optionsButton, _exitButton, _backButton;
    public Canvas _startCanvas, _optionsCanvas;
    // Start is called before the first frame update
    void Start()
    {
        _optionsButton.onClick.AddListener(TaskOnClickOp); 
        _backButton.onClick.AddListener(TaskOnClickBa);
        _startButton.onClick.AddListener(TaskOnClickSt);
        _optionsCanvas.GetComponent<Canvas>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TaskOnClickOp()
    {
        //Output this to console when Button1 or Button3 is clicked
        _startCanvas.GetComponent<Canvas>().enabled = false;
        _optionsCanvas.GetComponent<Canvas>().enabled = true;
    } 
    void TaskOnClickBa()
    {
        //Output this to console when Button1 or Button3 is clicked
        _startCanvas.GetComponent<Canvas>().enabled = true;
        _optionsCanvas.GetComponent<Canvas>().enabled = false;
    } 
    void TaskOnClickSt()
    {
        //Output this to console when Button1 or Button3 is clicked
        SceneManager.LoadScene("MainGameplay");
    }
}
