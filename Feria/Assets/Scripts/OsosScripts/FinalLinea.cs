using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider),typeof(Rigidbody))]
public class FinalLinea : MonoBehaviour
{
    public LineaControl linea;

	// Use this for initialization
	void Start () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "oso")
        {
            linea.ActivarTrampas();
        }
    }
}
