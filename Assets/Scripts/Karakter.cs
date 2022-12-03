using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Karakter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI navneFelt;

    public string navn;
    public string alder;
    public string[] personlighetsTrekk;
    public string[] kommentarer;

    [SerializeField]TextMeshProUGUI[] displayEgenskaper = new TextMeshProUGUI[5];

    public void EndreNavn()
    {
        navn = navneFelt.text;
    }

    public void EndreAlder(string alder)
    {
        this.alder = alder;
    }

    public void EndreEgenskaper(string [] egenskaper)
    {
        personlighetsTrekk = egenskaper;
        
        for (int i = 0; i < egenskaper.Length; i++)
        {
            displayEgenskaper[i].text = egenskaper[i];
        }

    }

    public void SetFargeP�Egenskaper(int i, Color32 farge)
    {
        //Debug.Log("Er inne i farge p� egenskaper: " + farge);
        displayEgenskaper[i].color = farge;
    }

    public void ResetFargeP�Egenskaper()
    {
        for(int i = 0; i < displayEgenskaper.Length; i++)
        {
            SetFargeP�Egenskaper(i, new Color32(0, 0, 0, 255));
        }
    }








}
