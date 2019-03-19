using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Mano_Control : MonoBehaviour
{
    public static Mano_Control _mano;
    [SteamVR_DefaultAction("Squeeze")]
    public SteamVR_Action_Single squeezeAction;
    public SteamVR_Action_Vector2 touchPadAction;
    // Use this for initialization

    void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        if (SteamVR_Input._default.inActions.GrabPinch.GetStateDown(SteamVR_Input_Sources.Any))
        {

            print("PresionandoTrigger");
        }

    }
}
