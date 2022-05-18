using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Messages : MonoBehaviour
{
    public enum MessageType { EnemySpotte, AngleCleared }

    public struct Message
    {
        MessageType type;

        int spot;

        Vector3 positon;
    }
}
