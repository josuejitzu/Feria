using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bote_Control : MonoBehaviour
{
    public enum TipoBote {verde,roja,azul};
    public TipoBote _tipo;
	// Use this for initialization
	void Start () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "basura")
        {
            //IngresoBasura(other.GetComponent<Basura_Control>()._tipoBasura.ToString());
        }
    }

    void IngresoBasura(string tipoBasura)
    {

        if(tipoBasura == _tipo.ToString())//Si es correcto
        {

        }else//equivocado
        {

        }

    }

}
