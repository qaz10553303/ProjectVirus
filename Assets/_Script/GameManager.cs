using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : SingletonBase<GameManager>
{
    public Turret selectedTower;
    public List<Turret> turretsList;
    public static float finalScore;
    public int waveCount;
    public int resourceCount;
    public float healthCount;



    void Start()
    {
        Init_();
    }

    // Update is called once per frame
    void Update()
    {
        resourceCount=Mathf.Max(resourceCount, 0);
        if (healthCount <= 0)
        {
            Debug.Log("GameOver!");
            //Pause Here!!!
            //SceneManager.LoadScene(2); comment this out when gameover
        }
    }

    void Init_()
    {
        turretsList = new List<Turret>();
        waveCount = 0;
        resourceCount = 1000;
        healthCount = 1000;
        finalScore = 0;
    }

}
