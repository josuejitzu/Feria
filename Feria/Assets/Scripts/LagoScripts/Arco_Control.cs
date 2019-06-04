using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arco_Control : MonoBehaviour
{
    public static Arco_Control _arco;
    public SkinnedMeshRenderer cuerdaBlendshape;
    public float fuerzaCuerda = 120.0f;
    public GameObject manoIzq, manoDer;
    public Vector3 distanciaManos;

    [Space(10)]
    [Header("Flechas")]
    public GameObject flecha_prefab;
    public List<GameObject> flechas_A = new List<GameObject>();
    public int cantidadFlechas;
    public Transform posFlecha,posFlecha_A,posFlecha_B;
    
    public GameObject flechaActual;
    public float flechaFuerzaTotal;
    public float fuerzaFlecha;
    [Header("Angulo")]
    public ProjectileArc projectileArc;
    public GameObject projectileArc_mesh;
    private float currentSpeed;
    private float currentAngle;
    private float currentTimeOfFlight;
    public Transform objetivoArco;
    public Transform ejeRotacionAngulo, ejeRotacion_A, ejeRotacion_B;
    Quaternion rotacionInicial;
    public float stepAngle;// es 5 veces la fuerza de la flecha
    public float angulo;
    public Transform inicioArc;

    public bool presionandoCuerda;


    public Trayectoria lineTrayectoria;
    public FMODUnity.StudioEventEmitter cuerda_sfx;

    private void OnValidate()
    {
        cuerdaBlendshape.SetBlendShapeWeight(0, fuerzaCuerda);
        //print(ejeRotacionAngulo.rotation.ToEulerAngles());
    }
    void Start ()
    {
        _arco = this;
        SpawnFlechas();
        Invoke("ActivarFlecha",1.0f);
        rotacionInicial = ejeRotacionAngulo.rotation;
        projectileArc_mesh.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {

      
       //print(distanciaManos.magnitude);

     

        if(Input.GetKeyDown(KeyCode.F))
        {
            //DispararFlecha(10.0f);
            presionandoCuerda = true;
        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            DispararFlecha(10.0f);
            presionandoCuerda = false;
        }

        presionandoCuerda = Mano_ArcoControl._manoArco.presionando;
       


        if(presionandoCuerda)
        {
            distanciaManos = manoIzq.transform.position - manoDer.transform.position;
            cuerda_sfx.Play();
            //Torcion de cuerda
            fuerzaCuerda = distanciaManos.magnitude * 80;
            //cuerdaBlendshape.SetBlendShapeWeight(0, fuerzaCuerda);
            //Fuerza de la flecha
            flechaFuerzaTotal = distanciaManos.magnitude * 35;
            lineTrayectoria.fuerzaCurva = flechaFuerzaTotal;
            projectileArc_mesh.SetActive(true);
            Vector3 rot = this.transform.rotation.eulerAngles;
            rot.z = 0.0f;
            projectileArc_mesh.transform.rotation = Quaternion.Euler(rot);
            //creacion de arco de acuerdo a la fuerza total de la flecha
            //ejeRotacionAngulo.rotation = Quaternion.Lerp(ejeRotacion_A.rotation,ejeRotacion_B.rotation,distanciaManos.magnitude);

            //Movimiento de la flecha de acuerdo a la distancia entre manos
            posFlecha.position = Vector3.Lerp(posFlecha_A.position, posFlecha_B.position, distanciaManos.magnitude);
            
        }

        if (!presionandoCuerda && fuerzaCuerda > 0.1f)
        {
            DispararFlecha(flechaFuerzaTotal);
        }
        

        cuerdaBlendshape.SetBlendShapeWeight(0, fuerzaCuerda);
        //////////////////////// LINE RENDER


    }


    void SpawnFlechas()
    {

        for (int i = 0; i < cantidadFlechas; i++)
        {
            GameObject flecha = Instantiate(flecha_prefab, posFlecha.position, posFlecha.transform.rotation) as GameObject;
            flecha.transform.name = "flecha_" + i;
            flecha.GetComponent<Flecha_Control>().padre = posFlecha.transform;
            flecha.transform.parent = posFlecha.transform;
            flecha.SetActive(false);
            
            flechas_A.Add(flecha);
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.transform.tag == "flecha")
        {
          //  Invoke("ActivarFlecha",0.5f);
        }
    }
    public void DispararFlecha(float f)
    {
        if (flechaActual == null)
            return;

        cuerda_sfx.Stop();
        flechaActual.transform.parent = null;
        flechaActual.GetComponent<Flecha_Control>().FlechaDisparada(f);
        projectileArc_mesh.SetActive(false);
        cuerdaBlendshape.SetBlendShapeWeight(0, fuerzaCuerda);
     
        fuerzaCuerda = 0.0f;
        flechaFuerzaTotal = 0.0f;
       
        posFlecha.position = posFlecha_A.position;
        presionandoCuerda = false;
        flechaActual = null;
        Invoke("ActivarFlecha", 0.5f);

    }
    public void ActivarFlecha()
    {
        flechaActual = null;
        foreach(GameObject flecha in flechas_A)
        {
            if(!flecha.activeInHierarchy)
            {
               
                flechaActual = flecha;

                flecha.SetActive(true);
                flechaActual.GetComponent<Flecha_Control>().FlechaEnArco();
                flechaActual.GetComponent<Flecha_Control>().padre = posFlecha.transform;
                flechaActual.transform.parent = posFlecha.transform;
                flechaActual.transform.position = posFlecha.position;
                flechaActual.transform.rotation = posFlecha.rotation;
              
                break;
            }
        }
    }
}
