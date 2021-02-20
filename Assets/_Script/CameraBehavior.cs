using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch myTouch = Input.GetTouch(Input.touchCount - 1);//save last touch infomation
            //Debug.Log(myTouch.position);
            if (myTouch.phase == TouchPhase.Began)//if user continues the touch
            {

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;
                if (Physics.Raycast(ray, out hitInfo, 1000,1<<0))//only cast on default gameobjects
                {
                    //Debug.Log("cast on other");
                    switch (hitInfo.transform.tag)
                    {
                        case "TowerPlatform":
                            hitInfo.transform.GetComponent<TowerPlatform>().SpawnTurret();
                            break;
                        case "ResourceDrop":
                            GameManager.Instance.resourceCount += 50;
                            Destroy(hitInfo.transform.gameObject);
                            break;
                        case "Turret":
                            hitInfo.transform.GetComponent<Turret>().ToggleUpgradeUI(true);

                            break;
                        default:
                            break;
                    }
                }
                else if(Physics.Raycast(ray, out hitInfo,1000,1<<LayerMask.NameToLayer("TurretUI")))
                {
                    //Debug.Log("cast on TurretUI");
                    foreach (Turret turret in GameManager.Instance.turretsList)
                    {
                        if (turret.upgradeUI.canvas.activeSelf && turret.upgradeUI.canvas != hitInfo.transform.gameObject)
                        {
                            turret.ToggleUpgradeUI(false);
                        }
                    }
                }
                else
                {
                    foreach (Turret turret in GameManager.Instance.turretsList)//if ray does not cast on turretUI, close the upgrade ui
                    {
                        if (turret.upgradeUI.canvas.activeSelf)
                        {
                            turret.ToggleUpgradeUI(false);
                        }
                    }
                }
            }
        }
    }
}
