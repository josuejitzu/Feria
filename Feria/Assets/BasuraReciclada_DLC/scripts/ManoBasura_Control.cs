using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using TMPro;
using UnityEngine.UI;

public class ManoBasura_Control : MonoBehaviour
{
        
    public enum ManoBasura { izquierda,derecha}
    public ManoBasura mano;

    [SteamVR_DefaultAction("Squeeze")]
    public SteamVR_Action_Single squeezeAction;
    public SteamVR_Action_Vector2 touchPadAction;
    public SteamVR_Behaviour_Pose control;

    public bool enBasura;
    public bool conBasura;


    public basura_botes basuraTemp;
    public basura_botes basuraEnMano;

    public GameObject puntero;
    bool enUI;
    public GameObject boton_seleccionado;
    public Color color_select, color_deselect;
    public GameObject play_letrero;

    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (mano == ManoBasura.izquierda)
        {

            if (SteamVR_Input._default.inActions.GrabPinch.GetStateDown(SteamVR_Input_Sources.LeftHand))
            {


                if (enUI && boton_seleccionado != null)
                {
                    boton_seleccionado.GetComponent<Button>().onClick.Invoke();
                }
                if (basuraTemp)
                {
                    TomarBasura();
                }


                print("PresionandoTrigger");
            }
            if (SteamVR_Input._default.inActions.GrabPinch.GetStateUp(SteamVR_Input_Sources.LeftHand))
            {

                if (basuraEnMano)
                {
                    SoltarBasura();
                }
            }
        }

        if (mano == ManoBasura.derecha)
        {
            if (SteamVR_Input._default.inActions.GrabPinch.GetStateDown(SteamVR_Input_Sources.RightHand))
            {


                if (enUI && boton_seleccionado != null)
                {
                    boton_seleccionado.GetComponent<Button>().onClick.Invoke();
                }
                if (basuraTemp)
                {
                    TomarBasura();
                }

                print("PresionandoTrigger");
            }

            if (SteamVR_Input._default.inActions.GrabPinch.GetStateUp(SteamVR_Input_Sources.RightHand))
            {

                if (basuraEnMano)
                {
                    SoltarBasura();
                }
            }
        }




        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward), Color.red);
       // Debug.DrawRay(puntero.transform.position, puntero.transform.TransformDirection(Vector3.up), Color.red);
       if (Physics.Raycast(puntero.transform.position, puntero.transform.TransformDirection(Vector3.up), out hit, 5000))
        {
            if (hit.transform.tag == "botonVR")
            {
                enUI = true;
                boton_seleccionado = hit.transform.gameObject;
                boton_seleccionado.GetComponentInChildren<TextMeshProUGUI>().color = color_select;
                print(hit.transform.name);
                puntero.SetActive(true);
            }
            else
            {
                if (boton_seleccionado != null)
                {
                    boton_seleccionado.GetComponentInChildren<TextMeshProUGUI>().color = color_deselect;
                    boton_seleccionado = null;
                    puntero.SetActive(false);
                }
                enUI = false;
                puntero.SetActive(false);
            }

        }
        else
        {
            if (boton_seleccionado != null)
            {
                boton_seleccionado.GetComponentInChildren<TextMeshProUGUI>().color = color_deselect;
                boton_seleccionado = null;
            }
            puntero.SetActive(false);
            enUI = false;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "basura")
        {
            enBasura = true;
            SetBasura(other);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.transform.tag == "basura")
        {
            enBasura = true;
            SetBasura(other);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.transform.tag == "basura")
        {
            enBasura = false;
            if(basuraTemp != null)
            {
                basuraTemp = null;
            }
        }
    }
    void SetBasura(Collider basura)
    {
        if(basuraTemp || !basura.GetComponent<Rigidbody>())
        {
            return;
        }
        basuraTemp = basura.GetComponent<basura_botes>();
    }

    public void TomarBasura()
    {
        basuraEnMano = basuraTemp;
        basuraTemp = null;
        var joint = AddFixedJoint();
        joint.connectedBody =  basuraEnMano.GetComponent<Rigidbody>();
    
    }
    public void SoltarBasura()
    {
        if(this.GetComponent<FixedJoint>())
        {
            this.GetComponent<FixedJoint>().connectedBody = null;
            Destroy(this.GetComponent<FixedJoint>());

            basuraEnMano.GetComponent<Rigidbody>().velocity = control.GetVelocity();
            basuraEnMano.GetComponent<Rigidbody>().angularVelocity = control.GetAngularVelocity();
        }

        basuraEnMano = null;
        basuraTemp = null;
    }

    private FixedJoint AddFixedJoint()
    {
        FixedJoint fx = gameObject.AddComponent<FixedJoint>();
        fx.breakForce = 20000;
        fx.breakTorque = 20000;
        return fx;
    }
}
