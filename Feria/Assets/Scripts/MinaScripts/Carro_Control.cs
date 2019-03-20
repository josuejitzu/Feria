using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carro_Control : MonoBehaviour
{
    public float velocidadFinal;
    public float velocidad;
    public float step;
    public float velocidadPos;
    public Transform posCentro,posIzquierda,posDerecha;
    // Use this for initialization
    public bool enCentro, enIzquierda, enDerecha;
    public bool moverIzquierda,moverCentro,moverDerecha;

	void Start ()
    {
        enCentro = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(velocidad < velocidadFinal)
        step += Time.deltaTime * 0.5f;

        velocidad = Mathf.Lerp(0, velocidadFinal, step);
        this.transform.Translate(Vector3.forward * (Time.deltaTime * velocidad));

        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
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
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {

            if (enCentro)
            {
                moverDerecha = true;
            } else if (enIzquierda)
            {
                moverCentro = true;
            } else if (enDerecha)
            {
                return;
            }

        }

        posCentro.position = new Vector3(0.0f, this.transform.position.y, this.transform.position.z);
        posDerecha.position = new Vector3(3.0f, this.transform.position.y, this.transform.position.z);
        posIzquierda.position = new Vector3(-3.0f, this.transform.position.y, this.transform.position.z);

        if (moverCentro)
        {
            Vector3 dist = posCentro.position - this.transform.position;
            this.transform.position = Vector3.MoveTowards(this.transform.position, posCentro.position, Time.deltaTime * velocidadPos);
            if(dist.magnitude <=0.1f)
            {
                this.transform.position = posCentro.position;
                moverCentro = false;
                moverDerecha = false;
                moverIzquierda = false;
                enCentro = true;
                enIzquierda = false;
                enDerecha = false;
            }
        }
        if(moverDerecha)
        {
            Vector3 dist = posDerecha.position - this.transform.position;
            this.transform.position = Vector3.MoveTowards(this.transform.position, posDerecha.position, Time.deltaTime * velocidadPos);
            if (dist.magnitude <= 0.1f)
            {
                this.transform.position = posDerecha.position;
                moverCentro = false;
                moverDerecha = false;
                moverIzquierda = false;
                enCentro = false;
                enIzquierda = false;
                enDerecha = true;
            }
        }
        if(moverIzquierda)
        {
            Vector3 dist = posIzquierda.position - this.transform.position;
            this.transform.position = Vector3.MoveTowards(this.transform.position, posIzquierda.position, Time.deltaTime * velocidadPos);
            if (dist.magnitude <= 0.1f)
            {
                this.transform.position = posIzquierda.position;
                moverCentro = false;
                moverDerecha = false;
                moverIzquierda = false;
                enCentro = false;
                enIzquierda = true;
                enDerecha = false;
            }
        }



	}
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "spawnZona")
        {
            Rieles_Control._rieles.ActivarTramo();
        }
    }


}
