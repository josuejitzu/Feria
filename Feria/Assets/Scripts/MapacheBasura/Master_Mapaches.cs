using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Master_Mapaches : MonoBehaviour
{
    public static Master_Mapaches _masterMapaches;

    [Space(10)]
    [Header("Tiempo")]
    public float duracionJuego;
	// Use this for initialization
	void Start ()
    {
        _masterMapaches = this;	
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
    public void IniciarJuego()
    {
        Lanzadores_Control._lanzadores.lanzar = true;
    }

    public void CambiarNivel(string n)
    {
        Master._master.CambiarNivel(n);
    }
}
