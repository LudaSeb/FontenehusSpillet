using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NødstiltakKort : Kort
{
    public int dager;
    public int energiEtterOpphold;

    public NødstiltakKort(int kortTall, string beskrivelse, string kortType, int dager, int energiEtterOpphold) : base(kortTall, beskrivelse, kortType)
    {
        this.kortTall = kortTall;
        this.beskrivelse = beskrivelse;
        this.dager = dager;
        this.energiEtterOpphold = energiEtterOpphold;
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
