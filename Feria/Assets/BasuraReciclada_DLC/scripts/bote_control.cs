using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class bote_control : MonoBehaviour
{
  
    public basura_botes.TipoBasura _tipo;
    public enum Valor {a,b,c}//a=10, b=20, c=30
    public Valor _valor;
    // Use this for initialization
    public GameObject mesh_organica, mesh_inorganica, mesh_reciclable;
    public Transform objetivo;
    public float velocidad;
    public bool mover;
    public TMP_Text puntos;
    public GameObject padre;

    public Animator puntos_anim;

    private void OnValidate()
    {
        CambiarTipo();
    }
    void Start () {
		
	}
	
	// Update is called once per frame
	//void Update ()
 //   {
 //       if (mover)
 //       {
 //           this.transform.position = Vector3.MoveTowards(this.transform.position, objetivo.position, Time.deltaTime * velocidad);
 //           Vector3 dis = objetivo.position - this.transform.position;
 //           if (dis.magnitude < 0.3f)
 //           {
 //               Desactivar();
 //           }
 //       }

       
	//}
    public void MiUpdate()
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
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "basura")
        {
            StartCoroutine(other.GetComponent<basura_botes>().EnBote());
            CompararBasura(other.GetComponent<basura_botes>()._tipoBasura);
            
        }
    }
    void CompararBasura(basura_botes.TipoBasura tipoB)
    {
        if (tipoB == _tipo)
        {
            //acierto
            print("basura correcta");
            if (_valor == Valor.a)
            {
                Master_botes._masterbotes.Sumarpuntos(10);
                puntos.text = "x10";
               
            }
            else if (_valor == Valor.b)
            {
                Master_botes._masterbotes.Sumarpuntos(20);
                puntos.text = "x20";
            
            }
            else if (_valor == Valor.c)
            {
                Master_botes._masterbotes.Sumarpuntos(40);
                puntos.text = "x40";
           
            }
            puntos_anim.gameObject.SetActive(true);
            Invoke("AnimacionPuntos", 0.9f);
        }
        else if (tipoB != _tipo)
        {
            //fallo
            print("basura incorrecta");
            Master_botes._masterbotes.RestarPuntos();
        }
        
    }
    void AnimacionPuntos()
    {
        puntos_anim.gameObject.SetActive(false);
    }
    public void Activar(float vel, Transform obj,basura_botes.TipoBasura t,Valor _v)
    {
        Master_botes._masterbotes.RegistrarUpdate(this.gameObject, "bote");
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
        Master_botes._masterbotes.RemoverUpdate(this.gameObject, "bote");
        this.gameObject.SetActive(false);
    }


    public void CambiarTipo()
    {
        mesh_organica.SetActive(false);
        mesh_inorganica.SetActive(false);
        mesh_reciclable.SetActive(false);

        if(_tipo == basura_botes.TipoBasura.organica)
        {
            mesh_organica.SetActive(true);
        }
        else if(_tipo == basura_botes.TipoBasura.inorganica)
        {
            mesh_inorganica.SetActive(true);
        }
        else if(_tipo == basura_botes.TipoBasura.reciclable)
        {
            mesh_reciclable.SetActive(true);
        }
    }
}
