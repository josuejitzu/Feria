using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Master_Lobby : MonoBehaviour
{
    public static Master_Lobby _masterLobby;
	// Use this for initialization
	void Start ()
    {
        _masterLobby = this;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            Master._master.NuevasSesion();
        }
		
	}
    public void CambiarNivel(string n)
    {
  
        Master._master.CambiarNivel(n);
        Master._master.DescontarTicket();        
    }
}
