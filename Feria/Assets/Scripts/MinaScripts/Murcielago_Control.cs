using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Murcielago_Control : MonoBehaviour
{
    public ParticleSystem explosionFx;
    public GameObject meshMurcielago;
	// Use this for initialization
	void Start ()
    {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "carro")
        {
            if(Master_Minas._mina != null)
              Master_Minas._mina.SumarMurcielago();
            meshMurcielago.SetActive(false);
            explosionFx.Play();
            StartCoroutine(Reinicio());
        }

    }
    public IEnumerator Reinicio()
    {
        yield return new WaitForSeconds(0.5f);
        meshMurcielago.SetActive(true);
        this.gameObject.SetActive(false);
    }

}
