using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using CI.QuickSave;


public class Scores_Lobby : MonoBehaviour
{
    public static Scores_Lobby _scoreLobby;
    public TMP_Text ticketsMano;
    public int ticketsJugador;
    public GameObject panelIngreso;
    
    [Space(10)]
    [Header("Minas")]
    public TMP_Text jugadorMinas_text;
    public TMP_Text correoMinas_text;
    public TMP_Text monedasmina_text;
    public TMP_Text murcielagos_text;
    public TMP_Text monedasMinas_HighScore_text, murcielagos_HighScore_text;
    public TMP_InputField nombreMinas,correoMinas_input;
    public int murcielagos, monedasMinas, murcielagosHigh, monedasMinas_High;
    public string jugadorMinas,correoMinas;
    [Space(10)]
    [Header("Patos")]
    public TMP_Text jugadorPatos_text;
    public TMP_Text correoPatos_text;
    public TMP_Text patos_text;
    public TMP_Text monedasPatos_text, cazadores_text;
    public TMP_Text monedasPatos_HighScore_text, patos_HighScore_text, cazadoresHighScore_text;
    public TMP_InputField nombrePatos,correoPato_input;
    public int patos, monedasPatos, cazadores, patosHigh, monedasPatos_High, cazadores_High;
    public string jugadorPatos,correoPatos;
    [Space(10)]
    [Header("Osos")]
    public TMP_Text jugadorOsos_text;
    public TMP_Text correoOsos_text;
    public TMP_Text osos_text;
    public TMP_Text monedasOsos_text, trampas_text;
    public TMP_Text ososHighScore_text, monedasOsosHighScore_text, trampasHighScore_text;
    public TMP_InputField nombreOsos,correoOsos_input;
    public int osos, monedasOsos, trampas, osos_High, monedasOsos_High, trampas_High;
    public string jugadorOsos,correoOsos;
    [Space(10)]
    [Header("Mapaches")]
    public TMP_Text jugadorMapaches_text;
    public TMP_Text correoMapaches_text;
    public TMP_Text basuraCorrecta_text;
    public TMP_Text basuraIncorrecta_text, monedasBasura_text;
    public TMP_Text basuraCorrectaHighScore_text, basuraIncorrectaHighScore_text, monedasBasuraHighScore_text;
    public TMP_InputField nombreMapaches,correoMapaches_input;
    public int basuraCorrecta, basuraIncorrecta, monedasBasura, basuraCorrecta_High, basuraIncorrecta_High, monedasBasura_High;
    public string jugadorMapaches, correoMapaches;

    [Space(10)]
    [Header("UI Operador")]
    public TMP_Text tickets_jugador;
    public TMP_InputField inputTickets;

    public QuickSaveSettings settingSeguridad;
    public SecurityMode tipoSeguridad;
    public string passwordScore;
    string dataPath;
    // Use this for initialization
    private void Awake()
    {
        _scoreLobby = this;
        dataPath = Application.dataPath;
    }
    private void OnLevelWasLoaded(int level)
    {
       // ActualizarMarcadores();
        _scoreLobby = this;
        //CompararScore();
    }
    private void Start()
    {
        _scoreLobby = this;
        //settingSeguridad.SecurityMode = SecurityMode.Aes;
        //settingSeguridad.Password = passwordScore;
        CargarScore();
       
        //SalvarScore();

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        // CompararScore();
        //ActualizarMarcadores();
        if (Input.GetKeyDown(KeyCode.I))
        {
            CompararScore();
        }

    }

