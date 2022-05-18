using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectZone : MonoBehaviour
{
    bool enemySpotted = false;

    List<Vector3> enemyPositions;

    private void OnTriggerEnter(Collider other)
    {
        
        //Si hay un enemigo en rango, comprobamos que haya rango de vision hasta el
        if(other.gameObject.GetComponent<Enemy>() != null)
        {
            RaycastHit hit;
            if(Physics.Raycast(transform.position, transform.position - other.transform.position, out hit))
            {
                enemySpotted = true;

            }
        }  
    }

    public bool spottedAnEnemy()
    {
        return enemySpotted;
    }

    public Messages getPositions()
    {
        Messages msg = new Messages();

        msg.type = Messages.MessageType.EnemySpotte;

        msg.positions = enemyPositions;

        return msg;
    }
}
