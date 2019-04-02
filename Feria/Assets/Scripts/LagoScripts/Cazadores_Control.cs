using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class Cazadores_Control : MonoBehaviour
{
    public static Cazadores_Control _cazadores;

    [Header("Cazadores")]
    public GameObject cazador_prefab;
    public List<GameObject> cazador_A = new List<GameObject>();
    public int cantidadCazadores = 5;

    [Header("Puntos")]
    [Space(10)]
    public Transform[] posicionesCazadores;
    int puntoAnterior;
    [Space(10)]
    [Header("Tiempos")]
    public float rateSpawn;
    public float sigSpawn;

    public bool spawnear;
	// Use this for initialization
	void Start ()
    {
        _cazadores = this;
        SpawnCazador();
        sigSpawn = Time.time + rateSpawn;
	}
	
	// Update is called once per frame
	void Update ()
    {
	
        if(spawnear)
        {
            if(Time.time > sigSpawn)
            {
                ActivarCazadores();
                sigSpawn = Time.time + rateSpawn;
            }
        }


	}

    void SpawnCazador()
    {
        for (int i = 0; i < cantidadCazadores; i++)
        {


            GameObject cazador = Instantiate(cazador_prefab, transform.position, Quaternion.identity);
            cazador.SetActive(false);
            cazador.transform.name = "cazador_" + i;
            cazador_A.Add(cazador);
        }
    }

    public void ActivarCazadores()
    {
        foreach(GameObject c in cazador_A)
        {
            if(!c.activeInHierarchy)
            {
                Transform pos = ActivarPunto();
                c.transform.position = pos.position;
                c.transform.rotation = pos.rotation;
                c.SetActive(true);
                //c.GetComponent <Cazadores_Control>().StopAllCoroutines();
                StartCoroutine( c.GetComponent<Cazador_Control>().ActivarCazador());
                break;
            }
        }
    }

    Transform ActivarPunto()
    {
        Transform pos = null;
        int rand = Random.Range(0, posicionesCazadores.Length);

        while(rand == puntoAnterior)///ESTO TRABA PORQUE NO ES POSIBLE SABER QUE POS SE ESCOGIO
        {
            rand = Random.Range(0, posicionesCazadores.Length);

        }
        posicionesCazadores[rand].transform.gameObject.SetActive(true);

        pos = posicionesCazadores[rand];
        puntoAnterior = rand;

        return pos;
    }

    void DesactivarPunto()
    {

    }

}
