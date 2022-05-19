using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class allyVision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Enemy en = other.gameObject.GetComponent<Enemy>();

        //Si hay un enemigo en rango, comprobamos que haya rango de vision hasta el
        if (en != null)
        {
            RaycastHit hit;
            //Si detectamos a un enemigo, almacenamos toda su información para transmitirsela a nuestros compañeros
            if (Physics.Raycast(transform.position, transform.position - other.transform.position, out hit))
            {
                //Debug.Log("Hola te veo");
                Messages msg = new Messages();
                msg.enemySpots = new List<int>();
                msg.positions = new List<Vector3>();

                msg.type = Messages.MessageType.EnemySpotted;

                msg.enemiesSpotted++;

                msg.enemySpots.Add(en.getSpot());

                msg.positions.Add(other.transform.position);

                GetComponentInParent<InfoReceiver>().sendInfo(msg);
            }
        }
    }
}
