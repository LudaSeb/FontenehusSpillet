using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ViseKort : MonoBehaviour
{
    HendelsesKort mittKort;
    public void SetMittKort(Kort mittKort)
    {
        this.mittKort = (HendelsesKort)mittKort;
        TextMeshPro[] alleTekstenePaaEtKort = GetComponentsInChildren<TextMeshPro>();
        
        alleTekstenePaaEtKort[0].text = this.mittKort.beskrivelse;
        alleTekstenePaaEtKort[1].text = this.mittKort.vanskelighetsgrad;

        //
        /*HentBilde hb = GetComponentInChildren<HentBilde>();
        if(hb != null)
        {
            hb.FinnBilde(this.mittKort.kortTall);
        }
        else
        {
        }*/
        
    }




}
