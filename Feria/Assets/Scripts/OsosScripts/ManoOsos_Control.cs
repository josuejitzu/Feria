using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.UI;
using TMPro;
public class ManoOsos_Control : MonoBehaviour
{
    public enum ManoOso {izquierda,derecha};
    public ManoOso mano;

    [SteamVR_DefaultAction("Squeeze")]
    public SteamVR_Action_Single squeezeAction;
    public SteamVR_Action_Vector2 touchPadAction;
    public SteamVR_Behaviour_Pose control;
    // Use this for initialization
    public Transform bellotaPos;
    public Bellota_Control bellotaTemp;
    public bool enBellota;
    public bool bellotaEnMano;
    public Rigidbody rigid;
    public bool puedenTomar;

    //common
    public GameObject boton_selecccionado;
    public bool enUI;
    public Color color_select, color_deselect;
    public GameObject puntero;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (mano == ManoOso.izquierda)
        {

            if (SteamVR_Input._default.inActions.GrabPinch.GetStateDown(SteamVR_Input_Sources.LeftHand))
            {


                if (enUI && boton_selecccionado != null)
                {
                    boton_selecccionado.GetComponent<Button>().onClick.Invoke();
                }
                if (enBellota == true && bellotaEnMano == false && puedenTomar)
                {
                    TomarBellota();
                }


                print("PresionandoTrigger");
            }
            if (SteamVR_Input._default.inActions.GrabPinch.GetStateUp(SteamVR_Input_Sources.LeftHand))
            {

                if (bellotaEnMano)
                {
                    SoltarBellota();
                }
            }
        }

        if (mano == ManoOso.derecha )
        {
            if (SteamVR_Input._default.inActions.GrabPinch.GetStateDown(SteamVR_Input_Sources.RightHand))
            {


                if (enUI && boton_selecccionado != null)
                {
                    boton_selecccionado.GetComponent<Button>().onClick.Invoke();
                }
                if (enBellota == true && bellotaEnMano == false && puedenTomar)
                {
                    TomarBellota();
                }

                print("PresionandoTrigger");
            }

            if (SteamVR_Input._default.inActions.GrabPinch.GetStateUp(SteamVR_Input_Sources.RightHand))
            {

                if (bellotaEnMano)
                {
                    SoltarBellota();
                }
            }
        }
    



        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward), Color.red);
        Debug.DrawRay(puntero.transform.position, puntero.transform.TransformDirection(Vector3.up), Color.red);
        if (Physics.Raycast(puntero.transform.position, puntero.transform.TransformDirection(Vector3.up), out hit, 5000))
        {
            if (hit.transform.tag == "botonVR")
            {
                enUI = true;
                boton_selecccionado = hit.transform.gameObject;
                boton_selecccionado.GetComponentInChildren<TextMeshProUGUI>().color = color_select;
                print(hit.transform.name);
                puntero.SetActive(true);
            }
            else
            {
                if (boton_selecccionado != null)
                {
                    boton_selecccionado.GetComponentInChildren<TextMeshProUGUI>().color = color_deselect;
                    boton_selecccionado = null;
                    puntero.SetActive(false);
                }
                   enUI = false;
                puntero.SetActive(false);
            }

        }
        else
        {
            if (boton_selecccionado != null)
            {
                boton_selecccionado.GetComponentInChildren<TextMeshProUGUI>().color = color_deselect;
                boton_selecccionado = null;
            }
            puntero.SetActive(false);
            enUI = false;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "bellota" && !enBellota)
        {
            enBellota = true;
            bellotaTemp = other.GetComponent<Bellota_Control>();
           

        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.transform.tag == "bellota")
        {
            enBellota = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "bellota")
        {
            enBellota = false;
           // bellotaTemp = null;

        }
        
    }

    public void TomarBellota()
    {
        bellotaTemp.BellotaAgarrada();
        bellotaTemp.transform.parent = this.transform;
        bellotaTemp.transform.position = bellotaPos.position;
        bellotaEnMano = true;
    } 
    public void SoltarBellota()
    {
        
        bellotaTemp.BellotaSoltada();
        bellotaTemp.transform.parent = null;
        bellotaEnMano = false;
        bellotaTemp.GetComponent<Rigidbody>().angularVelocity = control.GetAngularVelocity() * 1.5f;
        bellotaTemp.GetComponent<Rigidbody>().velocity= control.GetVelocity() * 1.5f;
        enBellota = false;
        bellotaTemp = null;


    }
}
