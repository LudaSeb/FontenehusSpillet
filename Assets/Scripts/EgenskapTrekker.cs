using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EgenskapTrekker : MonoBehaviour
{
    [SerializeField] Kortstokkene kortstokkene;
    [SerializeField] TMP_InputField tekstBoks;

    string[] egenskapsArray = new string[5];
    public void TrekkEgenskaper()
    {
        string egenskaper = "";

        List<Kort> kortListe = new List<Kort>();

        for(int i = 0; i < kortstokkene.personlighetsKort.Count; i++)
        {
            kortListe.Add(kortstokkene.personlighetsKort[i]);
        }

        //Debug.Log("Trukkede kort telling: " + kortListe.Count);
       
        
        List<Kort> trukkedeEgenskaper = GameObject.FindGameObjectWithTag("KortStokk").GetComponent<Kortstokk>().Trekk(5, kortListe);
        for(int i = 0; i < 5; i++)
        {
            //Debug.Log(kortListe[i].beskrivelse);
            egenskapsArray[i] = trukkedeEgenskaper[i].beskrivelse;
            
            egenskaper += $"{trukkedeEgenskaper[i].beskrivelse}\n";
        }

        tekstBoks.text = egenskaper;
        GameObject.FindGameObjectWithTag("Brikke").GetComponent<Karakter>().EndreEgenskaper(egenskapsArray);
        
        
    }
}
