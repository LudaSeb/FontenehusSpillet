using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HendelsesKort : Kort
{
    public string vanskelighetsgrad;
    public string kortTall;
    public string beskrivelse;

    public HendelsesKort(int kortTall, string beskrivelse, string kortType, string vanskelighetsgrad) : base(kortTall, beskrivelse, kortType)
    {
        this.kortTall = kortTall.ToString();
        this.beskrivelse = beskrivelse;
        this.vanskelighetsgrad = vanskelighetsgrad;
    }

    public override void AktiverKortet()
    {
        base.AktiverKortet();
        //Debug.Log(beskrivelse);
        //Rull terning
        //Legg til vanskelighetsgrad
        //Legg til humørmodifikator
    }
}
