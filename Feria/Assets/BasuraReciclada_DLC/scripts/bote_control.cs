using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bote_control : MonoBehaviour
{
    public enum TipoBote {organica,inorganico,reciclable}
    public TipoBote _tipo;
    public enum Valor {a,b,c}//a=10, b=20, c=30
    public Valor _valor;
    // Use this for initialization
    public GameObject mesh_organica, mesh_inorganica, mesh_reciclable;
    public Transform objetivo;
    public float velocidad;
    public bool mover;
    public GameObject padre;


    private void OnValidate()
    {
        CambiarTipo();
    }
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (mover)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, objetivo.position, Time.deltaTime * velocidad);
            Vector3 dis = objetivo.position - this.transform.position;
            if (dis.magnitude < 0.3f)
            {
                Desactivar();
            }
        }

       
	}
    public void Activar(float vel, Transform obj,TipoBote t,Valor _v)
    {
        _tipo = t;
        CambiarTipo();
        velocidad = vel;
        objetivo = obj;
        _valor = _v;
        mover = true;


    }
    public void Desactivar()
    {
        mover = false;
        objetivo = null;
        velocidad = 0;

        mesh_organica.SetActive(false);
        mesh_inorganica.SetActive(false);
        mesh_reciclable.SetActive(false);

        this.transform.position = padre.transform.position;
        this.gameObject.SetActive(false);
    }


    public void CambiarTipo()
    {
        mesh_organica.SetActive(false);
        mesh_inorganica.SetActive(false);
        mesh_reciclable.SetActive(false);

        if(_tipo == TipoBote.organica)
        {
            mesh_organica.SetActive(true);
        }
        else if(_tipo == TipoBote.inorganico)
        {
            mesh_inorganica.SetActive(true);
        }
        else if(_tipo == TipoBote.reciclable)
        {
            mesh_reciclable.SetActive(true);
        }
    }
}