    public void ActualizarMarcadores()
    {

        //Murcielagos
        murcielagos = Master._master.murcielagos;
        monedasMinas = Master._master.monedasMinas;
        monedasmina_text.text = monedasMinas.ToString("000");
        murcielagos_text.text = murcielagos.ToString("00");
        //Patos
        patos = Master._master.patos;
        monedasPatos = Master._master.monedasPatos;
        cazadores = Master._master.cazadores;
        patos_text.text = patos.ToString("00") + " /30";
        monedasPatos_text.text = monedasPatos.ToString("000");
        cazadores_text.text = cazadores.ToString("00");
        //Osos
        osos = Master._master.osos;
        monedasOsos = Master._master.monedasOsos;
        trampas = Master._master.trampas;
        osos_text.text = osos.ToString("00") + " /30";
        monedasOsos_text.text = monedasOsos.ToString("000");
        trampas_text.text = trampas.ToString("000");
        //Raquetas
        basuraCorrecta = Master._master.basuraCorrecta;
        basuraIncorrecta = Master._master.basuraIncorrecta;
        monedasBasura = Master._master.monedasBasura;
        basuraCorrecta_text.text = basuraCorrecta.ToString("000");
        basuraIncorrecta_text.text = basuraIncorrecta.ToString("000");
        monedasBasura_text.text = monedasBasura.ToString("000");

        ActualizarTickets();

        CompararScore();

    }
    public void ActualizarTickets()
    {
        ticketsJugador = Master._master.tickets;
        ticketsMano.text = "x " + ticketsJugador.ToString("00");
    }
    public void SalvarScore()
    {
        QuickSaveWriter.Create("HighScore").Write("murcielagoHigh", murcielagosHigh)
                                           .Write("murcielagoMonedasHigh", monedasMinas_High)
                                           .Write("jugadorMinas",jugadorMinas)
                                           .Write("patosHigh", patosHigh)
                                            .Write("monedasPatosHigh", monedasPatos_High)
                                            .Write("jugadorPatos",jugadorPatos)
                                            .Write("cazadoresHigh", cazadores_High)
                                            .Write("ososHigh", osos_High)
                                            .Write("monedasOsosHigh", monedasOsos_High)
                                            .Write("jugadorOsos",jugadorOsos)
                                            .Write("trampas", trampas_High)
                                            .Write("basuraCorrectaHigh", basuraCorrecta_High)
                                            .Write("basuraIncorrectaHigh", basuraIncorrecta_High)
                                            .Write("monedasBasuraHigh", monedasBasura_High)
                                            .Write("jugadorMapaches",jugadorMapaches).Commit();

        //Debug.Log(Application.persistentDataPath);

        //PlayerPrefs.GetInt("murcielagoHigh", murcielagosHigh);
        QuickSaveRaw.SaveString(dataPath +"\\ScoreDatos.txt",
                                "Highscores \r\n \r\n" +
                                "Caminos de la Mina \r\n" + "Nombre: "+ jugadorMinas +" Score: "+ murcielagosHigh.ToString()+ "\r\n"+"Correo: " + correoMinas+ "\r\n"+
                                "Bosque Bellota  \r\n " + "Nombre: "+ jugadorOsos +" Score: "+ osos_High.ToString()+ "\r\n" +" Correo: " + correoOsos + "\r\n"+
                                "Lago Lirio  \r\n " + "Nombre: "+ jugadorPatos +" Score: "+ patosHigh.ToString()+ "\r\n"+ " Correo: " + correoPatos+ "\r\n"+
                                "Campamaneto Mapache  \r\n " + "Nombre: "+ jugadorMapaches +" Score: "+ basuraCorrecta_High.ToString()+ "\r\n"+" Correo: " + correoMapaches + "\r\n"+
                                "Fecha:" + System.DateTime.Now);
      //  settingSeguridad.Password = passwordScore;
        print("Score exportado...");
        CargarScore();

    }
    public void CargarScore()
    {

        QuickSaveReader.Create("HighScore").Read<int>("murcielagoHigh", (r) => { murcielagosHigh = r; });
        QuickSaveReader.Create("HighScore").Read<int>("murcielagoMonedasHigh", (r) => { monedasMinas_High = r; });
        QuickSaveReader.Create("HighScore").Read<string>("jugadorMinas",(r) => { jugadorMinas = r; });

        QuickSaveReader.Create("HighScore").Read<int>("patosHigh", (r) => { patosHigh = r; });
        QuickSaveReader.Create("HighScore").Read<int>("monedasPatosHigh", (r) => { monedasPatos_High = r; });
        QuickSaveReader.Create("HighScore").Read<int>("cazadoresHigh", (r) => { cazadores_High = r; });
        QuickSaveReader.Create("HighScore").Read<string>("jugadorPatos", (r) => { jugadorPatos = r; });

        QuickSaveReader.Create("HighScore").Read<int>("ososHigh", (r) => { osos_High = r; });
        QuickSaveReader.Create("HighScore").Read<int>("monedasOsosHigh", (r) => { monedasOsos_High = r; });
        QuickSaveReader.Create("HighScore").Read<int>("trampas", (r) => { trampas_High = r; });
        QuickSaveReader.Create("HighScore").Read<string>("jugadorOsos", (r) => { jugadorOsos = r; });

        QuickSaveReader.Create("HighScore").Read<int>("basuraCorrectaHigh", (r) => { basuraCorrecta_High = r; });
        QuickSaveReader.Create("HighScore").Read<int>("basuraIncorrectaHigh", (r) => { basuraIncorrecta_High = r; });
        QuickSaveReader.Create("HighScore").Read<int>("monedasBasuraHigh", (r) => { monedasBasura_High = r; });
        QuickSaveReader.Create("HighScore").Read<string>("jugadorMapaches", (r) => { jugadorMapaches = r; });

        murcielagos_HighScore_text.text = murcielagosHigh.ToString("00");
        monedasMinas_HighScore_text.text = monedasMinas_High.ToString("000");
        jugadorMinas_text.text = jugadorMinas;

        patos_HighScore_text.text = patosHigh.ToString("00") + " /30";
        monedasPatos_HighScore_text.text = monedasPatos_High.ToString("000");
        cazadoresHighScore_text.text = cazadores_High.ToString("00");
        jugadorPatos_text.text = jugadorPatos;

        ososHighScore_text.text = osos_High.ToString("00") + " /30";
        monedasOsosHighScore_text.text = monedasOsos_High.ToString("000");
        trampasHighScore_text.text = trampas_High.ToString("00");
        jugadorOsos_text.text = jugadorOsos;

        basuraCorrectaHighScore_text.text = basuraCorrecta_High.ToString("000");
        basuraIncorrectaHighScore_text.text = basuraIncorrecta_High.ToString("000");
        monedasBasuraHighScore_text.text = monedasBasura_High.ToString("000");
        jugadorMapaches_text.text = jugadorMapaches;

        ActualizarMarcadores();

    }

