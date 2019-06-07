using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instrucciones_Raqueta : MonoBehaviour
{
    public GameObject[] instrucciones;
    int num;
    public FMODUnity.StudioEventEmitter menu_sfx, menuNo_sfx;
	// Use this for initialization
	void Start ()
    {
        AbrirInstruccion();
		
	}
	
	public void AbrirInstruccion()
    {
        
        instrucciones[num].SetActive(true);
    }
    public void CerrarInstruccion()
    {
        foreach (GameObject instruccion in instrucciones)
        {

            instruccion.SetActive(false);

        }
        num++;
        if(num == 6)
        {
            StartCoroutine(Practica());
        }
        else
        {
            AbrirInstruccion();
        }
    }
    public IEnumerator Practica()
    {
        Lanzadores_Control._lanzadores.lanzar = true;
        yield return new WaitForSeconds(15.0f);
        Lanzadores_Control._lanzadores.lanzar = false;
        yield return new WaitForSeconds(1.0f);
        AbrirInstruccion();
    }
    public void CambiarNivel(string n)
    {
        menu_sfx.Play();
        Master._master.CambiarNivel(n,false);
    }
}
