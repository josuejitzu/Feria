using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Osos_Master : MonoBehaviour
{
    public int osos_score;

    public float inicioJuego = 3.0f;
    public float tiempo;
    public bool empezarConteo;
    public TMP_Text tiempo_text;
    public TMP_Text tiempoUsuario_text;
    public float tiempoFinal = 170.0f;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
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
              
              

            }
            if (tiempo >= tiempoFinal)
            {
                // Parvada_Control._parvada.spawnear = false;
            }

        }


        tiempo_text.text = (((Mathf.Floor(tiempo / 60f)) % 60).ToString("00")) + ":" + (Mathf.Floor(tiempo % 60f).ToString("00") + "." + ((tiempo * 100) % 100).ToString("00"));// % deja el restante y / sale cuanto tuvo que dividires(arriba de la casita)
        tiempoUsuario_text.text = tiempo_text.text;
    }
}
