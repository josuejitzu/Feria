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

    // Use this for initialization
    void Start ()
    {
        _masterOsos = this;
		
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
        monedas += n;
        if(monedas == 100)
        {

        }
        monedas_text.text = monedas.ToString("000");
    }
    public void RestarMonedas(int n)
    {
        monedas -= n;
        if(monedas <=0)
        {
            monedas = 0;
        }
        monedas_text.text = monedas.ToString("000");

    }
    public void SumarTrampas()
    {
        trampas++;
        trampas_text.text = trampas.ToString("00");

    }
    public void RestarOso()
    {
        osos_score -= 1;
        if(osos_score < 0)
        {
            osos_score = 0;
        }
        osos_text.text = osos_score.ToString("00");
    }
    
    public void CambiarNivel(string n)
    {
        Master._master.CambiarNivel(n);
    }

    public void IniciarJuego()
    {
        empezarConteo = true;
        OsosManada_Control._osos.spawnear = true;
        MunicionBellota_Control._bellotas.spawnBellota = true;
    }
    public void FinJuego()
    {

    }
}
