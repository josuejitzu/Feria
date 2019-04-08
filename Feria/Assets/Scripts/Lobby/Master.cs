using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;


public class Master : MonoBehaviour
{
    public static Master _master;
 
        
    [Space(10)]
    [Header("Tickets")]
    public int tickets;
    public bool cambiandoNivel;
    [Space(10)]
    [Header("Minas")]
    public string nivelMinas_juego;
    public string nivelMinas_tutorial,lobby;
    public enum Niveles {lobby,minasJuego,minasTutorial,patosJuego,patosTutorial,osoJuego,osoTutorial,raquetasJuego,raquetasTutorial}
    string nivelACambiar ;
    [Space(10)]
    [Header("Patos")]
    public string nivelPatos_juego;
    public string nivelPatos_tutorial;
       
    // Use this for initialization


    private void OnLevelWasLoaded(int level)
    {

        cambiandoNivel = false;
        
    }

    private void Awake()
    {
        if(_master != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _master = this;
            DontDestroyOnLoad(this.gameObject);
        }
      

        //_masterLobby = this;
    }
    void Start()
    {
        cambiandoNivel = false;
       
    }
    
    public void CambiarNivel(string n)
    {
        if (tickets <= 0 || cambiandoNivel)//Comprobar si el jugador tiene tickets para ese juego
        {
            print("No tienes Tickets....");
            return;
        }
      

        if(n == Niveles.lobby.ToString())
        {
            nivelACambiar = lobby;
        }

        if(n == Niveles.minasJuego.ToString())
        {
            nivelACambiar = nivelMinas_juego;

        }else if( n ==  Niveles.minasTutorial.ToString())
        {
            nivelACambiar = nivelMinas_tutorial;
        }

        if(n == Niveles.patosJuego.ToString())
        {
            nivelACambiar = nivelPatos_juego;

        }
        else if(n == Niveles.patosTutorial.ToString())
        {
            print("En construccion");
        }

        if (n == Niveles.osoJuego.ToString())
        {
            print("En construccion");
        }
        else if(n == Niveles.osoTutorial.ToString())
        {
            print("En construccion");
        }

        if (n == Niveles.raquetasJuego.ToString())
        {
            print("En construccion");
        }
        else if (n == Niveles.raquetasTutorial.ToString())
        {
            print("En construccion");
        }


       // DescontarTicket();
        StartCoroutine(CambiarScena(nivelACambiar));

    }

    IEnumerator CambiarScena(string n)
    {
        cambiandoNivel = true;
        //fadeNegro
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(n);
    }

    public void DescontarTicket()
    {
        if(tickets > 0)
        {
            tickets--;
        }
    }

    public void SumarTicket()
    {
        tickets++;
    }

    public void NuevasSesion()
    {
        tickets = 3;
    }


	
	
}
