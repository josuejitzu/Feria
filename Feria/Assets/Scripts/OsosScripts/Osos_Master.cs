using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Osos_Master : MonoBehaviour
{
    public static Osos_Master _masterOsos;
    public int osos_score = 30;
    public int monedas;
    public int trampas;

    public float inicioJuego = 3.0f;
    public float tiempo;
    public bool empezarConteo;
    public TMP_Text tiempo_text;
    public TMP_Text tiempoUsuario_text;
    public float tiempoFinal = 170.0f;

    [Space(10)]
    [Header("UI")]
    
    public TMP_Text osos_text;
    public TMP_Text monedas_text, trampas_text;
    public GameObject panelInicio_juego;
    public TMP_Text tickets_text;
    public int tickets;
    [Header("UI Final")]
    public TMP_Text ososFinal_text;
    public TMP_Text monedasFinal_text, trampasFinal_text;
    public GameObject panelFinal;

    public ManoOsos_Control izquierda, derecha;
    public bool finJuego;
    [Header("SFX")]
    public FMODUnity.StudioEventEmitter moneda_sfx;
    public FMODUnity.StudioEventEmitter menu_sfx,menuNo_sfx;
    // Use this for initialization
    void Start ()
    {
        _masterOsos = this;

        ActualizarTickets();
	}
	
	// Update is called once per frame
	void Update ()
    {
        Tiempo();
	}
    void Tiempo()
    {
        if (empezarConteo)
        {

            tiempo -= Time.deltaTime;


            if (tiempo <= 0.0f)
            {

                empezarConteo = false;

                OsosManada_Control._osos.spawnear = false;
                OsosManada_Control._osos.DesactivarTrampas();
                tiempo = 0.0f;
                FinJuego();

            }
            if (tiempo >= tiempoFinal)
            {
                // Parvada_Control._parvada.spawnear = false;
               
            }

        }


        tiempo_text.text = (((Mathf.Floor(tiempo / 60f)) % 60).ToString("00")) + ":" + (Mathf.Floor(tiempo % 60f).ToString("00") + "." + ((tiempo * 100) % 100).ToString("00"));// % deja el restante y / sale cuanto tuvo que dividires(arriba de la casita)
        tiempoUsuario_text.text = tiempo_text.text;
    }

    public void SumarMoneda(int n)
    {
        if (finJuego)
            return;


        monedas += n;
        if(monedas == 100)
        {

        }
        monedas_text.text = monedas.ToString("000");
        moneda_sfx.Play();
    }
    public void RestarMonedas(int n)
    {
        if (finJuego)
            return;

        monedas -= n;
        if(monedas <=0)
        {
            monedas = 0;
        }
        monedas_text.text = monedas.ToString("000");

    }
    public void SumarTrampas()
    {
        if (finJuego)
            return;
        trampas++;
        trampas_text.text = trampas.ToString("00");

    }
    public void RestarOso()
    {
        if (finJuego)
            return;
        osos_score -= 1;
        if(osos_score <= 0)
        {
            osos_score = 0;
            FinJuego();
        }
        osos_text.text = osos_score.ToString("00")+" /30";
    }
    
    public void CambiarNivel(string n)
    {
        Master._master.CambiarNivel(n,false);
        menuNo_sfx.Play();
    }
    
    public void RepetirNivel(string n)
    {
        Master._master.DescontarTicket();
        Master._master.CambiarNivel(n,true);
        menu_sfx.Play();
    }

    public void IniciarJuego()
    {
        empezarConteo = true;
        panelInicio_juego.SetActive(false);
        OsosManada_Control._osos.spawnear = true;
        MunicionBellota_Control._bellotas.spawnBellota = true;

        izquierda.puedenTomar = true;
        derecha.puedenTomar = true;
    }

    public void FinJuego()
    {
        finJuego = true;
        OsosManada_Control._osos.spawnear = false;
        MunicionBellota_Control._bellotas.spawnBellota = false;

        izquierda.puedenTomar = false;
        derecha.puedenTomar = false;
        ActualizarTickets();
        ososFinal_text.text = osos_score.ToString("00") + " /30";
        monedasFinal_text.text = monedas.ToString("000");
        trampasFinal_text.text = trampas.ToString("000");
        panelFinal.SetActive(true);

        if (Master._master != null)
        {
            Master._master.osos = osos_score;
            Master._master.trampas = trampas;
            Master._master.monedasOsos = monedas;
            if (monedas >= 100)
            {
                Master._master.SumarTicket();
            }
        }

    }

    public void ActualizarTickets()
    {
        if (Master._master == null) return;

        tickets = Master._master.tickets;
        tickets_text.text = "X " +tickets.ToString("00");
    }

        

}
