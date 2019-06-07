using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstruccionesOsos : MonoBehaviour
{
    public GameObject osoInstruccion;
    public GameObject trampa1;
    public GameObject[] instrucciones;
    int num = 0;

    public FMODUnity.StudioEventEmitter menu_sfx, menuNo_sfx;
    // Use this for initialization
    void Start()
    {
        AparecerInstruccion();
    }

    void AparecerInstruccion()
    {
        instrucciones[num].SetActive(true);
        if (num == 2)
        {
            AparecerOso();
        }
    }

    public void CerrarInstruccion()
    {
        foreach (GameObject i in instrucciones)
        {
            i.SetActive(false);
        }
        num++;
        AparecerInstruccion();
    }

    void AparecerOso()
    {
        osoInstruccion.SetActive(true);
        trampa1.SetActive(true);
    }
    public void CambiarNivel(string n)
    {
        menu_sfx.Play();
        Master._master.CambiarNivel(n,false);
      
    }
}
