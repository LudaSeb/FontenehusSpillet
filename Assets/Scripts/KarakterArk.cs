using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KarakterArk : MonoBehaviour
{
    [SerializeField] GameObject g�VidereKnapp;

    bool[] ferdigSjekk = {false, false, false };

    public void MeldFraOmFerdighet(int i)
    {
        ferdigSjekk[i] = true;
        for(int j = 0; j < ferdigSjekk.Length; j++)
        {
            if(ferdigSjekk[j] == false)
            {
                return;
            }
        }

        g�VidereKnapp.SetActive(true);
    }


}
