using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Master_Patos : MonoBehaviour
{

    // Use this for initialization
    public float inicioJuego = 3.0f;
    public float tiempo;
    public bool empezarConteo;
    public TMP_Text tiempo_text;
    public float tiempoFinal = 170.0f;

	void Start ()
    {
        StartCoroutine(IniciarJuego());
	}
	
	// Update is called once per frame
	void Update ()
    {
        Tiempo();
		
	}

    public IEnumerator IniciarJuego()
    {
        
        yield return new WaitForSeconds(inicioJuego);
        empezarConteo = true;
        Parvada_Control._parvada.spawnear = true;
        yield return new WaitForSeconds(3.5f);
        Cazadores_Control._cazadores.spawnear = true;

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
            if(tiempo >= tiempoFinal)
            {
                Parvada_Control._parvada.spawnear = false;
            }
          
        }


        tiempo_text.text = (((Mathf.Floor(tiempo / 60f)) % 60).ToString("00")) + ":" + (Mathf.Floor(tiempo % 60f).ToString("00") + "." + ((tiempo * 100) % 100).ToString("00"));// % deja el restante y / sale cuanto tuvo que dividires(arriba de la casita)

    }
}
