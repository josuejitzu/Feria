using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.SceneManagement;
using CI.QuickSave;

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
    public int highscore;
    public string jugador_high;
    public TMP_Text puntos_text,puntos_final_text,puntos_high_text;
    public TMP_Text score_panel,jugadorHigh_text,score_controlador;
    public TMP_Text nuevoRecord;
    public TMP_InputField inputJugador;
    public GameObject panelInputJugador;
    [Space(10)]
    [Header("Keybinding")]
    public KeyCode iniciarJuego;
    public GameObject panelMenu;
    public GameObject botonMenu;
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
    public FMODUnity.StudioEventEmitter incorrecto_sfx;
    public FMODUnity.StudioEventEmitter inicio_sfx;
    public string nivel;

    public bool jugando;

    void Start ()
    {
        _masterbotes = this;
        CargarScore();
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

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
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
        jugando = true;
        foreach(Spawn_Botes sp in spawners)
        {
            sp.spawnear = true;
        }
        Spawn_Basura._spawnBasura.spawnear = true;
        empezarConteo = true;
        play_letrero.SetActive(false);
        inicio_sfx.Play();
    }

    public void Sumarpuntos(int puntos)
    {
        if (!jugando)
            return;

        score += puntos;
        puntos_text.text = score.ToString("000");
        score_controlador.text = "Score: " + score.ToString("000");
        moneda_sfx.Play();
    }

    public void RestarPuntos()
    {
        if (!jugando)
            return;

        if(score > 0)
            score -= 10;
        puntos_text.text = score.ToString("000");
        score_controlador.text = "Score: " + score.ToString("000");
        incorrecto_sfx.Play();

    }

    public void FinJuego()
    {
        inicio_sfx.Play();
        jugando = false;
        foreach (Spawn_Botes sp in spawners)
        {
            sp.spawnear = false;
            sp.FinJuego();
        }
        Spawn_Basura._spawnBasura.spawnear = false;
        empezarConteo = false;
        panel_final.SetActive(true);
        puntos_final_text.text = score.ToString("000");
        puntos_high_text.text = highscore.ToString("000");
        jugadorHigh_text.text = jugador_high;
        CompararScore();
    }
    
    //Score
    public void CargarScore()
    {

        //QuickSaveRoot.Delete("ScoreBasura");
        if (QuickSaveRoot.Exists("ScoreBasura"))
        {
            print("Se encontro archivo de guardado, leyendo...");
            QuickSaveReader lector = QuickSaveReader.Create("ScoreBasura");
            highscore = lector.Read<int>("highscore");
            jugador_high = lector.Read<string>("jugador");
            
            
        }
        else
        {
            print("No se encontro archivo de guardado crear");
            QuickSaveWriter.Create("ScoreBasura").Write("highscore",highscore)
                                                 .Write("jugador",jugador_high).Commit();

        }

        ActualizarTableros();

    }

    public void SalvarScore()
    {
        QuickSaveWriter.Create("ScoreBasura").Write("highscore", highscore)
                                                .Write("jugador", jugador_high).Commit();

        CargarScore();

        print("Se salvo score");
    }

    void ActualizarTableros()
    {
        //Tablero en escena
        puntos_text.text = score.ToString("000");
        //Tablero final
        puntos_final_text.text = highscore.ToString("000");
        jugadorHigh_text.text = jugador_high;
        puntos_high_text.text = highscore.ToString("000");

        //Tablero Controlador
        score_panel.text = jugador_high + "  " + highscore.ToString("000");
        score_controlador.text = "Score: " + score.ToString("000");

    }

    void CompararScore()
    {
        if(score > highscore)
        {
            highscore = score;
            nuevoRecord.gameObject.SetActive(true);
            InputJugador();
        }
    }

    public void InputJugador()
    {
        if (!panelInputJugador.activeInHierarchy)
        {
            panelInputJugador.SetActive(true);
        }
        else if (panelInputJugador.activeInHierarchy)
        {
            panelInputJugador.SetActive(false);
            jugador_high = inputJugador.text;
            nuevoRecord.gameObject.SetActive(false);
            highscore = score;
            SalvarScore();
        }
    }

    public void ReiniciarNivel()
    {
        SceneManager.LoadScene(nivel);
    }

    public void AbrirMenu()
    {
        if(panelMenu.activeInHierarchy)
        {
            panelMenu.SetActive(false);
            botonMenu.SetActive(true);
        }else if(!panelMenu.activeInHierarchy)
        {
            panelMenu.SetActive(true);
            botonMenu.SetActive(false);
        }
    }

}
