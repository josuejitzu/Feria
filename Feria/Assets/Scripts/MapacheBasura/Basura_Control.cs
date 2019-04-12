using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Basura_Control : MonoBehaviour
{
    public enum TipoBasura {roja,azul,verde}
    public TipoBasura _tipoBasura;
    public enum MeshBasura {periodico,botella,disco}
    public MeshBasura _mesh;
    public GameObject[] meshes;

    [Space(10)]
    public SphereCollider trigger,collision;
    public Rigidbody rigid;
    public Transform boteRojo, boteVerde, boteAzul;
    Transform destino;
    public bool moverABote;
    public float velocidadBasura;
    // Use this for initialization
    private void OnValidate()
    {
        CambiarMesh();
    }
    void Start ()
    {
		
	}

    private void Update()
    {
     
        if(moverABote)
        {
            Vector3 dist = destino.position - this.transform.position;
            this.transform.position = Vector3.Lerp(this.transform.position, destino.position, Time.deltaTime * velocidadBasura);
            if(dist.magnitude <=0.3f)
            {
                moverABote = false;
                StartCoroutine(TirarBote());
            }
        }
    }

    public void ActivarBasura()
    {
        StopCoroutine(TirarBote());
        moverABote = false;
        rigid.isKinematic = false;
    }
    IEnumerator TirarBote()
    {

        trigger.enabled = true;
        rigid.isKinematic = false;
        moverABote = false;
        yield return new WaitForSeconds(1.0f);

        this.gameObject.SetActive(false);

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "boteBasura")
        {

           StartCoroutine(BasuraABote(other.GetComponent<Bote_Control>()._tipo.ToString()));
        }
        if(other.transform .tag == "zonaMuerte")
        {
            StartCoroutine(TirarBote());
        }
    }
    public IEnumerator BasuraABote(string tipoRaqueta)
    {
        if(tipoRaqueta == "verde")
        {
            destino = boteVerde;

        }else if(tipoRaqueta == "roja")
        {
            destino = boteRojo;

        }else if(tipoRaqueta == "azul")
        {
            destino = boteAzul;
        }
        trigger.enabled = false;
        yield return new WaitForSeconds(0.5f);
        rigid.isKinematic = true;
        moverABote = true;

    }

    void CambiarMesh()
    {
        foreach(GameObject m in meshes)
        {
            m.SetActive(false);
        }
        if(_mesh == MeshBasura.periodico)
        {
            meshes[0].SetActive(true);
        }
        if (_mesh == MeshBasura.botella)
        {
            meshes[1].SetActive(true);
        }
        if (_mesh == MeshBasura.disco)
        {
            meshes[2].SetActive(true);
        }
    }
    public void SetearBasura(int n)
    {
        if(n == 0)
        {
            _tipoBasura = TipoBasura.azul;
            _mesh = MeshBasura.periodico;
            CambiarMesh();

        }else if(n == 1)
        {
            _tipoBasura = TipoBasura.roja;
            _mesh = MeshBasura.botella;
            CambiarMesh();
        }
        else if(n == 2)
        {
            _tipoBasura = TipoBasura.verde;
            _mesh = MeshBasura.disco;
            CambiarMesh();
        }
    }
}
