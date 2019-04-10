using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Master_Patos : MonoBehaviour
{
    public static Master_Patos _masterPatos;
    // Use this for initialization
    public float inicioJuego = 3.0f;
    public float tiempo;
    public bool empezarConteo;
    public TMP_Text tiempo_text;
    public TMP_Text tiempoUsuario_text;
    public float tiempoFinal = 170.0f;
    [Space(10)]
    [Header("Score")]
    public int patosScore;
    public int cazadoresScore;
    public int monedasScore;
    public TMP_Text patosScore_text;
    public TMP_Text cazadoresScore_text;
    public TMP_Text monedasScore_text;
    [Header("FinJuego")]
    public GameObject scoreFinal_tablero;
    public TMP_Text patos_final_txt;
    public TMP_Text monedas_final_txt;
    public TMP_Text cazadores_final_txt;

    public bool tutorial;

    void Start ()
    {
        _masterPatos = this;
        //StartCoroutine(IniciarJuego());
        tiempo = tiempoFinal;
	}
	
	// Update is called once per frame
	void Update ()
    {
        Tiempo();
		
	}

    public IEnumerator IniciarJuego()
    {
        NuevoJuego();
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

            tiempo -= Time.deltaTime;


            if (tiempo <= 0.0f)
            {

                empezarConteo = false;
                Parvada_Control._parvada.spawnear = false;
                Cazadores_Control._cazadores.spawnear = false;
                if(!tutorial)
                  FinJuego();
                
            }
            if(tiempo >= tiempoFinal)
            {
               // Parvada_Control._parvada.spawnear = false;
            }
          
        }


        tiempo_text.text = (((Mathf.Floor(tiempo / 60f)) % 60).ToString("00")) + ":" + (Mathf.Floor(tiempo % 60f).ToString("00") + "." + ((tiempo * 100) % 100).ToString("00"));// % deja el restante y / sale cuanto tuvo que dividires(arriba de la casita)
        tiempoUsuario_text.text = tiempo_text.text;
    }

    public void ScorePatos()//Tiene que ser llamado por el pato
    {
        if(patosScore > 0)
        {
            patosScore--;
            patosScore_text.text = patosScore + "/30";
        }

        if(patosScore <= 0)//Comprueba si ya llego a 0
        {
            //Perdiste
        }
    }
    public void ScoreCazadores()//llamado por el Cazador
    {
        cazadoresScore++;
        cazadoresScore_text.text = cazadoresScore.ToString("000");

    }
    public void ScoreMonedas(int n)//lamado por el cazador
    {
        monedasScore += n;
        monedasScore_text.text = monedasScore.ToString("000");

    }
    public void RestarMonedas(int n)//Tiene que ser llamado por el pato
    {
        monedasScore -= n;
        if(monedasScore < 0)
        {
            monedasScore = 0;
        }
        monedasScore_text.text = monedasScore.ToString("000");
    }

    void NuevoJuego()
    {
        patosScore_text.text = patosScore + "/30";
    }

    public void FinJuego()
    {

        scoreFinal_tablero.SetActive(true);
        patos_final_txt.text = patosScore + "/30";
        monedas_final_txt.text = monedasScore.ToString();
        cazadores_final_txt.text = cazadoresScore.ToString();
    }
    public void CambiarNivel(string n)
    {

        Master._master.CambiarNivel(n);

    }

}
