using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Basura_Control : MonoBehaviour
{
    public enum TipoBasura {roja,azul,verde}
    public TipoBasura _tipoBasura;
    public enum MeshBasura {bota,cd,jugo,lata,botellaVidrio,botellaPlastico,periodico,banana,huevo,manzana,pescado}
    public MeshBasura _mesh;
    public GameObject[] meshes;

    [Space(10)]
    public SphereCollider trigger,collision;
    public Rigidbody rigid;
    public Transform boteRojo, boteVerde, boteAzul;
    Transform destino;
    public bool moverABote;
    public float velocidadBasura;
    public TrailRenderer trail;
    public Material trailAzul, trailRojo, trailVerde;
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
        yield return new WaitForSeconds(0.4f);
        //Desactivar mesh
        foreach (GameObject m in meshes)
        {
            m.SetActive(false);
        }
        yield return new WaitForSeconds(0.5f);

        this.gameObject.SetActive(false);

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "boteBasura")
        {

            // StartCoroutine(BasuraABote(other.GetComponent<Bote_Control>()._tipo.ToString()));
           // StartCoroutine(other.GetComponent<Bote_Control>().BasuraEntrando());
        }
        if(other.transform .tag == "zonaMuerte")
        {
            StartCoroutine(TirarBote());
        }
    }
    public IEnumerator BasuraABote(string tipoRaqueta)//Lo envia la raqueta que lo golpea
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
        yield return new WaitForSeconds(1.5f);
        rigid.isKinematic = true;
        moverABote = true;

    }

    void CambiarMesh()
    { //bota,cd,jugo,lata,botellaVidrio,botellaPlastico,periodico,banana,huevo,manzana,pescado}
        foreach(GameObject m in meshes)
        {
            m.SetActive(false);
        }
        if(_mesh == MeshBasura.bota)
        {
            meshes[0].SetActive(true);
        }
        if (_mesh == MeshBasura.cd)
        {
            meshes[1].SetActive(true);
        }
        if (_mesh == MeshBasura.jugo)
        {
            meshes[2].SetActive(true);
        }
        if (_mesh == MeshBasura.lata)
        {
            meshes[3].SetActive(true);
        }
        if (_mesh == MeshBasura.botellaVidrio)
        {
            meshes[5].SetActive(true);
        }
        if (_mesh == MeshBasura.botellaPlastico)
        {
            meshes[4].SetActive(true);
        }
        if (_mesh == MeshBasura.periodico)
        {
            meshes[6].SetActive(true);
        }
        if (_mesh == MeshBasura.banana)
        {
            meshes[7].SetActive(true);
        }
        if (_mesh == MeshBasura.huevo)
        {
            meshes[8].SetActive(true);
        }
        if (_mesh == MeshBasura.manzana)
        {
            meshes[9].SetActive(true);
        }
        if (_mesh == MeshBasura.pescado)
        {
            meshes[10].SetActive(true);
        }
    }
    public void SetearBasura(int n)
    {//bota,cd,jugo,lata,botellaVidrio,botellaPlastico,periodico,banana,huevo,manzana,pescado}
        if (n == 0)
        {
            _tipoBasura = TipoBasura.azul;
            trail.material = trailAzul;
            RandomMeshAzul();
        

        }else if(n == 1)
        {
            _tipoBasura = TipoBasura.roja;
            trail.material = trailRojo;
            RandomMeshRoja();
           
        }
        else if(n == 2)
        {
            _tipoBasura = TipoBasura.verde;
            trail.material = trailVerde;
            RandomMeshVerde();
            
        }
        CambiarMesh();
    }
    void RandomMeshAzul()
    {
        int r = Random.Range(4, 7);
        if(r == 4)
        {
            _mesh = MeshBasura.botellaPlastico;
        }
        else if(r == 5)
        {
            _mesh = MeshBasura.botellaVidrio;

        }
        else if(r == 6)
        {
            _mesh = MeshBasura.periodico;
        }
    }
    void RandomMeshRoja()
    {
        int r = Random.Range(0, 4);
        if (r == 0)
        {
            _mesh = MeshBasura.bota;
        }
        else if (r == 1)
        {
            _mesh = MeshBasura.cd;

        }
        else if (r == 2)
        {
            _mesh = MeshBasura.jugo;
        }
        else if (r == 3)
        {
            _mesh = MeshBasura.lata;
        }
    }
    void RandomMeshVerde()
    {
        int r = Random.Range(7, 11);
        if (r == 7)
        {
            _mesh = MeshBasura.banana;
        }
        else if (r == 8)
        {
            _mesh = MeshBasura.huevo;

        }
        else if (r == 9)
        {
            _mesh = MeshBasura.manzana;
        }
        else if (r == 10)
        {
            _mesh = MeshBasura.pescado;
        }
    }
}
