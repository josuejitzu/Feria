using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cazador_Control : MonoBehaviour
{
    public GameObject objetivo;
    public float velocidadRotacion;
    public GameObject pistola;
    public bool apuntar;
    public BoxCollider trigger;
    [Space(10)]
    [Header("Slider")]
    public Slider barraDisparo;
    public Image fillBarra;
    public float tiempoDisparo;
    public float velocidadSlider;
    public Color color_init, color_final;
    public bool activarSlider;
    // Use this for initialization
    public bool dañado;
    [Space(10)]
    [Header("FX")]
    public GameObject golpe_fx;

	void Start ()
    {
        
        fillBarra.color = color_init;
       // StartCoroutine(ActivarCazador());
        
    }
	
	// Update is called once per frame
	void Update ()
    {

        //Pregunta por objetivo en Parvada_Control
        if (activarSlider)
        {
            barraDisparo.value = Mathf.Lerp(barraDisparo.value, tiempoDisparo, Time.deltaTime * velocidadSlider);
            fillBarra.color = Color.Lerp(fillBarra.color, color_final, Time.deltaTime * velocidadSlider);

            if(barraDisparo.value >= 99.0f)
            {
               // activarSlider = false;
               if(!dañado)
                {
                    StartCoroutine(objetivo.GetComponent<Pato_Control>().MatarPato());
                    StartCoroutine(DesactivarCazador());
                }
                
            }
        }

        if (apuntar)
        {
            if (objetivo == null)
            {
                StartCoroutine(DesactivarCazador());
                apuntar = false;
            }
            else
            {
                Vector3 dist = objetivo.transform.position - this.transform.position;
                dist.y = 0.0f;
                this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(dist), Time.deltaTime * velocidadRotacion);

              
                Vector3 distPistola = objetivo.transform.position - pistola.transform.position;
                Debug.DrawRay(pistola.transform.position, distPistola, Color.red);
                pistola.transform.LookAt(objetivo.transform.position);
            }
        }
        

    }

    public IEnumerator ActivarCazador()
    {
        //animacion de entrada
        trigger.enabled = true;
        yield return new WaitForSeconds(1.0f);
        BuscarObjetivo();
        //animacion de apuntar
        //activarSlider = true;
      
    }
    public IEnumerator MatarCazador()
    {
        apuntar = false;
        activarSlider = false;
        barraDisparo.value = 0.0f;
        golpe_fx.SetActive(true);
        Master_Patos._masterPatos.ScoreMonedas(5);
        // objetivo = null;
        //animacion de daño
        trigger.enabled = false;
        Master_Patos._masterPatos.ScoreCazadores();
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(DesactivarCazador());
      

    }
    public IEnumerator DesactivarCazador()
    {
        activarSlider = false;
        apuntar = false;

        //objetivo.GetComponent<Pato_Control>().enMira = false;
        barraDisparo.value = 0.0f;
        objetivo = null;
        yield return new WaitForSeconds(0.5f);
        trigger.enabled = false;
        //animacion de esconder
        yield return new WaitForSeconds(1.0f);
        golpe_fx.SetActive(false);
        apuntar = false;
        activarSlider = false;
        barraDisparo.value = 0.0f;
        fillBarra.color = color_init;
       
        this.gameObject.SetActive(false);

    }

    void BuscarObjetivo()
    {
        objetivo = Parvada_Control._parvada.DarObjetivo();

        if (objetivo == null)
        {
            print(this.transform.name + " no se encontro objetivo...buscando");
            Invoke("BuscarObjetivo", 1.0f);
        }
        else
        {
            objetivo.GetComponent<Pato_Control>().enMira = true;
            activarSlider = true;
            apuntar = true;
        }

    }


}


/*
 Etapa 1: el cazador se levanta
 Etapa 2: El cazador busca el objetivo y apunta
 Etapa 3A: El cazador Dispara en 3s
 Etapa 3B: El cazador es herido antes de los 3s
 Etapa 4: El cazador se esconde

 */