using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using CI.QuickSave;


public class Scores_Lobby : MonoBehaviour
{
    public static Scores_Lobby _scoreLobby;

    [Space(10)]
    [Header("Minas")]
    public TMP_Text monedasmina_text;
    public TMP_Text murcielagos_text;
    public TMP_Text monedasMinas_HighScore_text,murcielagos_HighScore_text;
    int murcielagos,monedasMinas, murcielagosHigh, monedasMinas_High;
    [Space(10)]
    [Header("Patos")]
    public TMP_Text patos_text;
    public TMP_Text monedasPatos_text, cazadores_text;
    public TMP_Text monedasPatos_HighScore_text, patos_HighScore_text,cazadoresHighScore_text;
    int patos,monedasPatos,cazadores,patosHigh, monedasPatos_High, cazadores_High;
    [Space(10)]
    [Header("Osos")]
    public TMP_Text osos_text;
    public TMP_Text monedasOsos_text, trampas_text;
    public TMP_Text ososHighScore_text, monedasOsosHighScore_text, trampasHighScore_text;
    int osos, monedasOsos,trampas,osos_High, monedasOsos_High, trampas_High;

    [Space(10)]
    [Header("Mapaches")]
    public TMP_Text basuraCorrecta_text;
    public TMP_Text basuraIncorrecta_text,monedasBasura_text;
    public TMP_Text basuraCorrectaHighScore_text, basuraIncorrectaHighScore_text, monedasBasuraHighScore_text;
    int basuraCorrecta,basuraIncorrecta,monedasBasura, basuraCorrecta_High, basuraIncorrecta_High, monedasBasura_High;
    
    [Space(10)]
    [Header("UI Operador")]
    public TMP_Text tickets_jugador;
    public TMP_InputField inputTickets;
    // Use this for initialization

    private void OnLevelWasLoaded(int level)
    {
        ActualizarMarcadores();
    }
    private void Start()
    {
        _scoreLobby = this;
        CargarScore();
        
        
    }
    // Update is called once per frame
    void FixedUpdate ()
    {
       // CompararScore();
        ActualizarMarcadores();
        if(Input.GetKeyDown(KeyCode.I))
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
        patos_text.text =patos.ToString("00") + " /30";
        monedasPatos_text.text = monedasPatos.ToString("000");
        cazadores_text.text =cazadores.ToString("00");
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

    }
    public void SalvarScore()
    {
        QuickSaveWriter.Create("HighScore").Write("murcielagoHigh", murcielagosHigh)
                                           .Write("murcielagoMonedasHigh", monedasMinas_High)
                                           .Write("patosHigh", patosHigh)
                                            .Write("monedasPatosHigh", monedasPatos_High)
                                            .Write("cazadoresHigh", cazadores_High)
                                            .Write("ososHigh", osos_High)
                                            .Write("monedasOsosHigh", monedasOsos_High)
                                            .Write("trampas", trampas_High)
                                            .Write("basuraCorrectaHigh", basuraCorrecta_High)
                                            .Write("basuraIncorrectaHigh", basuraIncorrecta_High)
                                            .Write("monedasBasuraHigh", monedasBasura_High).Commit();

        Debug.Log(Application.persistentDataPath);
        CargarScore();
        
    }
    public void CargarScore()
    {

        QuickSaveReader.Create("HighScore").Read<int>("murcielagoHigh", (r) => { murcielagosHigh = r; });
        QuickSaveReader.Create("HighScore").Read<int>("murcielagoMonedasHigh", (r) => { monedasMinas_High = r; });
        QuickSaveReader.Create("HighScore").Read<int>("patosHigh", (r) => { patosHigh = r; });
        QuickSaveReader.Create("HighScore").Read<int>("monedasPatosHigh", (r) => { monedasPatos_High = r; });
        QuickSaveReader.Create("HighScore").Read<int>("cazadoresHigh", (r) => { cazadores_High = r; });
        QuickSaveReader.Create("HighScore").Read<int>("ososHigh", (r) => { osos_High = r; });
        QuickSaveReader.Create("HighScore").Read<int>("monedasOsosHigh", (r) => { monedasOsos_High = r; });
        QuickSaveReader.Create("HighScore").Read<int>("trampas", (r) => {trampas_High= r; });
        QuickSaveReader.Create("HighScore").Read<int>("basuraCorrectaHigh", (r) => { basuraCorrecta_High = r; });
        QuickSaveReader.Create("HighScore").Read<int>("basuraIncorrectaHigh", (r) => { basuraIncorrecta_High = r; });
        QuickSaveReader.Create("HighScore").Read<int>("monedasBasuraHigh", (r) => { monedasBasura_High= r; });

        murcielagos_HighScore_text.text = murcielagosHigh.ToString("00");
        monedasMinas_HighScore_text.text = monedasMinas_High.ToString("000");
        patos_HighScore_text.text = patosHigh.ToString("00") + " /30";
        monedasPatos_HighScore_text.text = monedasPatos_High.ToString("000");
        cazadoresHighScore_text.text = cazadores_High.ToString("00");
        ososHighScore_text.text = osos_High.ToString("00") + " /30";
        monedasOsosHighScore_text.text = monedasOsos_High.ToString("000");
        trampasHighScore_text.text = trampas_High.ToString("00");
        basuraCorrectaHighScore_text.text = basuraCorrecta_High.ToString("000");
        basuraIncorrectaHighScore_text.text = basuraIncorrecta_High.ToString("000");
        monedasBasuraHighScore_text.text = monedasBasura_High.ToString("000");


    }

    public void CompararScore()
    {
        if(murcielagos > murcielagosHigh)//Gana el que mas murcielagos tiene
        {
            murcielagosHigh = murcielagos;
            monedasMinas_High = monedasMinas;
        }
        if (patos > patosHigh)//Gana el que mas patos tiene
        {
            patosHigh = patos;
            monedasPatos_High = monedasPatos;
            cazadores_High = cazadores;
        }
        if (osos > osos_High)//Gana el que mas osos tiene
        {
            osos_High = osos;
            monedasOsos_High = monedasOsos;
        }
        if (basuraCorrecta > basuraCorrecta_High)//Gana el que mas basuraCorrecta tiene
        {
            basuraCorrecta_High = basuraCorrecta;
            basuraIncorrecta_High = basuraIncorrecta;
            monedasBasura_High = monedasBasura;
        }

        SalvarScore();

    }

}
