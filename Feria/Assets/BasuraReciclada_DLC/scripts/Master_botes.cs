using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.SceneManagement;


public class Master_botes : MonoBehaviour
{
    public static Master_botes _masterbotes;
    public Spawn_Botes[] spawners;
    
    [Space(10)]
    [Header("UpdateScripts")]
    public List<basura_botes> basuras = new List<basura_botes>();
    public List<bote_control> botes = new List<bote_control>();
    [Space(10)]
    [Header("Score")]
    public int score;
    public TMP_Text puntos_text,puntos_final_text,puntos_high_text;
    [Space(10)]
    [Header("Keybinding")]
    public KeyCode iniciarJuego;
    [Space(10)]
    [Header("Tiempo")]
    public float tiempo;
    public TMP_Text tiempo_text;
    public TMP_Text tiempoUsuario_text;
    public bool empezarConteo;
    public GameObject play_letrero;
    public GameObject panel_final;
    [Space(10)]
    [Header("SFX")]
    public FMODUnity.StudioEventEmitter moneda_sfx;

    void Start ()
    {
        _masterbotes = this;
	}
	
	// Update is called once per frame
	void Update ()
    {
        for (int i = 0; i < botes.Count; i++)
        {
            botes[i].MiUpdate();
        }
        if(Input.GetKeyDown(iniciarJuego))
        {
            IniciarJuego();
        }

        Tiempo();


        //UI

	}

    void Tiempo()
    {
        if (empezarConteo)
        {

            tiempo -= Time.deltaTime;

            if (tiempo <= 0.0f)
            {

                empezarConteo = false;
                FinJuego();
            }
        }
        if(tiempo <= 0)
        {
            tiempo = 0;
        }

        tiempo_text.text = (((Mathf.Floor(tiempo / 60f)) % 60).ToString("00")) + ":" + (Mathf.Floor(tiempo % 60f).ToString("00") + "." + ((tiempo * 100) % 100).ToString("00"));// % deja el restante y / sale cuanto tuvo que dividires(arriba de la casita)
        tiempoUsuario_text.text = tiempo_text.text;

    }

    public void RegistrarUpdate(GameObject objeto, string tipo)
    {
        if(tipo == "basura")
        {
            basuras.Add(objeto.GetComponent<basura_botes>());
        }else if(tipo == "bote")
        {
            botes.Add(objeto.GetComponent<bote_control>());
        }
    }
    public void RemoverUpdate(GameObject objeto,string tipo)
    {
        if (tipo == "basura")
        {
            basuras.Remove(objeto.GetComponent<basura_botes>());
        }
        else if (tipo == "bote")
        {
            botes.Remove(objeto.GetComponent<bote_control>());
        }
    }

    public void IniciarJuego()
    {
        foreach(Spawn_Botes sp in spawners)
        {
            sp.spawnear = true;
        }
        Spawn_Basura._spawnBasura.spawnear = true;
        empezarConteo = true;
        play_letrero.SetActive(false);
    }

    public void Sumarpuntos(int puntos)
    {
        score += puntos;
        puntos_text.text = score.ToString("000");
        moneda_sfx.Play();
    }

    public void RestarPuntos()
    {
        if(score > 0)
            score -= 10;
        puntos_text.text = score.ToString("000");
    }

    public void FinJuego()
    {
        foreach (Spawn_Botes sp in spawners)
        {
            sp.spawnear = false;
        }
        Spawn_Basura._spawnBasura.spawnear = false;
        empezarConteo = false;
        panel_final.SetActive(true);
        puntos_final_text.text = score.ToString("000");
    }
}
