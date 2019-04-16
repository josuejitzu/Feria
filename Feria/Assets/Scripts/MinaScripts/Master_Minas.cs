using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Master_Minas : MonoBehaviour
{
    public static Master_Minas _mina;
    [Space(10)]
    [Header("Tickes")]
    public int tickets_ganados;
    [Header("Score")]
    public int murcielago_score;
    public int monedas_score;
    [Header("Tiempo")]
    public bool empezarConteo;
    public float tiempo;
    public Text tiempo_texto;
    [Space(10)]
    [Header("UI")]
    public TMP_Text murcielagos_txt;
    public TMP_Text monedas_txt;
    public TMP_Text trampas_txt;
    public GameObject panelInicio;
    [Header("ScoreFinal")]
    public GameObject scoreFinal_tablero;
    public TMP_Text murcielagos_final_txt;
    public TMP_Text monedas_final_txt;
    public TMP_Text trampas_final_txt;

    // Use this for initialization
    void Start ()
    {
        _mina = this;
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            IniciarJuego();
        }

        Tiempo();

	}

    void Tiempo()
    {
        if (empezarConteo)
        {

            tiempo += Time.deltaTime;


            if (tiempo <= 0.0f)
            {

                empezarConteo = false;
            }else if(tiempo >= 150)
            {
                Rieles_Control._rieles.etapafinal = true;
            }

           if(tiempo >= 60.0f &&  tiempo <= 119.0f)
            {
             /*   Rieles_Control._rieles.etapa1 = false;
                Rieles_Control._rieles.etapa2 = true;*/
                Carro_Control._carro.velocidadFinal = 10.5f;

            }
            else if( tiempo >= 120.0f)
            {
                /*  Rieles_Control._rieles.etapa3 = true;
                  Rieles_Control._rieles.etapa1 = false;
                  Rieles_Control._rieles.etapa2 = false;*/
                if (!Carro_Control._carro.enFinal) 
                Carro_Control._carro.velocidadFinal = 12.0f;
            }

        }


        tiempo_texto.text = (((Mathf.Floor(tiempo / 60f)) % 60).ToString("00")) + ":" + (Mathf.Floor(tiempo % 60f).ToString("00") + "." + ((tiempo * 100) % 100).ToString("00"));// % deja el restante y / sale cuanto tuvo que dividires(arriba de la casita)

    }

    public void SumarMurcielago()
    {
        murcielago_score++;
        murcielagos_txt.text = murcielago_score.ToString("00");
    }

    public void RestarMurcielago()
    {
        if(murcielago_score > 0)
            murcielago_score -= 1;

        murcielagos_txt.text = murcielago_score.ToString("00");
    }

    public void SumarMonedas()
    {
        monedas_score++;
        if(monedas_score == 100)
        {
            SumarTicket();
            monedas_score = 0;
        }

        monedas_txt.text = monedas_score.ToString("000");
    }
    public void RestarMonedas(int n)
    {
        monedas_score -= n;

        if(monedas_score <0)
        {
            monedas_score = 0;
        }
        monedas_txt.text = monedas_score.ToString("000");

    }

    public void FinJuego()
    {

        //pararcoche
        StartCoroutine(Carro_Control._carro.Parar());
        murcielagos_final_txt.text = murcielago_score.ToString();
        monedas_final_txt.text = monedas_score.ToString();

        scoreFinal_tablero.SetActive(true);
       // trampas_final_txt =;
       //Aparecer Score enfrente 
       if(Master._master != null)
        {
            Master._master.monedasMinas = monedas_score;
            Master._master.murcielagos = murcielago_score;
        }


     }

    public void IniciarJuego()
    {
        empezarConteo = true;
        panelInicio.SetActive(false);
        Carro_Control._carro.velocidadFinal = 9.0f;
    }

    public void SumarTicket()
    {
        tickets_ganados++;
    }
    public void CambiarNivel(string n)
    {
        
            Master._master.CambiarNivel(n);
        
    }
}
