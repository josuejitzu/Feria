using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivadorInstrucciones : MonoBehaviour {

    // Use this for initialization
    public int instrucciones;
	void Start () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "carro")
        {
            Carro_Control._carro.puedeMoverse = false;
            InstruccionesMinas_Control._instruccionesMinas.AparecerInstrucciones(instrucciones);
            this.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
