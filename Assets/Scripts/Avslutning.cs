using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Avslutning : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI oppsumeringsRapport;
    [SerializeField] DagLagrer dagLagrer;
    private void OnEnable()
    {
        int[] dager = dagLagrer.dagRapport;
        string dagTekst = "";
        int j = 0;
        for(int i = 0; i < dager.Length; i++)
        {

            dagTekst += $"Dag {i+1}: {dager[i]}";
            if(i < dager.Length - 1)
            {
                dagTekst += ", ";
            }
            if(j == 0)
            {
                dagTekst += "\t\t\t";
                j = 1;
            }
            else if(j >= 1)
            {
                dagTekst += "\n";
                j = 0;
            }
            
        }
        oppsumeringsRapport.text = dagTekst;
    }
}
