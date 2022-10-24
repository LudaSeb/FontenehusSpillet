using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SjanseKort : Kort
{
    int energi;
    int[] modifikatorer;
    string sporsmal;
    public SjanseKort(int kortTall, string beskrivelse, string kortType) : base(kortTall, beskrivelse, kortType)
    {
    }


}
