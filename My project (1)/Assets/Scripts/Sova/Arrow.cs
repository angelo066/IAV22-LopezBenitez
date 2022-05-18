using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public GameObject reconZone;

    void Start()
    {
        Instantiate(reconZone, this.transform.position, Quaternion.identity);
    }
}
