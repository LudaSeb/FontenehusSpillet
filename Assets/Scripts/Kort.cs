using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kort
{
    public int kortTall;
    public string beskrivelse;
    public string kortType;
    public Kort(int kortTall, string beskrivelse, string kortType)
    {
        this.kortTall = kortTall;
        this.beskrivelse = beskrivelse;
        this.kortType = kortType;
    }

    public virtual void AktiverKortet()
    {
        
    }
    
}
