using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Sova : MonoBehaviour
{
    public GameObject[] arrowSpots;

    GameObject[] importantSpots;

    NavMeshAgent agent;

    private void Start()
    {
        importantSpots = GameManager.Instance.importantSpots;
        agent = this.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        ArrowToHooka();
    }


    void ArrowToHooka()
    {
        agent.SetDestination(importantSpots[13].transform.position);

        transform.LookAt(arrowSpots[0].transform.position);


    }

}
