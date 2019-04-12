using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LanzadorBasura_Control : MonoBehaviour
{
    public GameObject basura_prefab;
    public int cantidadBasura;
    public List<GameObject> basuras = new List<GameObject>();
    [Space(10)]
    [Header("Mesh")]
    public Animator mapache_anim;
    [Space(10)]
    [Header("Tiempo")]
    public float rateDisparo;
    float sigDisparo;
    public float velocidad;
    public Transform boteVerde, boteRojo, boteAzul;

    public bool opcionB;
	// Use this for initialization
	void Start ()
    {
        SpawnBasura();
        sigDisparo = Time.time + rateDisparo;
        //InvokeRepeating("LanzarBasura", 2.0f,3.0f);
        //StartCoroutine(MapacheLanzando());
	}
	
	// Update is called once per frame
	void Update ()
    {
		//if(Time.time >= sigDisparo)
  //      {
  //          LanzarBasura();
  //          sigDisparo = Time.time + rateDisparo;
  //      }
	}
    public void SpawnBasura()
    {
        for (int i = 0; i < cantidadBasura; i++)
        {
            GameObject basura = Instantiate(basura_prefab, transform.position, Quaternion.identity) as GameObject;
            basura.transform.name = "basura " + i;

            if (opcionB)
            {
                basura.GetComponent<Basura_Control_B>().boteAzul = boteAzul;
                basura.GetComponent<Basura_Control_B>().boteRojo = boteRojo;
                basura.GetComponent<Basura_Control_B>().boteVerde = boteVerde;
            }
            else
            {
                basura.GetComponent<Basura_Control>().boteAzul = boteAzul;
                basura.GetComponent<Basura_Control>().boteRojo = boteRojo;
                basura.GetComponent<Basura_Control>().boteVerde = boteVerde;
            }

            basura.SetActive(false);
            basuras.Add(basura);
        }
    }
    public void LanzarBasura()
    {
        GameObject basura = BasuraRand();
        basura.transform.position = this.transform.position;
        basura.transform.rotation = this.transform.rotation;

        basura.SetActive(true);

        if (opcionB)
        {
            basura.GetComponent<Basura_Control_B>().ActivarBasura();
            basura.GetComponent<Basura_Control_B>().SetearBasura(RandTipoBasura());
            basura.GetComponent<Basura_Control_B>().rigid.velocity = transform.forward * velocidad;
        }
        else
        {
            basura.GetComponent<Basura_Control>().ActivarBasura();
            basura.GetComponent<Basura_Control>().SetearBasura(RandTipoBasura());
            basura.GetComponent<Basura_Control>().rigid.velocity = transform.forward * velocidad;
        }
        
    }

    GameObject BasuraRand()
    {
       GameObject basura = null;
       foreach(GameObject b in basuras)
        {
            if(!b.activeInHierarchy)
            {
                basura = b;
                break;
            }

        }

        return basura;
    }
    
    int RandTipoBasura()
    {
        int r = Random.Range(0, 3);

        return r;
    }
    public IEnumerator MapacheLanzando()
    {
        mapache_anim.SetTrigger("lanzar");
        yield return new WaitForSeconds(1.3f);
        LanzarBasura();


    }
}
