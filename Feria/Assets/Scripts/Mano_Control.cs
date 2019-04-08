using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.UI;

public class Mano_Control : MonoBehaviour
{
    public static Mano_Control _mano;
    [SteamVR_DefaultAction("Squeeze")]
    public SteamVR_Action_Single squeezeAction;
    public SteamVR_Action_Vector2 touchPadAction;
    // Use this for initialization
    SteamVR_Behaviour_Pose control;

    public GameObject boton_selecccionado;
    public  bool enUI;
    float triggerValueL,triggerValueR;
    public Color color_select, color_deselect;

    void Start ()
    {
        control = this.GetComponent<SteamVR_Behaviour_Pose>();
	}

    // Update is called once per frame
    void Update()
    {

        if (SteamVR_Input._default.inActions.GrabPinch.GetStateDown(SteamVR_Input_Sources.LeftHand))
        {
           
               Carro_Control._carro.MoverIzquierda();
            if (enUI && boton_selecccionado != null)
            {
                boton_selecccionado.GetComponent<Button>().onClick.Invoke();
            }


            print("PresionandoTrigger");
        }
        if (SteamVR_Input._default.inActions.GrabPinch.GetStateDown(SteamVR_Input_Sources.RightHand))
        {
           
                Carro_Control._carro.MoverDerecha();
           
            if(enUI && boton_selecccionado != null)
            {
                boton_selecccionado.GetComponent<Button>().onClick.Invoke();
            }

            print("PresionandoTrigger");
        }

        
            triggerValueR = squeezeAction.GetAxis(SteamVR_Input_Sources.RightHand);
       
            triggerValueL = squeezeAction.GetAxis(SteamVR_Input_Sources.LeftHand);

        if (triggerValueR > 0.2f)
        {
            Carro_Control._carro.MoverDerecha();
        }
        if(triggerValueL > 0.2f)
        {
            Carro_Control._carro.MoverIzquierda();
        }


        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward),Color.red);
        if (Physics.Raycast(transform.position,transform.TransformDirection(Vector3.forward), out hit,1000))
        {
            if(hit.transform.tag == "botonVR")
            {
                enUI = true;
                boton_selecccionado = hit.transform.gameObject;
                boton_selecccionado.GetComponent<Image>().color = color_select;
                //boton_selecccionado.GetComponent<Button>().s
                print(hit.transform.name);
            }
            else
            {
                if (boton_selecccionado != null)
                {
                    boton_selecccionado.GetComponent<Image>().color = color_deselect;
                    boton_selecccionado = null;
                }
             //   enUI = false;
            }
           
        }else
        {
            if(boton_selecccionado != null)
            {
                boton_selecccionado.GetComponent<Image>().color = color_deselect;
                boton_selecccionado = null;
            }
            //enUI = false;
        }

    }
}
