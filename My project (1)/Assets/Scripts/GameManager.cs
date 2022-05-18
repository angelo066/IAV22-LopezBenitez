using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Numero de enemigos con el que queremos hacer las pruebas (entre 2 y 5)
    public int numEnemies;

    //Spots importantes del mapa
    public GameObject[] importantSpots;

    //lugares donde se pueden colocar nuestros enemigos
    public GameObject[] enemySpawnSpots;

    //Pregab de enemigo para instanciarlo
    public GameObject enemy;

    //Singleton
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        for(int i= 0; i < numEnemies; i++)
        {
            int spot = Random.Range(0, enemySpawnSpots.Length);

            Instantiate(enemy, enemySpawnSpots[spot].transform.position, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

