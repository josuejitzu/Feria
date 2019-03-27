using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Master_Minas : MonoBehaviour
{
    public static Master_Minas _mina;
    [Header("Score")]
    public int score_murcielago;
    public int monedas_score;
    [Header("Tiempo")]
    public bool empezarConteo;
    public float tiempo;
    public Text tiempo_texto;
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
            empezarConteo = true;
            Carro_Control._carro.velocidadFinal = 7.0f;
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
                Carro_Control._carro.velocidadFinal = 9.0f;

            }
            else if( tiempo >= 120.0f)
            {
                /*  Rieles_Control._rieles.etapa3 = true;
                  Rieles_Control._rieles.etapa1 = false;
                  Rieles_Control._rieles.etapa2 = false;*/
                if (!Carro_Control._carro.enFinal) 
                Carro_Control._carro.velocidadFinal = 11.0f;
            }

        }


        tiempo_texto.text = (((Mathf.Floor(tiempo / 60f)) % 60).ToString("00")) + ":" + (Mathf.Floor(tiempo % 60f).ToString("00") + "." + ((tiempo * 100) % 100).ToString("00"));// % deja el restante y / sale cuanto tuvo que dividires(arriba de la casita)

    }

    public void SumarMurcielago()
    {
        score_murcielago++;
    }

    public void RestarMurcielago()
    {
        if(score_murcielago > 0)
            score_murcielago -= 1;
    }

    public void SumarMonedas()
    {
        monedas_score++;
    }
    public void FinJuego()
    {
        //pararcoche
        StartCoroutine(Carro_Control._carro.Parar());
        //Aparecer Score enfrente 
    }
}
