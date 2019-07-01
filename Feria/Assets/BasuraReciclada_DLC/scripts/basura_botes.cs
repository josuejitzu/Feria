using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basura_botes : MonoBehaviour
{
    public enum TipoBasura {organica,inorganica,reciclable}
    public TipoBasura _tipoBasura;
    public enum MeshBasura { bota, cd, jugo, lata, botellaVidrio, botellaPlastico, periodico, banana, huevo, manzana, pescado }
    public MeshBasura _mesh;
    public GameObject[] meshes;
    [Space(10)]
    [Header("VFX")]
    public ParticleSystem humo_vfx;
    public SphereCollider trigger,colision;
    // Use this for initialization
    private void OnValidate()
    {
        CambiarMesh();
    }
    void Start () {
		
	}
	
	
    
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "terreno")
        {
            StartCoroutine(Desactivar());
        }
    }
    public IEnumerator EnBote()
    {
        trigger.enabled = false;
        colision.enabled = false;
        //desactivar mesh
        foreach (GameObject m in meshes)
        {
            m.SetActive(false);
        }
        //VFX
        humo_vfx.Play();
        this.GetComponent<Rigidbody>().isKinematic = true;
        yield return new WaitForSeconds(1.0f);
        Spawn_Basura._spawnBasura.DescontarBasura();
       
        this.gameObject.SetActive(false);
    }
    public IEnumerator Desactivar()
    {
        yield return new WaitForSeconds(2.0f);
        Spawn_Basura._spawnBasura.DescontarBasura();
        this.GetComponent<Rigidbody>().isKinematic = true;
        this.gameObject.SetActive(false);
    }
    void CambiarMesh()
    { //bota,cd,jugo,lata,botellaVidrio,botellaPlastico,periodico,banana,huevo,manzana,pescado}
        foreach (GameObject m in meshes)
        {
            m.SetActive(false);
        }
        if (_mesh == MeshBasura.bota)
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
            _tipoBasura = TipoBasura.reciclable;
          //  trail.material = trailAzul;
            RandomMeshAzul();


        }
        else if (n == 1)
        {
            _tipoBasura = TipoBasura.inorganica;
          //  trail.material = trailRojo;
            RandomMeshRoja();

        }
        else if (n == 2)
        {
            _tipoBasura = TipoBasura.organica;
            //trail.material = trailVerde;
            RandomMeshVerde();

        }
        this.GetComponent<Rigidbody>().isKinematic = false;
        trigger.enabled = true;
        colision.enabled = true;
        CambiarMesh();
    }
    void RandomMeshAzul()
    {
        int r = Random.Range(4, 7);
        if (r == 4)
        {
            _mesh = MeshBasura.botellaPlastico;
        }
        else if (r == 5)
        {
            _mesh = MeshBasura.botellaVidrio;

        }
        else if (r == 6)
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
