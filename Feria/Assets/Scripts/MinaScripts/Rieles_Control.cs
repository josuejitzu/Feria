using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Rieles_Control : MonoBehaviour
{
    public static Rieles_Control _rieles;

    public GameObject[] rieles_prefab;
    public List<GameObject> rielesA = new List<GameObject>();
    public List<GameObject> rielesB = new List<GameObject>();
    public List<GameObject> rielesC = new List<GameObject>();

    public Transform[] posiciones;
    public int cantidad;
    // Use this for initialization
    bool inicio = true;
    bool nuevoRiel;
    Vector3 posicionPrevia;

    public int cantRielesSpawn;
	void Start ()
    {
        _rieles = this;
        Spawnear();

	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
    void Spawnear()
    {
        for (int i = 0; i < cantidad; i++)
        {
            GameObject riel = Instantiate(rieles_prefab[0], transform.position, Quaternion.identity) as GameObject;
            riel.SetActive(false);
            riel.transform.name = "rielA " + i;
            rielesA.Add(riel);
        }

        for (int j = 0; j < cantidad; j++)
        {
            GameObject riel = Instantiate(rieles_prefab[1], transform.position, Quaternion.identity) as GameObject;
            riel.SetActive(false);
            riel.transform.name = "rielB " + j;
            rielesB.Add(riel);
        }
        for (int k = 0; k < cantidad; k++)
        {
            GameObject riel = Instantiate(rieles_prefab[2], transform.position, Quaternion.identity) as GameObject;
            riel.SetActive(false);
            riel.transform.name = "rielC " + k;
            rielesC.Add(riel);
        }



        print("Se terminaron de cargar los rieles");
        if(inicio)
        {
            ActivarTramo();
            inicio = false;
        }
    }
    void ActivarTramo()
    {
        for (int i = 0; i < cantidad-1; i++)
        {
            ActivarRiel();
        }
    }
    public void ActivarRiel()
    {
        GameObject riel = SeleccionRiel();
        
       
        if(!nuevoRiel)
        {
            riel.transform.position = new Vector3(0.0f, -1.07f, 6.0f);
            nuevoRiel = true;
            posicionPrevia = riel.transform.position;

        }if(cantRielesSpawn >= 20.0f)
        {
            riel.transform.position = ElegirPos();
            posicionPrevia = riel.transform.position;
        }
        else
        {
            posicionPrevia.z += 5.0f;
            riel.transform.position = posicionPrevia;
            posicionPrevia = riel.transform.position;
        }

        riel.SetActive(true);
        cantRielesSpawn++;

    }

    GameObject SeleccionRiel()
    {
        int r = Random.Range(0, rieles_prefab.Length);

        GameObject riel = null;

        if (r == 0)
        {
            foreach (GameObject ri in rielesA)
            {
                if (ri.activeInHierarchy == false)
                {
                    riel = ri;
                    
                }
            }
        }
        if (r == 1)
        {
            foreach (GameObject ri in rielesB)
            {
                if (ri.activeInHierarchy == false)
                {
                    riel = ri;
                    
                }
            }
        }
        if (r == 2)
        {
            foreach (GameObject ri in rielesC)
            {
                if (ri.activeInHierarchy == false)
                {
                    riel = ri;
                    
                }
            }
        }


        return riel;
    }
    Vector3 ElegirPos()
    {
        int rand = Random.Range(0,3);

        Vector3 pos = posiciones[rand].position;
        pos.z = posicionPrevia.z + 5.0f;
        return pos;
    }
}
