using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Messages : MonoBehaviour
{
    public enum MessageType { EnemySpotte, AngleCleared }

    public MessageType type;

    public int spot;

    public List<Vector3> positions;
}
