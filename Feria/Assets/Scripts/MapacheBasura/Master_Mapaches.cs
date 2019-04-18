﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Master_Mapaches : MonoBehaviour
{
    public static Master_Mapaches _masterMapaches;

    [Space(10)]
    [Header("Tiempo")]
    public float duracionJuego;
    bool empezarConteo;
    public TMP_Text tiempo_text;
    public TMP_Text tiempoUsuario_text;
    

    [Space(10)]
    [Header("Score")]
    public int basuraCorrecta;
    public int basuraIncorrecta;
    public int monedas;

    [Space(10)]
    [Header("UI")]
    public GameObject panelInicio;
    public TMP_Text basuraCorrecta_text, basuraIncorrecta_text, monedas_text;
    [Header("UI Final")]
    public GameObject panelFinal;
    public TMP_Text basuraCorrectaFinal_text, basuraIncorrectaFinal_text, monedasFinal_text;


    // Use this for initialization
    void Start ()
    {
        _masterMapaches = this;
        
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

            duracionJuego -= Time.deltaTime;

            if(duracionJuego <= 120.0f)
            {
                Lanzadores_Control._lanzadores.CambiarVelocidad(2);
            }else if(duracionJuego <= 60.0f)
            {
                Lanzadores_Control._lanzadores.CambiarVelocidad(3);
            }
            if (duracionJuego <= 0.0f)
            {

                empezarConteo = false;
                FinJuego();
               

            }
            

        }


        tiempo_text.text = (((Mathf.Floor(duracionJuego / 60f)) % 60).ToString("00")) + ":" + (Mathf.Floor(duracionJuego % 60f).ToString("00") + "." + ((duracionJuego * 100) % 100).ToString("00"));// % deja el restante y / sale cuanto tuvo que dividires(arriba de la casita)
        tiempoUsuario_text.text = tiempo_text.text;
    }

    public void SumarBasura(string tipo)
    {
        if(tipo == "correcta")
        {
            basuraCorrecta++;
            SumarMoneda(5);
        }else if(tipo == "incorrecta")
        {
            basuraIncorrecta++;
            RestarMoneda(10);
        }
        basuraCorrecta_text.text = basuraCorrecta.ToString("000");
        basuraIncorrecta_text.text = basuraIncorrecta.ToString("000");
    }
    public void RestarBasura(string tipo)
    {
        if (tipo == "correcta")
        {
            basuraCorrecta--;
        }
        else if (tipo == "incorrecta")
        {
            basuraIncorrecta--;
        }
    }
    public void SumarMoneda(int n)
    {
        monedas += n;
        monedas_text.text = monedas.ToString("000");
    }
    public void RestarMoneda(int n)
    {
        monedas -= n;
        if(monedas < 0)
        {
            monedas = 0;
        }

        monedas_text.text = monedas.ToString("000");
    }


    public void IniciarJuego()
    {
        empezarConteo = true;
        panelInicio.SetActive(false);
        Lanzadores_Control._lanzadores.lanzar = true;
    }

    public void CambiarNivel(string n)
    {
        Master._master.CambiarNivel(n,false);
    }
    public void RepetirJuego(string n)
    {
        Master._master.CambiarNivel(n, true);
    }
    public void FinJuego()
    {
       
        panelFinal.SetActive(true);
        basuraCorrectaFinal_text.text = basuraCorrecta.ToString("000");
        basuraIncorrectaFinal_text.text = basuraIncorrecta.ToString("000");
        monedasFinal_text.text = monedas.ToString("000");
        Lanzadores_Control._lanzadores.lanzar = false;

        if (Master._master != null)
        {
            Master._master.basuraCorrecta = basuraCorrecta;
            Master._master.basuraIncorrecta = basuraIncorrecta;
            Master._master.monedasBasura = monedas;
            if (monedas >= 100)
            {
                Master._master.SumarTicket();
            }
        }
    }
}
