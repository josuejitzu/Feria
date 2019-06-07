using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instrucciones_Lago : MonoBehaviour
{
    public static Instrucciones_Lago _instruccionesLago;
    public GameObject[] instrucciones;
    int num;
    public FMODUnity.StudioEventEmitter menu_sfx, menuNo_sfx;
    // Use this for initialization
    void Start ()
    {
        _instruccionesLago = this;
        StartCoroutine( AparecerInstrucciones(0));
	}

    // Update is called once per frame
    public IEnumerator AparecerInstrucciones(int n)
    {
        foreach (GameObject i in instrucciones)
        {
            i.SetActive(false);
        }
        yield return new WaitForSeconds(1.0f);
        instrucciones[n].SetActive(true);

    }
    public void CerrarInstrucciones()
    {
        foreach (GameObject i in instrucciones)
        {
            i.SetActive(false);
        }
        if (num == 5)
        {
            StartCoroutine(Practica());
        }
        else
        {
            num++;
            StartCoroutine(AparecerInstrucciones(num));
        }
     
    }
    public IEnumerator Practica()
    {
        StartCoroutine(Master_Patos._masterPatos.IniciarJuego());
        yield return new WaitForSeconds(20);
        num++;
        StartCoroutine(AparecerInstrucciones(num));
    }
    public void CambiarNivel(string n)
    {
        menu_sfx.Play();
        Master._master.CambiarNivel(n,false);

    }
}
