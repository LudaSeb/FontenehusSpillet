using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Ruter : MonoBehaviour
{
    [SerializeField] ViseKort visekortPrototype;
    int dagNr;
    HendelsesKort kort;
    


    private void Start()
    {
            
    }

    public bool Activate()
    {
        if(kort == null)
        {
            return false;
        }
        else
        {
            kort.AktiverKortet();
            Bevegelse b = GameObject.FindGameObjectWithTag("Brikke").GetComponent<Bevegelse>();
            b.SetVanskelighetsgrad(kort.vanskelighetsgrad);
            b.SetStatusHendelsesKort();
            GameObject.FindGameObjectWithTag("UI").GetComponent<UiManager>().SetHendelseskortVerdi(kort.beskrivelse, kort.vanskelighetsgrad);

            return true;
        }
    }

    public void SetDayNumber(int dagnummer)
    {
        dagNr = dagnummer;
        GetComponentInChildren<TextMeshPro>().text = dagnummer.ToString();
    }

    public void SetKort(HendelsesKort kort)
    {
        this.kort = kort;
        ViseKort v = Instantiate(visekortPrototype, transform);

        v.transform.localPosition = new Vector3(0, .1f, 0);

        TextMeshPro[] tMP = v.GetComponentsInChildren<TextMeshPro>();

        v.SetMittKort(kort);
        //tMP[0].text = kort.beskrivelse;
        //tMP[1].text = kort.vanskelighetsgrad;
        

    }

}
