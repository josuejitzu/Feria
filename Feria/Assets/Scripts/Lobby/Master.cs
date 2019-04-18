using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using TMPro;
using Valve.VR;

public class Master : MonoBehaviour
{
    public static Master _master;

    public enum Niveles { lobby, minasJuego, minasTutorial, patosJuego, patosTutorial, osoJuego, osoTutorial, raquetasJuego, raquetasTutorial }
    string nivelACambiar;


    [Space(10)]
    [Header("Tickets")]
    public int tickets;
    public bool cambiandoNivel;
    [Space(10)]
    [Header("Minas")]
    public string nivelMinas_juego;
    public string nivelMinas_tutorial,lobby;
    public int murcielagos, monedasMinas, obstaculos;
   
    [Space(10)]
    [Header("Patos")]
    public string nivelPatos_juego;
    public string nivelPatos_tutorial;
    public int patos,monedasPatos,cazadores; // 0/30;
  

    [Space(10)]
    [Header("Osos")]
    public string nivelOsos_juego;
    public string nivelOsos_tutorial;
    public int osos, monedasOsos, trampas;


    [Space(10)]
    [Header("Mapaches")]
    public string nivelMapache_juego;
    public string nivelMapache_tutorial;
    public int basuraCorrecta, basuraIncorrecta,monedasBasura;
   

    [Space(10)]
    [Header("UI Operador")]
    public TMP_Text tickets_jugador;
    public TMP_InputField inputTickets;
       
    // Use this for initialization


    private void OnLevelWasLoaded(int level)
    {

        cambiandoNivel = false;
        tickets_jugador.text = tickets.ToString("00");

        if (Scores_Lobby._scoreLobby != null)
        {
            Scores_Lobby._scoreLobby.CompararScore();
            print("Solicitando comparacio de scores");
        }
        SteamVR_Fade.Start(Color.black, 0);
        SteamVR_Fade.Start(Color.clear, 1);

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
        tickets_jugador.text = tickets.ToString("00");
        SteamVR_Fade.Start(Color.black, 0);
		SteamVR_Fade.Start(Color.clear, 1);
    }

    public void CambiarNivel(string n,bool descontar)
    {
        if(tickets <= 0)
        {
            print("No tienes Tickets....");
            if(n == Niveles.lobby.ToString())
            {
                nivelACambiar = lobby;
                StartCoroutine(CambiarScena(nivelACambiar));
            }
            return;
        }

        if (cambiandoNivel)
        {
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
            nivelACambiar = nivelPatos_tutorial;
        }

        if (n == Niveles.osoJuego.ToString())
        {
            nivelACambiar = nivelOsos_juego;
        }
        else if(n == Niveles.osoTutorial.ToString())
        {
            nivelACambiar = nivelOsos_tutorial;
        }

        if (n == Niveles.raquetasJuego.ToString())
        {
            nivelACambiar = nivelMapache_juego;
        }
        else if (n == Niveles.raquetasTutorial.ToString())
        {
            print("En construccion");
        }

        if(descontar)
                DescontarTicket();

        StartCoroutine(CambiarScena(nivelACambiar));

    }

    IEnumerator CambiarScena(string n)
    {
        cambiandoNivel = true;
        //fadeNegro
        SteamVR_Fade.Start(Color.black, 0.9f);
        
        // Wait until the asynchronous scene fully loads
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(n);
    }

    public void DescontarTicket()
    {
      
        if(tickets > 0)
        {
            tickets--;
        }
        tickets_jugador.text = tickets.ToString("00");
    }

    public void SumarTicket()
    {
        tickets++;
        tickets_jugador.text = tickets.ToString("00");

    }
    public void IngresoTickets()
    {
        tickets += int.Parse(inputTickets.text);
        tickets_jugador.text = tickets.ToString("00");
        inputTickets.text = "";
    }

    public void NuevasSesion()
    {
        SceneManager.LoadScene("Lobby_01");
        tickets = 3;



    }

    public void ScoreDeSesion(string nombreJuego)
    {
        if (nombreJuego == "mina")
        {
            
        }
    }


	
	
}
