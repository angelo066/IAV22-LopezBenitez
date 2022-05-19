using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoReceiver : MonoBehaviour
{
    public void sendInfo(Messages info)
    {
        InfoReceiver[] players = GameManager.Instance.allies();

        for(int i= 0; i < players.Length; i++)
        {
            players[i].recvInfo(info);
        }
    }

    public virtual void recvInfo(Messages info)
    {
        Debug.Log("Num enmies:" + info.enemiesSpotted);
        Debug.Log("Enemie Spot:" + info.enemySpots[0]);
        Debug.Log("Enemie Pos:" + info.positions[0]);
    }

}
