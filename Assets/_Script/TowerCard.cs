using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TowerCard : MonoBehaviour
{
    public Image u_towerImage;
    public Text u_towerName;
    public Turret towerInfo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickTowerCard()
    {
        if(GameManager.Instance.selectedTower == towerInfo)//if current select card is self
        {
            GetComponent<Button>().image.color = new Color(1, 1, 1, 1);//set back to white
            GameManager.Instance.selectedTower = null;
        }
        else
        {
            for (int i = 0; i < transform.parent.childCount; i++)
            {
                transform.parent.GetChild(i).GetComponent<Button>().image.color= new Color(1, 1, 1, 1);//set all buttons to white
            }
            GetComponent<Button>().image.color = new Color(0, 0.7f, 0, 1);//dark green
            GameManager.Instance.selectedTower = towerInfo;
        }
        
    }
}
