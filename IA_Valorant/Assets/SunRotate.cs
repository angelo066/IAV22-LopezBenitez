using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunRotate : MonoBehaviour
{
    [SerializeField]
    float speed = 1.0f;
    
    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        Vector3 axis = new Vector3( 0, 1, 0 );
        transform.Rotate(axis, speed*Time.deltaTime);
    }
}
