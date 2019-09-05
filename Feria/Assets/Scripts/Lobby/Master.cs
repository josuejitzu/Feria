using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using TMPro;
using Valve.VR;
using CI.QuickSave;

public class Master : MonoBehaviour
{
    public static Master _master;

    public enum Niveles {lobby, minasJuego, minasTutorial, patosJuego, patosTutorial, osoJuego, osoTutorial, raquetasJuego, raquetasTutorial }
    string nivelACambiar;

    [Space(10)]
    public GameObject menu_panel;
    public GameObject cerrarBoton, menuBoton;

    [Space(10)]
    [Header("Tickets")]
    public int tickets;
    public bool cambiandoNivel;
    string dataPath;
    public string infoTickets;
    public int ticketsComprados;
    public bool nuevoUsuario = true;
    public int usuarios;
    public int usuarioNum;

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

    [Space(10)]
    [Header("SFX")]
    public FMODUnity.StudioEventEmitter menu_sfx;
    public FMODUnity.StudioEventEmitter menuNo_sfx;
    // Use this for initialization


    private void OnLevelWasLoaded(int level)
    {


        cambiandoNivel = false;
        tickets_jugador.text = tickets.ToString("00");

        dataPath = Application.dataPath;

        //ConteoTickets();
        //ConteoUsuarios();
        //if (level == 1 && nuevoUsuario)
        //    NuevoUsuario();

        if (_master = this)
        {
            if (nuevoUsuario)
            {
               StartCoroutine(NuevoUsuario());
            }
            else if (!nuevoUsuario && level == 0)
            {
                nuevoUsuario = true;
            }
        }



        if (Scores_Lobby._scoreLobby != null)
        {
            Scores_Lobby._scoreLobby.CompararScore();
            print("Solicitando comparacion de scores");
        }
        SteamVR_Fade.Start(Color.black, 0);
        SteamVR_Fade.Start(Color.clear, 1);




    }


    private void Awake()
    {
        if (_master != null)
        {
            gameObject.GetComponent<Master>().StopAllCoroutines();
            Destroy(gameObject);
            print("Se destruyo aquel objeto" + gameObject);

          
        }
        else
        {
            _master = this;
            DontDestroyOnLoad(this.gameObject);
            print("No se destruyo este objeto:" + this.gameObject);
           
        }

       
        //_masterLobby = this;

    }

    void Start()
    {
        cambiandoNivel = false;
        tickets_jugador.text = tickets.ToString("00");
        SteamVR_Fade.Start(Color.black, 0);
		SteamVR_Fade.Start(Color.clear, 1);


        //if (nuevoUsuario)
        //    NuevoUsuario();

        if (Scores_Lobby._scoreLobby != null)
        {
            Scores_Lobby._scoreLobby.CompararScore();
            print("Solicitando comparacion de scores");
        }

        
    }
    private void Update()
    {
        
        if(Input.GetKey(KeyCode.Escape))
        {
            QuitarJuego();
        }
    }

    public void CambiarNivelOperador(string n)
    {
        CambiarNivel(n, false);
    }

