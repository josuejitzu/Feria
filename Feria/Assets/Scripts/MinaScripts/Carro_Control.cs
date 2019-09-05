using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carro_Control : MonoBehaviour
{
    public static Carro_Control _carro;
    public float velocidadFinal;
    public float velocidad;
    public float step;
    public float velocidadPos;
    public Transform posCentro,posIzquierda,posDerecha;
    // Use this for initialization
    public bool enCentro, enIzquierda, enDerecha;
    public bool moverIzquierda,moverCentro,moverDerecha;
    public bool enMovimiento;

    public bool puedeMoverse;
    public   bool enFinal;
    public bool inmortal;

    public float velocidadA, velocidadB, velocidadC;
    public GameObject luzR, luzL;

    [Space(10)]
    [Header("MonedasFx")]
    public GameObject[] monedasAnim;
    [Space(10)]
    [Header("SFX")]
    public FMODUnity.StudioEventEmitter rieles_sfx;
    public FMODUnity.StudioEventEmitter cambioCarril_sfx, choque_sfx,moneda_sfx;

    [Space(10)]
    [Header("Golpe Efectos")]
    public GameObject murcielago;
    public Transform muercielago_pos;
    public Animator murcielago_anim;
    public Animator destello_anim;
    public GameObject golpe_fx;
    public GameObject murcielagoPerdido_fx;



	void Start ()
    {
        _carro = this;
        enCentro = true;

	}
	
	// Update is called once per frame
	void Update ()
    {
        if(velocidad < velocidadFinal)
          step += Time.deltaTime * 0.5f;

        velocidad = Mathf.Lerp(0, velocidadFinal, step);

        if(velocidad > 0.5)
        {
            if(!rieles_sfx.IsPlaying())
                 rieles_sfx.Play();
        }else if(velocidad < 0.1f)
        {
          
                rieles_sfx.Stop();
        }
        this.transform.Translate(Vector3.forward * (Time.deltaTime * velocidad));

        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoverIzquierda();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoverDerecha();
        }

        posCentro.position = new Vector3(posCentro.position.x, this.transform.position.y, this.transform.position.z);
        posDerecha.position = new Vector3(posDerecha.position.x, this.transform.position.y, this.transform.position.z);
        posIzquierda.position = new Vector3(posIzquierda.position.x, this.transform.position.y, this.transform.position.z);

        if (moverCentro)
        {

            enMovimiento = true;
            Vector3 dist = posCentro.position - this.transform.position;
            this.transform.position = Vector3.Lerp(this.transform.position, posCentro.position, Time.deltaTime * velocidadPos);
            if(dist.magnitude <=0.2f)
            {
                //this.transform.position = posCentro.position;
                moverCentro = false;
                moverDerecha = false;
                moverIzquierda = false;
                enCentro = true;
                enIzquierda = false;
                enDerecha = false;
                enMovimiento = false;
            }
        }
        if(moverDerecha)
        {
            enMovimiento = true;
            Vector3 dist = posDerecha.position - this.transform.position;
            this.transform.position = Vector3.Lerp(this.transform.position, posDerecha.position, Time.deltaTime * velocidadPos);
            if (dist.magnitude <= 0.2f)
            {
                //this.transform.position = posDerecha.position;
                moverCentro = false;
                moverDerecha = false;
                moverIzquierda = false;
                enCentro = false;
                enIzquierda = false;
                enDerecha = true;
                enMovimiento = false;
            }
        }
        if(moverIzquierda)
        {
            enMovimiento = true;
            Vector3 dist = posIzquierda.position - this.transform.position;
            this.transform.position = Vector3.Lerp(this.transform.position, posIzquierda.position, Time.deltaTime * velocidadPos);
            if (dist.magnitude <= 0.2f)
            {
               // this.transform.position = posIzquierda.position;
                moverCentro = false;
                moverDerecha = false;
                moverIzquierda = false;
                enCentro = false;
                enIzquierda = true;
                enDerecha = false;
                enMovimiento = false;
            }
        }

        if(this.transform.position.z >= 80.0f)
        { 
            if(!enFinal)
            puedeMoverse = true;
        }

	}
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "spawnZona")
        {
            other.GetComponent<BoxCollider>().enabled = false;
            Rieles_Control._rieles.ActivarTramo();
        }
        if(other.transform.tag =="trampa")
        {

            StartCoroutine(RecibirDaño());
            choque_sfx.Play();
        }

        if(other.transform.tag == "finalSpawn")
        {
            StartCoroutine(AcomodoFinal());
            Rieles_Control._rieles.FinalCamino();
        }
        if (other.transform.tag == "finalVia") 
        {
            Master_Minas._mina.FinJuego();
            rieles_sfx.Stop();
            cambioCarril_sfx.Stop();
            ApagarLuces();
        }
    }

    public IEnumerator RecibirDaño()
    {
       
        yield return new WaitForSeconds(0.1f);
        if (!inmortal)
        {
            destello_anim.SetTrigger("destello");
            inmortal = true;
            StartCoroutine(AventarMurcielago());
            Master_Minas._mina.RestarMonedas(10);
            // velocidadFinal = -1.0f;
            golpe_fx.GetComponent<ParticleSystem>().Stop();
            golpe_fx.SetActive(true);
            yield return new WaitForSeconds(1.0f);
            golpe_fx.SetActive(false);
           // velocidadFinal = 7.0f;
            inmortal = false;
        }
   

        Master_Minas._mina.RestarMurcielago();
    }

    public void MoverDerecha()
    {
        if (!puedeMoverse)
            return;


        if (enMovimiento)
            return;
        if (enCentro)
        {
            moverDerecha = true;


        }
        else if (enIzquierda)
        {
            moverCentro = true;
        }
        else if (enDerecha)
        {
            return;
        }
        cambioCarril_sfx.Play();

    }

    public void MoverIzquierda()
    {
        if (!puedeMoverse)
            return;
        if (enMovimiento)
            return;

        if (enCentro)
        {
            moverIzquierda = true;
        }
        else if (enDerecha)
        {
            moverCentro = true;
        }
        else if (enIzquierda)
            return;

        cambioCarril_sfx.Play();
    }


    IEnumerator AcomodoFinal()
    {
        moverCentro = true;
        puedeMoverse = false;
        enFinal = true;
        yield return new WaitForSeconds(1.0f);
        puedeMoverse = false;
    }

    public IEnumerator Parar()
    {
        yield return new WaitForSeconds(0.2f);
        velocidadFinal = 5.0f;
        yield return new WaitForSeconds(0.5f);
        velocidadFinal = 3.0f;
        yield return new WaitForSeconds(0.1f);
        velocidadFinal = 0.0f;

    }

    public IEnumerator AventarMurcielago()
    {

        yield return new WaitForSeconds(0.1f);
        murcielago.SetActive(true);
        murcielago_anim.gameObject.SetActive(true);
        murcielago_anim.SetTrigger("aventar");
        yield return new WaitForSeconds(1.0f);
        murcielago.transform.parent = null;
        murcielago_anim.gameObject.SetActive(false);
        murcielagoPerdido_fx.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        murcielago.transform.parent = this.transform;
        murcielago.transform.position = muercielago_pos.position;
        murcielagoPerdido_fx.SetActive(false);


    }

    public IEnumerator ActivarMonedaEfecto()
    {
        GameObject moneda = null;
        foreach(GameObject m in monedasAnim)
        {
            if(!m.activeInHierarchy)
            {
                moneda = m;
                moneda.SetActive(true);
            }
        }
        moneda_sfx.Play();
        yield return new WaitForSeconds(0.45f);
        if(moneda != null)
             moneda.SetActive(false);

    }

    public void ApagarLuces()
    {
        luzL.SetActive(false);
        luzR.SetActive(false);
    }
    public void CambiarVelocidad(int v)
    {

        if (v == 1)
        {
            velocidadFinal = velocidadA;
        }else if( v== 2)
        {
            velocidadFinal = velocidadB;

        }else if(v == 3)
        {
            velocidadFinal = velocidadC;
        }


    }
}
