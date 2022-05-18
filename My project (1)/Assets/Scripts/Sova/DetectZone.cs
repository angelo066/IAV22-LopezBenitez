using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        
        //Si hay un enemigo en rango, comprobamos que haya rango de vision hasta el
        if(other.gameObject.GetComponent<Enemy>() != null)
        {
            RaycastHit hit;
            if(Physics.Raycast(transform.position, transform.position - other.transform.position, out hit))
            {
                Debug.Log("Detected");

            }
        }  
    }
}
