using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.UI;
public class ManoOsos_Control : MonoBehaviour {

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

    bool enUI;
    GameObject boton_selecccionado;

    void Start() {

    }

    // Update is called once per frame
    void Update()
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
            if(enBellota == true && bellotaEnMano == false)
            {
                TomarBellota();
            }

            print("PresionandoTrigger");
        }
        if (SteamVR_Input._default.inActions.GrabPinch.GetStateUp(SteamVR_Input_Sources.RightHand))
        {

           if(bellotaEnMano)
            {
                SoltarBellota();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "bellota" && !bellotaEnMano)
        {
            enBellota = true;
            bellotaTemp = other.GetComponent<Bellota_Control>();

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "bellota" && bellotaEnMano)
        {
            enBellota = false;
            bellotaTemp = null;

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

   
        


    }
}
