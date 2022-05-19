using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    //Prefab for our revealing zone
    public GameObject reconZone;

    //Mesage to retrun from our reconZone
    Messages msg;

    Sova shooter;

    void Start()
    {
       GameObject recon = Instantiate(reconZone, this.transform.position, Quaternion.identity);

        DetectZone dz = recon.GetComponent<DetectZone>();
        dz.setArrow(this);

    }

    public void setInfo(Messages info) {
        msg = info;

        shooter.receiveInfoFromSenses(msg);
    }

    public void setShooter(Sova s) { shooter = s; }
}
