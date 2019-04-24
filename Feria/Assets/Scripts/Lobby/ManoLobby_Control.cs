using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.UI;
using TMPro;

public class ManoLobby_Control : MonoBehaviour
{
    public static Mano_Control _manoLobby;
    [SteamVR_DefaultAction("Squeeze")]
    public SteamVR_Action_Single squeezeAction;
    public SteamVR_Action_Vector2 touchPadAction;
    // Use this for initialization
    public GameObject boton_selecccionado;
    public bool enUI;
    float triggerValueL, triggerValueR;
    public Color color_select, color_deselect;
    public GameObject puntero;

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

           // Carro_Control._carro.MoverDerecha();

            if (enUI && boton_selecccionado != null)
            {
                boton_selecccionado.GetComponent<Button>().onClick.Invoke();
            }

            print("PresionandoTrigger");
        }


        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward), Color.red);
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 3000))
        {
            if (hit.transform.tag == "botonVR" )
            {
                enUI = true;
                boton_selecccionado = hit.transform.gameObject;
                //boton_selecccionado.GetComponent<Image>().color = color_select;
                boton_selecccionado.GetComponentInChildren<TextMeshProUGUI>().color = color_select;
              
                puntero.SetActive(true);
                print(hit.transform.name);
            }
            else
            {
               if (boton_selecccionado != null)
                {
                   // boton_selecccionado.GetComponent<Image>().color = color_deselect;
                    boton_selecccionado.GetComponentInChildren<TextMeshProUGUI>().color = color_deselect;
                   
                }
                boton_selecccionado = null;
                puntero.SetActive(false);
                enUI = false;

            }

        }
        else
        {
            if (boton_selecccionado != null)
            {
               // boton_selecccionado.GetComponent<Image>().color = color_deselect;
                boton_selecccionado.GetComponentInChildren<TextMeshProUGUI>().color = color_deselect;
              
                
            }
            puntero.SetActive(false);
            boton_selecccionado = null;
            enUI = false;
        }
    }
}
