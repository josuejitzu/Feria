using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider))]
public class Mano_ArcoControl : MonoBehaviour
{

    public static Mano_ArcoControl _manoArco;
    [SteamVR_DefaultAction("Squeeze")]
    public SteamVR_Action_Single squeezeAction;

    public BoxCollider trigger;

    public bool enCuerda;
    public bool presionando;

    public GameObject boton_selecccionado;
    public bool enUI;

    private void OnValidate()
    {
        trigger.isTrigger = true;
    }
    // Use this for initialization
    void Start ()
    {
        _manoArco = this;	
	}

    // Update is called once per frame
    void Update()
    {
        if (SteamVR_Input._default.inActions.GrabPinch.GetStateDown(SteamVR_Input_Sources.LeftHand))
        {
            

            print("PresionandoTrigger");
        }
        if (SteamVR_Input._default.inActions.GrabPinch.GetState(SteamVR_Input_Sources.RightHand))
        {

            if (!enUI && enCuerda)
            {

                presionando = true;
                Arco_Control._arco.presionandoCuerda = true;
                print("agarrando cuerda");
            }

            if (enUI && boton_selecccionado != null)
            {
                boton_selecccionado.GetComponent<Button>().onClick.Invoke();
            }

           // Arco_Control._arco.DispararFlecha(Arco_Control._arco.flechaFuerzaTotal);
            print("PresionandoTrigger");
        }
        if (SteamVR_Input._default.inActions.GrabPinch.GetStateUp(SteamVR_Input_Sources.RightHand))
        {

            if(presionando)
              presionando = false;

        }
    }
    private void OnTriggerEnter(Collider other)
    {

        if(other.transform.tag == "arco")
        {
            enCuerda = true;
            print("Encuerda");
        }


    }
    private void OnTriggerStay(Collider other)
    {
        if(other.transform.tag == "arco")
        {
            enCuerda = true;
            print(" En cuerda");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.transform.tag == "arco")
        {
            //enCuerda = false;
            print("Salio de cuerda");
        }
    }
}
