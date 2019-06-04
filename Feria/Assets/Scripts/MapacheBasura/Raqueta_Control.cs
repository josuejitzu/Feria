using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

[RequireComponent(typeof(BoxCollider),typeof(Rigidbody))]
public class Raqueta_Control : MonoBehaviour
{
    
    public enum TipoRaqueta {verde,roja,azul};
    public TipoRaqueta tipoRaqueta;
    TipoRaqueta tipoOriginal;
    public SteamVR_Behaviour_Pose control;
    public Material matVerde, matRojo, matAzul;
    public ParticleSystem hit_verde, hit_rojo, hit_azul;

    public FMODUnity.StudioEventEmitter golpe_sfx,swoosh_sfx;
	// Use this for initialization
	void Start ()
    {
        tipoOriginal = tipoRaqueta;
	}
	
	// Update is called once per frame
	void Update ()
    {
        print(control.GetVelocity().magnitude);	
        if(control.GetVelocity().magnitude > 4.0f)
        {
            if(!swoosh_sfx.IsPlaying())
                   swoosh_sfx.Play();
        }

            
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "basura")
        {
            //other.GetComponent<Rigidbody>().velocity = control.GetVelocity();
            StartCoroutine(other.GetComponent<Basura_Control>().BasuraABote(tipoRaqueta.ToString()));
            other.GetComponent<Rigidbody>().angularVelocity = control.GetAngularVelocity() * 2.0f;
            ActivarGolpeFx();
        }

        if(other.transform.tag == "raqueta")
        {
            FusionRaquetas();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.transform.tag == "raqueta")
        {
            SeparacionRaquetas();
        }
    }
    void FusionRaquetas()
    {
        tipoRaqueta = TipoRaqueta.azul;
        this.GetComponent<Renderer>().material = matAzul;
    }
    void SeparacionRaquetas()
    {
        tipoRaqueta = tipoOriginal;

        if(tipoRaqueta == TipoRaqueta.roja)
            this.GetComponent<Renderer>().material = matRojo;

        if (tipoRaqueta == TipoRaqueta.verde)
            this.GetComponent<Renderer>().material = matVerde;
    }

    void ActivarGolpeFx()
    {
        if (tipoRaqueta == TipoRaqueta.verde)
        {
            hit_verde.Play();

        }else if(tipoRaqueta == TipoRaqueta.roja)
        {
            hit_rojo.Play();
        }else if(tipoRaqueta == TipoRaqueta.azul)
        {
            hit_azul.Play();
        }
        golpe_sfx.Play();
    }
}