    public void CambiarNivel(string n,bool descontar)
    {
        if(tickets <= 0)
        {
            print("No tienes Tickets....");
            //Sonido de no tener tickets
            menuNo_sfx.Play();
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
        if (Scores_Lobby._scoreLobby != null)
            Scores_Lobby._scoreLobby.SalvarScore();
        yield return new WaitForSeconds(0.9f);
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
        menu_sfx.Play();

    }
    
    public void IngresoTickets()
    {
        tickets += int.Parse(inputTickets.text);
        tickets_jugador.text = tickets.ToString("00");

        ConteoTickets();

        inputTickets.text = "";


        if (Scores_Lobby._scoreLobby != null)
        {
            Scores_Lobby._scoreLobby.ActualizarTickets();
        }



        menu_sfx.Play();
    }

    public IEnumerator NuevoUsuario()
    {
        //QuickSaveRaw.
        yield return new WaitForSeconds(1.0f);


        QuickSaveWriter.Create("Usuarios").Commit();
        QuickSaveWriter escritor = QuickSaveWriter.Create("Usuarios");
        QuickSaveReader lector = QuickSaveReader.Create("Usuarios");

       // ConteoTickets();

        if (!lector.Exists("usuario0") && nuevoUsuario)//Si no existe el usuario0 significa que es la primera vez que se habre el juego en la PC
        {
            usuarios++;
            print("No existen usuarios en EQUIPO...creando nuevo usuario");

            escritor.Write("usuario" + usuarioNum.ToString(), usuarioNum);
            escritor.Write("tickets" + usuarioNum.ToString(), 2);//deberia ser ticketsComprados pero como no existe y es la primera vez se hardwrite
            escritor.Write("ticketsComprados", 2);
            escritor.Write("usuarios", usuarios);
            escritor.Commit();

            print("usuario" + usuarioNum.ToString());
            yield return new WaitForSeconds(1.0f);
            lector.Read<int>("usuario" + usuarioNum.ToString(), (r) => { usuarioNum = r; });
            lector.Read<int>("tickets" + usuarioNum.ToString(), (r) => { ticketsComprados = r; });
            lector.Read<int>("ticketsComprados", (r) => { ticketsComprados = r; });
            // ConteoTickets();
            nuevoUsuario = false;

        }
        else if(nuevoUsuario)
        {
            print("Nueva Sesion...Creando Usuario");

            usuarioNum += 1;
            usuarios++;
            escritor.Write("usuario" + usuarioNum.ToString(), usuarioNum);
            escritor.Write("tickets" + usuarioNum.ToString(), 2);

            

            if(usuarios == 1)
            {
                ticketsComprados = 2;
                File.WriteAllText(dataPath + "\\InfraT.txt", ticketsComprados.ToString());
            }
            else
            {
                ticketsComprados = int.Parse(File.ReadAllText(dataPath + "\\InfraT.txt"));
                ticketsComprados += tickets;
                File.WriteAllText(dataPath + "\\InfraT.txt", ticketsComprados.ToString());
            }

           

            escritor.Write("usuarios", usuarios);
            escritor.Commit();
   

            print("usuario" + usuarioNum.ToString());
            lector.Read<int>("usuario" + usuarioNum.ToString(), (r) => { usuarioNum = r; });
       
            print("Se leyeron:" + ticketsComprados);
            
            nuevoUsuario = false;

           
        
        }
     
     

    }
   
    public void ConteoTickets()
    {
       

        ticketsComprados += tickets - 2;
        File.WriteAllText(dataPath + "\\InfraT.txt", ticketsComprados.ToString());
        //QuickSaveWriter escritor = QuickSaveWriter.Create("Usuarios");
        //escritor.Write("ticketsComprados", ticketsComprados).Commit();

        // QuickSaveReader.Create("Usuarios").Read<int>("ticketsComprados", (r) => { ticketsComprados = r; });

    }

    public void NuevasSesion()
    {
        tickets = 2;
       //nuevoUsuario = true;
        SceneManager.LoadScene("Titulo_01");

    }

    public void ScoreDeSesion(string nombreJuego)
    {
        if (nombreJuego == "mina")
        {
            
        }
    }


    public void AbrirMenu()
    {
       
            menu_panel.SetActive(true);
            menuBoton.SetActive(false);
            cerrarBoton.SetActive(true);

        
      
    }
    public void CerrarMenu()
    {
        menu_panel.SetActive(false);
        menuBoton.SetActive(true);
        cerrarBoton.SetActive(false);
    }
	
    public void SalvarDatosTickets()
    {

        //Tickets default = usuarios * 2;
        //Tickets extra = Ticketscomprados - (usuarios * 2)
        int ticketsDefault = usuarios * 2;
        int ticketsExtra = ticketsComprados - ticketsDefault;
        if (ticketsExtra <= 0)
            ticketsExtra = 0;


     
        File.AppendAllText(dataPath + "\\InfraNET.txt",
                                     "\r\nTickets del " + System.DateTime.Now + " \r\n \r\n" +
                                    "Usuarios contados:" + usuarios.ToString() + " \r\n" +
                                    "Tickets Default(2):" + ticketsDefault.ToString() + " \r\n" +
                                    "Tickets Extra:" + ticketsExtra.ToString() + " \r\n" +
                                    "Tickets comprados(total,e-2): " + ticketsComprados.ToString() +"\r\n"+
                                    "_________________________________");

        



        //Esto borraria para cuando inicie de nuevo el juego empiece a contar de 0, como si fuera un nuevo dia
      //  QuickSaveWriter.Create("Usuarios").Write("ticketsComprados", 0).Commit();
        QuickSaveWriter.Create("Usuarios").Write("usuarios", 0).Commit();
    }

    public void QuitarJuego()
    {
        print("Cerrando Juego...");
        if(Scores_Lobby._scoreLobby != null)
        {
            Scores_Lobby._scoreLobby.SalvarScore();
        }

        SalvarDatosTickets();
        //Salvar datos de tickets en texto
        Application.Quit();
    }
}
