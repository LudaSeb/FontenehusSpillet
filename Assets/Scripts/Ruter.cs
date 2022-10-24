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
            Debug.Log("Gjør fridag");
            return false;
        }
        else
        {
            kort.AktiverKortet();
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

        tMP[0].text = kort.beskrivelse;
        tMP[1].text = kort.vanskelighetsgrad;
        

    }

}
