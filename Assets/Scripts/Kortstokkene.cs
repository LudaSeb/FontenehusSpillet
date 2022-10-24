using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kortstokkene : MonoBehaviour
{
    public List<HendelsesKort> hendelsesKort = new List<HendelsesKort>();
    public List<SjanseKort> sjanseKort = new List<SjanseKort>();
    public List<PersonlighetsKort> personlighetsKort = new List<PersonlighetsKort>();


    //Dette er for testing
    string[] vanskelighetsgrader = { "Fin", "Normal", "Kjip" };


    //public List<HendelsesKort> hendelsesKort = new List<>();

    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < 21; i++)
        {
            string vanskelighet = vanskelighetsgrader[Random.Range(0, 3)];
            hendelsesKort.Add(new HendelsesKort(i, $"Dette er beskrivelsen til kort nummer {i}", "Hendelseskort", vanskelighet));
            Debug.Log($"Korttall: {hendelsesKort[i].kortTall}, beskrivelse: {hendelsesKort[i].beskrivelse}, korttype: {hendelsesKort[i].kortType}, beskrivelse: {hendelsesKort[i].vanskelighetsgrad}");
        }   
    }

    
}
