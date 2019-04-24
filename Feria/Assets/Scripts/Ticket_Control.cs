using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ticket_Control : MonoBehaviour
{

    public GameObject ticket_anim;

    public void QuitarTicket()
    {
        if(Master._master.tickets > 0)
          ticket_anim.SetActive(true);
    }
}
