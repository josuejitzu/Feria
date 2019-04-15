using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;

public class Mano_Raqueta : MonoBehaviour
{

    public GameObject boton_selecccionado;
    public bool enUI;
    public Color color_select, color_deselect;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (SteamVR_Input._default.inActions.GrabPinch.GetStateDown(SteamVR_Input_Sources.LeftHand))
        {

            // Carro_Control._carro.MoverIzquierda();
            if (enUI && boton_selecccionado != null)
            {
                boton_selecccionado.GetComponent<Button>().onClick.Invoke();
            }


            print("PresionandoTrigger");
        }
        if (SteamVR_Input._default.inActions.GrabPinch.GetStateDown(SteamVR_Input_Sources.RightHand))
        {

            // Carro_Control._carro.MoverIzquierda();
            if (enUI && boton_selecccionado != null)
            {
                boton_selecccionado.GetComponent<Button>().onClick.Invoke();
            }
           

            print("PresionandoTrigger");
        }

        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward), Color.red);
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 1000))
        {
            if (hit.transform.tag == "botonVR" )
            {
                enUI = true;
                boton_selecccionado = hit.transform.gameObject;
                boton_selecccionado.GetComponent<Image>().color = color_select;

                print(hit.transform.name);
            }
            else
            {
                if (boton_selecccionado != null)
                {
                    boton_selecccionado.GetComponent<Image>().color = color_deselect;
                    boton_selecccionado = null;
                }
                enUI = false;
            }

        }
        else
        {
            if (boton_selecccionado != null)
            {
                boton_selecccionado.GetComponent<Image>().color = color_deselect;
                boton_selecccionado = null;
            }
            enUI = false;
        }

    }
}
