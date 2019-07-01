using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Spawn_Basura : MonoBehaviour
{
    public static Spawn_Basura _spawnBasura;
    public GameObject basuraPrefab;
    public List<GameObject> basuras = new List<GameObject>();
    public Transform[] posiciones_spawn;
    int posAnterior = 0;
    int tipoAnterior = 0;
    public int poolBasura;//cantidad de basura a spawnear
    public int cantidadBasura;
    public int limiteBasura;
    [Space(10)]
    [Header("Tiempos")]
    public float rateSpawn;
    float sigSpawn;
    public bool spawnear;

	// Use this for initialization
	void Start ()
    {
        _spawnBasura = this;
        SpawnBasura();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(spawnear)
        {
            TiempoSpawn();
        }
	}

    void TiempoSpawn()
    {
        if(Time.time >= sigSpawn)
        {
            if(cantidadBasura < limiteBasura)
            {
                ActivarBasura();
                sigSpawn = Time.time + rateSpawn;
            }
        }
    }

    void ActivarBasura()
    {
        foreach(GameObject basura in basuras)
        {
            if(!basura.activeInHierarchy)
            {
                basura.SetActive(true);
                Transform pos = PosicionRand();
                basura.transform.position = pos.position;
               //basura.transform.rotation = pos.rotation;
                basura.GetComponent<basura_botes>().SetearBasura(TipoRandom());
               
                cantidadBasura++;
                break;
            }
        }
    }

    public void DescontarBasura()
    {
        cantidadBasura--;
    }

    void SpawnBasura()
    {
        for (int i = 0; i < poolBasura; i++)
        {
            GameObject basura = Instantiate(basuraPrefab, this.transform.position, Quaternion.identity) as GameObject;
            basura.SetActive(false);
            basuras.Add(basura);
        
        }
    }

    Transform PosicionRand()
    {
        int r = Random.Range(0, posiciones_spawn.Length);
        while(r == posAnterior)
        {
            r = Random.Range(0, posiciones_spawn.Length);
        }
        Transform pos = posiciones_spawn[r];
        posAnterior = r;
        return pos;
    }

    int TipoRandom()
    {
        int r = Random.Range(0, 3);
 
        while(r == tipoAnterior)
        {
            r = Random.Range(0, 3);
        }
        tipoAnterior = r;
       
        return r;
    }
}