    /// <summary>
    /// Es llamda por Master.cs cuando se carga el nivel
    /// </summary>
    public void CompararScore()
    {
        if (murcielagos > murcielagosHigh)//Gana el que mas murcielagos tiene
        {
            murcielagosHigh = murcielagos;
            monedasMinas_High = monedasMinas;
            ActivarIngreso("minas");
        }
        if (patos > patosHigh)//Gana el que mas patos tiene
        {
            patosHigh = patos;
            monedasPatos_High = monedasPatos;
            cazadores_High = cazadores;
            ActivarIngreso("patos");
        }
        if (osos > osos_High)//Gana el que mas osos tiene
        {
            osos_High = osos;
            monedasOsos_High = monedasOsos;
            ActivarIngreso("osos");
        }
        if (basuraCorrecta > basuraCorrecta_High)//Gana el que mas basuraCorrecta tiene
        {
            basuraCorrecta_High = basuraCorrecta;
            basuraIncorrecta_High = basuraIncorrecta;
            monedasBasura_High = monedasBasura;
            ActivarIngreso("mapaches");
        }

        //SalvarScore();

    }
    public void ActivarIngreso(string t)
    {
        panelIngreso.SetActive(true);

        nombreMinas.gameObject.SetActive(false);
        correoMapaches_input.gameObject.SetActive(false);

        nombrePatos.gameObject.SetActive(false);
        correoPato_input.gameObject.SetActive(false);

        nombreOsos.gameObject.SetActive(false);
        correoOsos_input.gameObject.SetActive(false);

        nombreMapaches.gameObject.SetActive(false);
        correoMapaches_input.gameObject.SetActive(false);


        if (t == "minas")
        {
            nombreMinas.gameObject.SetActive(true);
            correoMapaches_input.gameObject.SetActive(true);

        }else if(t == "patos")
        {
            nombrePatos.gameObject.SetActive(true);
            correoPato_input.gameObject.SetActive(true);

        }else if( t == "osos")
        {
            nombreOsos.gameObject.SetActive(true);
            correoOsos_input.gameObject.SetActive(true);

        }else if(t == "mapaches")
        {
            nombreMapaches.gameObject.SetActive(true);
            correoMapaches_input.gameObject.SetActive(true);
        }
    
    }

    public void Ingresar()
    {

        if(nombreMinas.gameObject.activeInHierarchy)
        {
            jugadorMinas = nombreMinas.text;
            correoMinas = correoMapaches_input.text;
            nombreMinas.gameObject.SetActive(false);
            correoMinas_input.gameObject.SetActive(false);
        }
        if (nombrePatos.gameObject.activeInHierarchy)
        {
            jugadorPatos = nombrePatos.text;
            correoPatos = correoPato_input.text;
            nombrePatos.gameObject.SetActive(false);
            correoPato_input.gameObject.SetActive(false);
        }
        if (nombreOsos.gameObject.activeInHierarchy)
        {
            jugadorOsos = nombreOsos.text;
            correoOsos = correoOsos_input.text;
            nombreOsos.gameObject.SetActive(false);
            correoOsos_input.gameObject.SetActive(false);
        }
        if (nombreMapaches.gameObject.activeInHierarchy)
        {
            jugadorMapaches = nombreMapaches.text;
            correoMapaches = correoMapaches_input.text;
            nombreMapaches.gameObject.SetActive(false);
            correoMapaches_input.gameObject.SetActive(false);
        }
        panelIngreso.SetActive(false);
        SalvarScore();

    }

}