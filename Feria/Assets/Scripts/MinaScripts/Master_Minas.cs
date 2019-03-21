using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Master_Minas : MonoBehaviour
{
    public static Master_Minas _mina;
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
            Carro_Control._carro.velocidadFinal = 11.0f;
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
            }

            if(tiempo >= 60.0f &&  tiempo <= 119.0f)
            {
                Rieles_Control._rieles.etapa1 = false;
                Rieles_Control._rieles.etapa2 = true;
                Carro_Control._carro.velocidadFinal = 9.0f;

            }
            else if( tiempo >= 120.0f)
            {
                Rieles_Control._rieles.etapa3 = true;
                Rieles_Control._rieles.etapa1 = false;
                Rieles_Control._rieles.etapa2 = false;
                Carro_Control._carro.velocidadFinal = 11.0f;
            }
        }


        tiempo_texto.text = (((Mathf.Floor(tiempo / 60f)) % 60).ToString("00")) + ":" + (Mathf.Floor(tiempo % 60f).ToString("00") + "." + ((tiempo * 100) % 100).ToString("00"));// % deja el restante y / sale cuanto tuvo que dividires(arriba de la casita)

    }

}
