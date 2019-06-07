using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstruccionesMinas_Control : MonoBehaviour
{
    public static InstruccionesMinas_Control _instruccionesMinas;
    public  GameObject[] instrucciones;
    public FMODUnity.StudioEventEmitter menu_sfx, menuNo_sfx;
	// Use this for initialization
	void Start ()
    {
        _instruccionesMinas = this;	
	}
	
	
    public void AparecerInstrucciones(int num)
    {
        foreach(GameObject i in instrucciones)
        {
            i.SetActive(false);
        }
        Carro_Control._carro.velocidadFinal = 0.0f;
        Carro_Control._carro.puedeMoverse = false;
        instrucciones[num].SetActive(true);

    }
    public void CerrarInstrucciones()
    {
        foreach (GameObject i in instrucciones)
        {
            i.SetActive(false);
        }
        Carro_Control._carro.velocidadFinal = 5.0f;
        Carro_Control._carro.puedeMoverse = true;

    }
    public void CambiarNivel(string n)
    {
        menu_sfx.Play();
        Master._master.CambiarNivel(n,false);

    }
}
