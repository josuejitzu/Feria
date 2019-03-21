using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Techo_Control : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "carro")
        {
            StartCoroutine(Reiniciar());
        }

    }
    public IEnumerator Reiniciar()
    {
        yield return new WaitForSeconds(3.0f);
        this.gameObject.SetActive(false);
    }
}
