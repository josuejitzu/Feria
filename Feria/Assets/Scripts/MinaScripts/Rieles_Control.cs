using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Rieles_Control : MonoBehaviour
{
    public static Rieles_Control _rieles;

    [Header("Rieles")]
    public GameObject[] rieles_prefab;
    public List<GameObject> rielesA = new List<GameObject>();
    public List<GameObject> rielesB = new List<GameObject>();
    public List<GameObject> rielesC = new List<GameObject>();
  


    int enPos  = 0;
    public int cantidad;
    public List<GameObject> rielesUsando = new List<GameObject>();
    public int cantRielesSpawn;
    bool inicio = true;
    bool nuevoRiel = true;
    int rielAnterior;
    Vector3 posicionPrevia;


    [Space(10)]
    [Header("Paredes")]
    public GameObject[] paredes_prefab;
    public GameObject posParedes;
    public List<GameObject> paredesA = new List<GameObject>();
    public List<GameObject> paredesB = new List<GameObject>();
  
    public int cantidadParedes;
    Vector3 posParedNueva;
    Vector3 posParedvieja;
    bool nuevaPared = true;

    [Space(10)]
    [Header("Pisos")]
    public GameObject[] pisos_prefab;
    public List<GameObject>pisosA = new List<GameObject>();
    public List<GameObject>pisosB = new List<GameObject>();
    public List<GameObject>pisosC = new List<GameObject>();
    public int cantidadPisos;
    bool nuevoPiso = true;
    public Transform posPiso;
    Vector3 posPisoViejo;

    [Space(10)]
    [Header("Techos")]
    public GameObject[] techos_prefab;
    public List<GameObject> techoA = new List<GameObject>();
    public List<GameObject> techoB = new List<GameObject>();
    public List<GameObject> techoC = new List<GameObject>();
    public int cantidadTechos;
    public Transform posTecho;
    bool nuevoTecho = true;
    Vector3 posTechoViejo;

    [Space(10)]
    [Header("Soporte")]
    public GameObject[] soportes_prefab;
    public List<GameObject> soporteA = new List<GameObject>();
    public int cantidadSoportes;
    public Transform posSoporte;
    Vector3 posSoporteViejo;
    bool nuevoSoporte = true;

    [Space(10)]
    [Header("Trampas")]
    public int trampaSeleccionada;
    public int rateTrampas = 5;
    public int sigTrampa;

    [Space(10)]
    [Header("Murcielagos")]
    public int cantidadColocar;
    public int murcielagosPuestos = 0;

    [Space(10)]
    [Header("Monedas")]
    public int cantidadMonedas;

    [Space(10)]
    public GameObject spawnzona_prefab;//
    public List<GameObject> zonasSpawn = new List<GameObject>();
    public Transform[] posiciones;

    [Space(10)]
    [Header("TramoFinal")]
    public GameObject piezaFinal;
    // Use this for initialization

    public bool etapa1, etapa2, etapa3, etapafinal; 
 
    int ladoDer, ladoIzq;

	void Start ()
    {
        etapa1 = true;

        _rieles = this;

        SpawnearParedes();
        SpawnSoportes();
        SpawneearPisos();
        SpawnearTechos();
        Spawnear();
     
	}
	
	void SpawnSoportes()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject soporte = Instantiate(soportes_prefab[0], transform.position, Quaternion.identity) as GameObject;
            soporte.SetActive(false);
            soporte.transform.name = "SoporteA " + i;
            soporteA.Add(soporte);
        }

    }

    void SpawnearParedes()
    {
        for (int i = 0; i < cantidadParedes; i++)
        {
            GameObject pared = Instantiate(paredes_prefab[0], transform.position, Quaternion.identity) as GameObject;
            pared.SetActive(false);
            pared.transform.name = "paredeA " + i;
            paredesA.Add(pared);
        }
        for (int j = 0; j < cantidadParedes; j++)
        {
            GameObject pared = Instantiate(paredes_prefab[1], transform.position, Quaternion.identity) as GameObject;
            pared.SetActive(false);
            pared.transform.name = "paredeB " + j;
            paredesB.Add(pared);
        }
       

    }

    void SpawneearPisos()
    {
        for (int i = 0; i < cantidadPisos; i++)
        {
            GameObject piso = Instantiate(pisos_prefab[0], transform.position, Quaternion.identity) as GameObject;
            piso.SetActive(false);
            piso.transform.name = "pisoA " + i;
            pisosA.Add(piso);
        }

        for (int i = 0; i < cantidadPisos; i++)
        {
            GameObject piso = Instantiate(pisos_prefab[1], transform.position, Quaternion.identity) as GameObject;
            piso.SetActive(false);
            piso.transform.name = "pisoB " + i;
            pisosB.Add(piso);
        }

        for (int i = 0; i < cantidadPisos; i++)
        {
            GameObject piso = Instantiate(pisos_prefab[2], transform.position, Quaternion.identity) as GameObject;
            piso.SetActive(false);
            piso.transform.name = "pisoC " + i;
            pisosC.Add(piso);
        }
        
    }

    void SpawnearTechos()
    {
        for (int i = 0; i < cantidadTechos; i++)
        {
            GameObject techo = Instantiate(techos_prefab[0], transform.position, Quaternion.identity) as GameObject;
            techo.SetActive(false);
            techo.transform.name = "techoA" + i;
            techoA.Add(techo);
        }
        for (int j= 0; j < cantidadTechos; j++)
        {
            GameObject techo = Instantiate(techos_prefab[1], transform.position, Quaternion.identity) as GameObject;
            techo.SetActive(false);
            techo.transform.name = "techoB" + j;
            techoB.Add(techo);
        }
        for (int k = 0; k < cantidadTechos; k++)
        {
            GameObject techo = Instantiate(techos_prefab[2], transform.position, Quaternion.identity) as GameObject;
            techo.SetActive(false);
            techo.transform.name = "techoC" + k;
            techoC.Add(techo);
        }
    }

    void Spawnear()
    {
        //Spawn 9 Tipos de Rieles
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
   
    public void ActivarTramo()
    {
        if(etapafinal)
        {
            posicionPrevia.x = 0.0f;
            posicionPrevia.y = 0.0f;
            posicionPrevia.z += 2.0f;
            piezaFinal.transform.position = posicionPrevia;
            piezaFinal.SetActive(true);
           // FinalCamino();
            return;
        }
        DestruccionDeTramo();
        //Rieles
        for (int i = 0; i < 20; i++)//antes 15
        {
            ActivarRiel();

            if(i == 1)
            {
                GameObject sz = Instantiate(spawnzona_prefab, posicionPrevia, Quaternion.identity) as GameObject;
                zonasSpawn.Add(sz);
            }
           
        }
        nuevoRiel = false;

        //PAREDES
        for (int i = 0; i < 40; i++)
        {

            GameObject pared = ElegirPared();
            if(nuevaPared)
            {
                pared.transform.position = posParedes.transform.position;
                posParedNueva = pared.transform.position;
                nuevaPared = false;
            }
            else
            {
                if (pared != null)
                {
                    posParedNueva.z += 4.5f;
                    pared.transform.position = posParedNueva;
                    pared.SetActive(true);
                   // StopCoroutine(pared.GetComponent<Pared_Control>().Reiniciar());
                    posParedNueva = pared.transform.position;
                }
            }

        }
        
        //PISO
        for (int i = 0; i < 15; i++)
        {
            GameObject piso = ElegirPiso();
            if(nuevoPiso)
            {
                piso.transform.position = posPiso.transform.position;
                posPisoViejo = piso.transform.position;
                nuevoPiso = false;
            }
            else
            {
                if (piso != null)
                {
                    posPisoViejo.z += 15.4f;
                    piso.transform.position = posPisoViejo;
                }
            }
            if (piso != null)

            {
                piso.SetActive(true);
               // StopCoroutine(piso.GetComponent<Piso_Control>().Reiniciar());
                posPisoViejo = piso.transform.position;
            }
       
        }

        //TECHO
        for (int j = 0; j < 20; j++)
        {
            GameObject techo = ElegirTecho();
            if(nuevoTecho)
            {
                techo.transform.position = posTecho.position;
                posTechoViejo = techo.transform.position;
                nuevoTecho = false;
            }else
            {
                if(techo != null)
                {
                    posTechoViejo.z += 9.9f;
                    techo.transform.position = posTechoViejo;
                }
            }
            if(techo != null)
            {
                techo.SetActive(true);
                //StopCoroutine(techo.GetComponent<Techo_Control>().Reiniciar());
                posTechoViejo = techo.transform.position;
            }
           //// StopCoroutine(techo.GetComponent<Techo_Control>().Reiniciar());
           // posTechoViejo = techo.transform.position;

        }

        //SOPORTE
        for (int i = 0; i < 10 ; i++)
        {


            GameObject soporte = ElegirSoporte();
          
            if (nuevoSoporte)
            {
                soporte.transform.position = posSoporte.position;
                posSoporteViejo = soporte.transform.position;
                nuevoSoporte = false;

            }
            else
            {
                if (soporte != null)
                {
                    posSoporteViejo.z += 45.0f;
                    soporte.transform.position = posSoporteViejo;
                    soporte.SetActive(true);
                    StopCoroutine(soporte.GetComponent<Soporte_Control>().Reiniciar());
                    posSoporteViejo = soporte.transform.position;
                }
            }
           // StopCoroutine(soporte.GetComponent<Soporte_Control>().Reiniciar());

        }

        
    }

    public void ActivarRiel()
    {
        GameObject riel = null;


        if (nuevoRiel)
        {
            
                riel = SeleccionRiel();
                riel.transform.position = ElegirPos(0);
                riel.SetActive(true);
            
        }
        else
        {
            
            for (int i = 0; i < posiciones.Length; i++)
            {
                riel = SeleccionRiel();
                while (riel == null)
                {
                    riel = SeleccionRiel();
                }
                if (riel != null)
                {
                    riel.transform.position = ElegirPos(i);

                    if(cantRielesSpawn >= 25)
                    {
                        int l = LoteriaTrampa();
                        riel.GetComponent<Riel_Control>().SetTrampa(l);

                        if(l == 7)
                        {
                            if (murcielagosPuestos < 40)
                            {
                                riel.GetComponent<Riel_Control>().SetMurcielagos(LoteriaMurcielago());
                                murcielagosPuestos++;
                            }

                        }
                        else if(l == 9)
                        {
                            if(cantidadMonedas < 120)
                            {
                                riel.GetComponent<Riel_Control>().SetMonedas(LoteriaMonedas());
                                
                            }
                        }

                        sigTrampa = cantRielesSpawn + rateTrampas;
                       /* if (cantRielesSpawn > sigTrampa)
                        {
                            riel.GetComponent<Riel_Control>().SetTrampa(LoteriaTrampa());
                            sigTrampa = cantRielesSpawn + rateTrampas;
                        }
                        */

                        
                    }

                    riel.SetActive(true);
                    StopCoroutine(riel.GetComponent<Riel_Control>().Reseteo(0.5f));
                    //posicionPrevia = riel.transform.position;
                }
               
            }

        }

            cantRielesSpawn++;
            posicionPrevia = riel.transform.position;
          
                
    }

    public void DestruccionDeTramo()
    {
        //comienza la desactivacion los rieles del tramo actual
        foreach(GameObject r in rielesUsando)
        {
            if (r.activeInHierarchy)
            {
                r.GetComponent<Riel_Control>().Activado();
                
            }
        }

    }

    public void FinalCamino()
    {
       
        foreach(GameObject p in paredesA)
        {
            p.SetActive(false);
        }
        foreach (GameObject pb in paredesB)
        {
            pb.SetActive(false);
        }
        foreach (GameObject piso in pisosA)
        {
            piso.SetActive(false);
        }
        foreach(GameObject t in techoA)
        {
            t.SetActive(false);
        }
        foreach(GameObject s in soporteA)
        {
            s.SetActive(false);
        }
        foreach(GameObject z in zonasSpawn )
        {

            z.SetActive(false);

        }

      
    }

    GameObject SeleccionRiel()
    {
        GameObject riel = null;



            int r = Random.Range(0, 3);

           while(r == rielAnterior)
            {
                r = Random.Range(0, 3);
            }

            if (r == 0)
            {
                foreach (GameObject ri in rielesA)
                {
                    if (ri.activeInHierarchy == false)
                    {
                        riel = ri;

                    break;

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
                    break;
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
                    break;
                    }
                }
            }

        rielAnterior = r;

        return riel;
    }

    Vector3 ElegirPos(int n)
    {

        Vector3 pos = posiciones[n].position;
        pos.y = -1.07f;
        pos.z = posicionPrevia.z + 5.0f;
       // print(n);
        return pos;
    }

    GameObject ElegirPared()
    {
        int rand = Random.Range(0,2);
        GameObject p = null;


       
      if (rand == 0)
      {
        foreach (GameObject pared in paredesA)
        {
            if (!pared.activeInHierarchy)
            {
                        p = pared;
                        break;
            }
         }
      }
      else if (rand == 1)
            {
                foreach (GameObject pared in paredesB)
                {
                    if (!pared.activeInHierarchy)
                    {
                        p = pared;
                         break;
                    }
                }
       }



        if (p == null)
            print("No se encontro pared");


        return p;

    }

    GameObject ElegirPiso()
    {
        GameObject piso = null;

        foreach (GameObject p in pisosA)
        {
            if (!p.activeInHierarchy)
            {
                piso = p;
                break;
            }
        }

      
        if(piso == null)
        {
            print("NO se encontro piso");
        }
        return piso;
    }

    GameObject ElegirTecho()
    {
        GameObject techo = null;

        foreach (GameObject t in techoA)
        {
            if (!t.activeInHierarchy)
            {

                techo = t;
               // techo.GetComponent<Techo_Control>().StopAllCoroutines();
             //   StopAllCoroutines(techo.GetComponent<Techo_Control>());
                break;
            }

        }
        if (techo == null)
            print("No se encontro techo");

        return techo;
    }

    GameObject ElegirSoporte()
    {
        GameObject soporte = null;

        foreach(GameObject s in soporteA)
        {
            if(!s.activeInHierarchy)
            {
                soporte = s;
               // soporte.GetComponent<Soporte_Control>().StopAllCoroutines();
                soporte.gameObject.SetActive(false);
                break;
            }
        }
        return soporte;
    }

    int LoteriaTrampa()
    {
        int rand = Random.Range(0, 27);
        while(rand == trampaSeleccionada)
        {
            rand = Random.Range(0, 27);
        }

        trampaSeleccionada = rand;

        return rand;
    }
    
    int LoteriaMurcielago()
    {

        int rand = Random.Range(0, 1);
        return rand;

    }

    int LoteriaMonedas()
    {

        int rand = Random.Range(0, 4);
        return rand;

    }

}
