using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DagLagrer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tekstBoks;
    string displayTekst;
    public int[] dagRapport = new int[28];

    public void LeggTilTekstDel(string tekstDel)
    {
        displayTekst += tekstDel;
        tekstBoks.text = displayTekst;
    }

    public void ClearDisplayText()
    {
        displayTekst = "";
        tekstBoks.text = displayTekst;
    }

    public void LeggTilDagITotalen(int dag, int energi)
    {
        dagRapport[dag] = energi;
    }

    
}
