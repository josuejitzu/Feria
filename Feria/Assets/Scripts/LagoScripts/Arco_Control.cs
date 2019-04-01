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
    public Transform posFlecha;
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
    public Transform ejeRotacionAngulo;
    Quaternion rotacionInicial;
    public float stepAngle;// es 5 veces la fuerza de la flecha
    public float angulo;
    public Transform inicioArc;

    public bool presionandoCuerda;
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
        //projectileArc_mesh.SetActive(false);
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
            //DispararFlecha(10.0f);
            presionandoCuerda = false;
        }


        distanciaManos = manoIzq.transform.position - manoDer.transform.position;
        //Torcion de cuerda
        fuerzaCuerda = distanciaManos.magnitude * 80;
        cuerdaBlendshape.SetBlendShapeWeight(0, fuerzaCuerda);
        //Fuerza de la flecha
        flechaFuerzaTotal = distanciaManos.magnitude * 25;
        //creacion de arco de acuerdo a la fuerza total de la flecha
        stepAngle = 90 - (flechaFuerzaTotal * 5);//fuerzaFlecha * (20.0f * distanciaManos.magnitude);
        //print(stepAngle);
        if (ejeRotacionAngulo.rotation.x < 180.0f)
        {
            ejeRotacionAngulo.rotation = Quaternion.Euler(stepAngle,ejeRotacionAngulo.rotation.eulerAngles.y,ejeRotacionAngulo.rotation.eulerAngles.z);// (Vector3.left, stepAngle);
        }

        print("fuerza flecha: "+flechaFuerzaTotal+" angulo: "+ stepAngle);
        presionandoCuerda = Mano_ArcoControl._manoArco.presionando;
        //SetTargetWithAngle(objetivoArco.position, angulo);

         Vector3 posFlechas = new Vector3(posFlecha.transform.position.x, posFlecha.transform.position.y, 0.45f - (distanciaManos.magnitude /2));
         posFlecha.position = posFlechas;
        

        //////////////////////// LINE RENDER


    }


    public void SetTargetWithAngle(Vector3 point, float angle)
    {
        currentAngle = angle;

        Vector3 direction = point - inicioArc.position;
        float yOffset = -direction.y;
        direction = Math3d.ProjectVectorOnPlane(Vector3.up, direction);
        float distance = direction.magnitude;

        currentSpeed = ProjectileMath.LaunchSpeed(distance, yOffset, Physics.gravity.magnitude, angle * Mathf.Deg2Rad);

        projectileArc.UpdateArc(currentSpeed, distance, Physics.gravity.magnitude, currentAngle * Mathf.Deg2Rad, direction, true);
        //SetTurret(direction, currentAngle);

        currentTimeOfFlight = ProjectileMath.TimeOfFlight(currentSpeed, currentAngle * Mathf.Deg2Rad, yOffset, Physics.gravity.magnitude);
    }



    void SpawnFlechas()
    {

        for (int i = 0; i < cantidadFlechas; i++)
        {
            GameObject flecha = Instantiate(flecha_prefab, posFlecha.position, posFlecha.transform.rotation) as GameObject;
            flecha.transform.name = "flecha_" + i;
            flecha.transform.parent = this.transform;
            flecha.SetActive(false);
            
            flechas_A.Add(flecha);
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "flecha")
        {
            flechaActual = other.transform.gameObject;
            flechaActual.GetComponent<Flecha_Control>().FlechaEnArco();
            flechaActual.GetComponent<Flecha_Control>().padre = posFlecha.transform;
            flechaActual.transform.parent = posFlecha.transform;
            flechaActual.transform.position = posFlecha.position;
            flechaActual.transform.rotation = posFlecha.rotation;
        }
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

        flechaActual.transform.parent = null;
        flechaActual.GetComponent<Flecha_Control>().FlechaDisparada(f);
        flechaFuerzaTotal = 0.0f;
        cuerdaBlendshape.SetBlendShapeWeight(0, fuerzaCuerda);
        fuerzaCuerda = 0;
        presionandoCuerda = false;
        Invoke("ActivarFlecha", 0.5f);

    }
    void ActivarFlecha()
    {
        flechaActual = null;
        foreach(GameObject flecha in flechas_A)
        {
            if(!flecha.activeInHierarchy)
            {
                flecha.SetActive(true);
                break;
            }
        }
    }
}
