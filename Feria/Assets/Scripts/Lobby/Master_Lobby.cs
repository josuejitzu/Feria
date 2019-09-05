﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Master_Lobby : MonoBehaviour
{
    public static Master_Lobby _masterLobby;

    [Header("Animacion Tickets")]
    public GameObject ticketPato;
    public GameObject ticketMinas, ticketOsos, ticketRaquetas;
    [Header("SFX")]
    public FMODUnity.StudioEventEmitter menu_sfx;
    public FMODUnity.StudioEventEmitter menuNo_sfx;
	// Use this for initialization
	void Start ()
    {
        _masterLobby = this;
	}
	
	
    public void CambiarNivel(string n)
    {
       
        Master._master.CambiarNivel(n,true);
        menu_sfx.Play();    

        //if (!Master._master.cambiandoNivel)
        //    Master._master.DescontarTicket();

    }
    public void CambiarTutorial(string n)
    {

        Master._master.CambiarNivel(n, false);
    }
}
