using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class OsosManada_Control : MonoBehaviour {

    public static OsosManada_Control _osos;
    // Use this for initialization
    [Space(10)]
    [Header("Osos")]
    public GameObject osoPrefab;
    public int cantidad;
    public List<GameObject> osos = new List<GameObject>();
    [Space(10)]
    [Header("Posiciones")]
    public Transform[] spawnPos;
    public Transform[] finCaminoPos;
    int posPasada;
    Transform posfinal_temp;
    [Space(10)]
    [Header("Trampas")]
    public LineaControl LineaA;
    public LineaControl LineaB, LineaC, LineaD, LineaE, LineaF, LineaG;
    [Space(10)]
    [Header("Tiempos")]
    public float intervaloSpawn;
    float sigSpawn;
    public bool spawnear;

    private void OnDrawGizmos()
    {
        int e = 0;
        Gizmos.color = Color.red;
        foreach (Transform p in spawnPos)
        {
            Gizmos.DrawLine(spawnPos[e].transform.position, finCaminoPos[e].transform.position);
            e++;
        }


    }
    void Start()
    {
       
        _osos = this;
        SpawnOsos();
        sigSpawn = Time.time + intervaloSpawn;
      
	}

	
	// Update is called once per frame
	void Update ()
    {

        if (spawnear)
        {
            if (Time.time > sigSpawn)
            {
                ActivarOso();
                sigSpawn = Time.time + intervaloSpawn;
            }
        }

    }

    public void SpawnOsos()
    {
        for (int i = 0; i < cantidad; i++)
        {
            GameObject oso = Instantiate(osoPrefab, transform.position, Quaternion.identity);
            oso.SetActive(false);
            oso.transform.name = "oso" + i;
            osos.Add(oso);
        }
    }
    


    public void ActivarOso()
    {
        foreach(GameObject oso in osos)
        {
            if(!oso.activeInHierarchy)
            {
                Transform pos = EscogerPos();
               
                oso.transform.position = pos.position;
                oso.transform.rotation = pos.rotation;
               
                oso.GetComponent<Oso_Control>().objetivo = posfinal_temp;
                oso.SetActive(true);

                break;
            }
        }
    }

    Transform EscogerPos()
    {
        int r = Random.Range(0, spawnPos.Length);

        if(r == posPasada)
        {
            r = Random.Range(0, spawnPos.Length);
        }
        Transform pos = spawnPos[r];
        posPasada = r;
        posfinal_temp = finCaminoPos[r];
        return pos;
    }

    public void ActivarTrampa(string linea)
    {
        if(linea == "a")
        {
            LineaA.ActivarTrampas();
        }
        if (linea == "b")
        {
            LineaB.ActivarTrampas();
        }
        if (linea == "c")
        {
            LineaC.ActivarTrampas();
        }
        if (linea == "d")
        {
            LineaD.ActivarTrampas();
        }
        if (linea == "e")
        {
            LineaE.ActivarTrampas();
        }
        if (linea == "f")
        {
            LineaF.ActivarTrampas();
        }
        if (linea == "g")
        {
            LineaG.ActivarTrampas();
        }

    }
    /*
    public void ActivarTrampa(string linea)//debe ser llamada cuando se desactiva una trampa
    {
        int rt = 0;
        switch (linea)
        {
            case "a":
                 rt = LoteriaTrampa();
                for (int i = 0; i < 6; i++)
                {
                    if(i == rt)
                    {
                        if(trampasA[rt].activeInHierarchy)
                        {
                            rt = LoteriaTrampa();
                        }
                        trampasA[rt].SetActive(true);
                        trampasA[rt].GetComponent<TrampaOso_Control>().ActivarTrampa();
                    }
                }
                break;
            case "b":
                 rt = LoteriaTrampa();
                for (int i = 0; i < 6; i++)
                {
                    if (i == rt)
                    {
                        if (trampasA[rt].activeInHierarchy)
                        {
                            rt = LoteriaTrampa();
                        }
                        trampasB[rt].SetActive(true);
                        trampasB[rt].GetComponent<TrampaOso_Control>().ActivarTrampa();
                    }
                }
                break;
            case "c":
                 rt = LoteriaTrampa();
                for (int i = 0; i < 6; i++)
                {
                    if (i == rt)
                    {
                        if (trampasA[rt].activeInHierarchy)
                        {
                            rt = LoteriaTrampa();
                        }
                        trampasC[rt].SetActive(true);
                        trampasC[rt].GetComponent<TrampaOso_Control>().ActivarTrampa();
                    }
                }
                break;
            case "d":
                 rt = LoteriaTrampa();
                for (int i = 0; i < 6; i++)
                {
                    if (i == rt)
                    {
                        if (trampasA[rt].activeInHierarchy)
                        {
                            rt = LoteriaTrampa();
                        }
                        trampasD[rt].SetActive(true);
                        trampasD[rt].GetComponent<TrampaOso_Control>().ActivarTrampa();
                    }
                }
                break;
            case "e":
                 rt = LoteriaTrampa();
                for (int i = 0; i < 6; i++)
                {
                    if (i == rt)
                    {
                        if (trampasA[rt].activeInHierarchy)
                        {
                            rt = LoteriaTrampa();
                        }
                        trampasE[rt].SetActive(true);
                        trampasE[rt].GetComponent<TrampaOso_Control>().ActivarTrampa();
                    }
                }
                break;
            case "f":
                 rt = LoteriaTrampa();
                for (int i = 0; i < 6; i++)
                {
                    if (i == rt)
                    {
                        if (trampasA[rt].activeInHierarchy)
                        {
                            rt = LoteriaTrampa();
                        }
                        trampasF[rt].SetActive(true);
                        trampasF[rt].GetComponent<TrampaOso_Control>().ActivarTrampa();
                    }
                }
                break;
            case "g":
                rt = LoteriaTrampa();
                for (int i = 0; i < 6; i++)
                {
                    if (i == rt)
                    {
                        if (trampasA[rt].activeInHierarchy)
                        {
                            rt = LoteriaTrampa();
                        }
                        trampasG[rt].SetActive(true);
                        trampasG[rt].GetComponent<TrampaOso_Control>().ActivarTrampa();
                    }
                }
                break;
            default:
                print("No se encontro la linea solicitada");
                break;

        }
    }
    */
    /*  int LoteriaTrampa()
    {
        int r = Random.Range(0, 6);
        return r;
    }*/
    
    
}
