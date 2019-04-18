using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bote_Control : MonoBehaviour
{
    public enum TipoBote {verde,roja,azul};
    public TipoBote _tipo;
    public Animator bote_anim;
    public GameObject moneda_anim;
	// Use this for initialization
	void Start ()
    {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "basura")
        {
            IngresoBasura(other.GetComponent<Basura_Control>()._tipoBasura.ToString());
            StartCoroutine(BasuraEntrando());
        }
    }

    void IngresoBasura(string tipoBasura)
    {

        if(tipoBasura == _tipo.ToString())//Si es correcto
        {
            if(Master_Mapaches._masterMapaches != null)
                 Master_Mapaches._masterMapaches.SumarBasura("correcta");

            moneda_anim.SetActive(true);

        }
        else//equivocado
        {
            Master_Mapaches._masterMapaches.SumarBasura("incorrecta");
        }

    }
    public IEnumerator BasuraEntrando()
    {
        yield return new WaitForSeconds(0.7f);
        bote_anim.SetTrigger("abrirbote");
        moneda_anim.SetActive(false);
        yield return new WaitForSeconds(1.0f);
       
    }

}
