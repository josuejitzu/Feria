using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MunicionBellota_Control : MonoBehaviour
{
    public static MunicionBellota_Control _bellotas;

    public GameObject bellotaPrefab;
    public Transform[] posiciones_spawn;
    int posAnterior = 0;
    public int cantidadLimite;
    public int cantidadBellotas;
    [Space(10)]
    [Header("Tiempo")]
    public float rateTiempo;
    float sigSpawn;
    public bool spawnBellota;
	// Use this for initialization
	void Start ()
    {
        _bellotas = this;
        sigSpawn = rateTiempo + Time.time;
	}
	
	// Update is called once per frame
	void Update ()
    {
        TiempoSpawn();
		
	}

    void TiempoSpawn()
    {
        if (!spawnBellota)
            return;

        if (Time.time >= sigSpawn)
        {
            if (cantidadBellotas < cantidadLimite)
                SpawnBellota();

            sigSpawn = rateTiempo + Time.time;
        }
    }
    public void SpawnBellota()
    {
        GameObject bellota = Instantiate(bellotaPrefab, transform.position, Quaternion.identity);
        bellota.SetActive(false);
        bellota.transform.position = PosicionRand().position;
        bellota.SetActive(true);
        cantidadBellotas++;
    }

    Transform PosicionRand()
    {
        int r = Random.Range(0, posiciones_spawn.Length);
        while (r == posAnterior)
        {
            r = Random.Range(0, posiciones_spawn.Length);
        }
        Transform pos = posiciones_spawn[r];
        posAnterior = r;
        return pos;
    }
}
