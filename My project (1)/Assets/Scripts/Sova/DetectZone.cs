using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectZone : MonoBehaviour
{
    bool infoHasChanged = false;

    List<Vector3> enemyPositions = new List<Vector3>();
    List<int> enemySpots = new List<int>();

    int enemiesSpotted = 0;

    //Para enviar la informacion una vez la tenemos
    Arrow thisArrow = null;
    Messages msg = new Messages();


    private void OnTriggerEnter(Collider other)
    {
        Enemy en = other.gameObject.GetComponent<Enemy>();

        //Si hay un enemigo en rango, comprobamos que haya rango de vision hasta el
        if (en != null)
        {
            RaycastHit hit;
            //Si detectamos a un enemigo, almacenamos toda su información para transmitirsela a nuestros compañeros
            if(Physics.Raycast(transform.position, transform.position - other.transform.position, out hit))
            {
                enemyPositions.Add(other.gameObject.transform.position);
                enemySpots.Add(en.getSpot());

                enemiesSpotted++;
                infoHasChanged = true;
            }
        }  
    }

    private void Update()
    {
        //Timer para borrar la zona

        if (infoHasChanged)
        {
            setInfo();
            thisArrow.setInfo(msg);
            infoHasChanged = false;
        }
    }

    public Messages setInfo()
    {
        msg.type = Messages.MessageType.EnemySpotte;

        msg.positions = enemyPositions;

        msg.enemiesSpotted = enemiesSpotted;

        msg.enemySpots = enemySpots;


        return msg;
    }

    public void setArrow(Arrow a) { thisArrow = a; }
}
