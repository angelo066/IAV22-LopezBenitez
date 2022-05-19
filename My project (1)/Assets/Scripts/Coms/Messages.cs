using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Messages : MonoBehaviour
{
    public enum MessageType { EnemySpotted, AngleCleared }

    public MessageType type;

    public List<int> enemySpots;

    public List<Vector3> positions;

    public int enemiesSpotted;
}
