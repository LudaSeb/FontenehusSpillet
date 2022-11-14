using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SjanseKort : Kort
{
    public int energi;
    public string[] modifikatorer;
    public string sporsmal;
    public SjanseKort(int kortTall, string beskrivelse, string kortType, int energi, string[] modifikatorer, string sporsmal) : base(kortTall, beskrivelse, kortType)
    {
        this.kortTall = kortTall;
        this.beskrivelse = beskrivelse;
        this.kortType = kortType;
        this.energi = energi;
        this.modifikatorer = modifikatorer;
        this.sporsmal = sporsmal;
    }


}
