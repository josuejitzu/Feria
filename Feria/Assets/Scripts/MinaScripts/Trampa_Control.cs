using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Trampa_Control : MonoBehaviour
{
    public enum TipoTrampa {barrera,piedras,viga};
    public TipoTrampa _tipoTrampa;
    public GameObject[] trampas_mesh;
    public BoxCollider[] triggers_trampas;
    // Use this for initialization
    private void OnValidate()
    {
        CambiarTrampa();
    }
    public void CambiarTrampa()
    {
        //Ocultamos todas las trampas
        foreach(GameObject t in trampas_mesh)
        {
            t.SetActive(false);
        }
        foreach(BoxCollider b in triggers_trampas)
        {
            b.enabled = false;
        }

        if(_tipoTrampa == TipoTrampa.barrera)
        {
            trampas_mesh[0].SetActive(true);
            triggers_trampas[0].enabled = true;
        }else if(_tipoTrampa == TipoTrampa.piedras)
        {
            trampas_mesh[1].SetActive(true);
            triggers_trampas[1].enabled = true;
        }
        else if(_tipoTrampa == TipoTrampa.viga)
        {
            trampas_mesh[2].SetActive(true);
            triggers_trampas[2].enabled = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "carro")
        {
            foreach (GameObject b in trampas_mesh)
            {
                b.SetActive(false);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.transform.tag == "carro")
        {
            //Carro_Control._carro.inmortal = false;
           
            
        }
    }
    public IEnumerator PausarCollider()
    {
        foreach(BoxCollider b in triggers_trampas)
        {
            b.enabled = false;
        }
        yield return new WaitForSeconds(4.0f);
        foreach (BoxCollider b in triggers_trampas)
        {
            b.enabled = true;
        }
    }


}
