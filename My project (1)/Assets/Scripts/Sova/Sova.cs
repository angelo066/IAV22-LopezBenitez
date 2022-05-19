using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Sova : InfoReceiver
{
    //Enum de los lugares a los que mirar o en los que colocarse
    enum Spots {Market, Close_Long, Close_Fountain, Long_Pocket, Garden, Garden_Pocket, Elbow, Close_Ct, Ct, BackSite, Container_Box, Containner_Cubby,
    Under_Window, Hooka_Entry, Back_Hooka, Hooka_Close, Long_Garden};

    enum arrows { Hooka, Long, Site};

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
    bool clearingHooka = false;
    //Booleano para saber si tengo que lanzar flecha a hooka
    bool clearingLong = false;
    //Booleano para saber si tengo que lanzar flecha a hooka
    bool clearingSite = false;

    //Booleano para saber cuando tenemos que comunicar nueva informacion
    bool newInformation = false;
    Messages msg; //Mesaje para enviar
    Arrow actualArrow; //Flecha que nos proporciona la informacion

    //The angle that we are holding at the moment
    Transform holding = null;

    private void Start()
    {
        importantSpots = GameManager.Instance.importantSpots;
        agent = this.GetComponent<NavMeshAgent>();

        enemiesFrequentPositions = new List<int>();
    }

    // Update is called once per frame
    //Guardarme los spots importantes de Sova en el Start
    void Update()
    {
        //Debugging input
        if (Input.GetKeyDown(KeyCode.F1))
        {
            clearingHooka = true;
        }
        else if (Input.GetKeyDown(KeyCode.F2))
        {
            clearingLong = true;
        }
        else if (Input.GetKeyDown(KeyCode.F3))
        {
            clearingSite = true;
        }
        else if (Input.GetKeyDown(KeyCode.F4))
        {
            stopHolding();
        }

        if (clearingHooka)
        {

            Vector3 hookaEntrance = importantSpots[(int)Spots.Hooka_Entry].transform.position;
            Vector3 hookaArrowSpot = arrowSpots[(int)arrows.Hooka].transform.position;

            agent.SetDestination(hookaEntrance);

            Vector3 dist = transform.position - hookaEntrance;
            if (dist.magnitude < offsetToPoints)
            {
                shootArrow(hookaArrowSpot, ref clearingHooka, true);

                //Miramos a donde pueden correr los enemigos
                holding = importantSpots[(int)Spots.Back_Hooka].transform;
            }

            //Preparamos la flecha antes de asomar a la esquina para no perder tiempo
            if(dist.magnitude < offsetToPoints + lookingOffset) transform.LookAt(arrowSpots[(int)arrows.Hooka].transform);

        }
        else if (clearingLong)
        {

            Vector3 longClose = importantSpots[(int)Spots.Close_Long].transform.position;
            Vector3 longArrowSpot = arrowSpots[(int)arrows.Long].transform.position;

            agent.SetDestination(longClose);

            Vector3 dist = transform.position - longClose;
            if (dist.magnitude < offsetToPoints)
            {
                shootArrow(longArrowSpot, ref clearingLong, false);

                //Despues de lanzar la flecha, miramos al sitio del que pueden salir los enemigos corriendo
                holding = importantSpots[(int)Spots.Long_Pocket].transform;
            }

            //Miramos antes para no perder tiempo
            if (dist.magnitude < offsetToPoints + lookingOffset) transform.LookAt(arrowSpots[(int)arrows.Long].transform);

        }
        else if (clearingSite)
        {
            //Decidimos desde donde lanzar la flecha
            Vector3 distToLong = transform.position - importantSpots[(int)Spots.Close_Long].transform.position;
            Vector3 distToHooka = transform.position - importantSpots[(int)Spots.Hooka_Entry].transform.position;

            //La lanzamos desde larga
            if (distToLong.magnitude < distToHooka.magnitude)
            {
                Vector3 longGarden = importantSpots[(int)Spots.Long_Garden].transform.position;
                Vector3 siteArrowSpot = arrowSpots[(int)arrows.Site].transform.position;

                agent.SetDestination(longGarden);

                Vector3 dist = transform.position - longGarden;
                if (dist.magnitude < offsetToPoints)
                {
                    shootArrow(siteArrowSpot, ref clearingSite, false);

                    holding = importantSpots[(int)Spots.Containner_Cubby].transform;
                }


                if (dist.magnitude < offsetToPoints + lookingOffset) transform.LookAt(arrowSpots[(int)arrows.Site].transform);
            }//La lanzamos desde hooka
            else
            {
                Vector3 inHooka = importantSpots[(int)Spots.Hooka_Close].transform.position;
                Vector3 siteArrowSpot = arrowSpots[(int)arrows.Site].transform.position;

                agent.SetDestination(inHooka);

                Vector3 dist = transform.position - inHooka;
                if (dist.magnitude < offsetToPoints)
                {
                    shootArrow(siteArrowSpot, ref clearingSite, false);

                    holding = importantSpots[(int)Spots.BackSite].transform;
                }


                if (dist.magnitude < offsetToPoints + lookingOffset) transform.LookAt(arrowSpots[(int)arrows.Site].transform);
            }
        }


        if (newInformation)
        {
            sendInfo(msg);
            newInformation = false;
        }

        transform.LookAt(holding);
    }


    void shootArrow(Vector3 target, ref bool zone, bool wallOrientation)
    {

        GameObject arr = Instantiate(arrow,target, Quaternion.identity);

        if(wallOrientation)arr.transform.Rotate(new Vector3(0, 0, 90));
        else arr.transform.Rotate(new Vector3(90, 0, 0));

        arr.GetComponent<Arrow>().setShooter(this);

        zone = false;

        //if(/*Enemy Spotted */)
        //{
        //    enemiesFrequentPositions.Add(/*pos*/);
        //}

    }
    void addToMemory()
    {
        //Escribir en el txt las nuevas posiciones
    }

    void initializePriporities()
    {
        //Inicializar tu lista de puntos frecuentes
    }

    void stopHolding() { holding = null; }

    public void receiveInfoFromArrow(Messages info) {
        msg = info;
        newInformation = true;
    }
}
