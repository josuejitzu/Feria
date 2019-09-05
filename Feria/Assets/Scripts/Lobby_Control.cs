using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.SceneManagement;

public class Lobby_Control : MonoBehaviour
{
    public string lobby;
    bool cambiandoNivel;
    public FMODUnity.StudioEventEmitter menu;
	// Use this for initialization
	void Start () {
		
	}
	
	public void CambiarLobby()
    {
        StartCoroutine(CambiarScena(lobby));
        menu.Play();
    }
    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    IEnumerator CambiarScena(string n)
    {
        cambiandoNivel = true;
        if (Scores_Lobby._scoreLobby != null)
            Scores_Lobby._scoreLobby.SalvarScore();
        yield return new WaitForSeconds(0.9f);
        //fadeNegro
        SteamVR_Fade.Start(Color.black, 0.9f);

        // Wait until the asynchronous scene fully loads
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(n);
    }

}
