using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Sova : MonoBehaviour
{
    //La flecha, evidentemente
    public GameObject arrow;

    //Floats para hacer calculos a la hora de ir a un punto o mirar en una direccion
    public float offsetToPoints;
    public float lookingOffset;

    //Lista para guardar donde vemos a los enemigos para cargarla en futuras partidas
    List<int> enemiesFrequentPositions;

    //Spots a los que lanzar las flechas
    public GameObject[] arrowSpots;
    //Spots donde puede haber enemigos y a donde podemos ir
    GameObject[] importantSpots;

    //NavMesh para movernos por el escenario
    NavMeshAgent agent;

    //Angulos que ya han comprobado nuestros compañeros
    int[] anglesCleared;

    //Booleano para saber si tengo que lanzar flecha a hooka
    bool clearingHooka = true;

    //El lugar al que está mirando el personaje en este momento
    Transform lookingAt;

    private void Start()
    {
        importantSpots = GameManager.Instance.importantSpots;
        agent = this.GetComponent<NavMeshAgent>();

        enemiesFrequentPositions = new List<int>();
    }

    // Update is called once per frame
    void Update()
    {
        if (clearingHooka)
        {

            Vector3 hookaEntrance = importantSpots[13].transform.position;

            agent.SetDestination(hookaEntrance);

            Vector3 dist = transform.position - hookaEntrance;
            if (dist.magnitude < offsetToPoints) ArrowToHooka();


            if(dist.magnitude < offsetToPoints + lookingOffset) lookingAt = arrowSpots[0].transform;

        }

        transform.LookAt(lookingAt);
    }


    void ArrowToHooka()
    {


        GameObject arr = Instantiate(arrow,arrowSpots[0].transform.position, Quaternion.identity);

        arr.transform.Rotate(new Vector3(0, 0, 90));

        clearingHooka = false;

        //if(/*Enemy Spotted */)
        //{
        //    enemiesFrequentPositions.Add(/*pos*/);
        //}

    }

    void ReceiveInfo(Messages.Message msg)
    {

    }


    void addToMemory()
    {
        //Escribir en el txt las nuevas posiciones
    }

    void initializePriporities()
    {
        //Inicializar tu lista de puntos frecuentes
    }
}
