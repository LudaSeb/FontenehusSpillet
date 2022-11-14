using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] GameObject[] uiPanels;

    [SerializeField] GameObject rulleTerningPanel;
    [SerializeField] GameObject resultatTerningPanel;

    [SerializeField] TextMeshProUGUI energiScore;
    [SerializeField] Slider humørModifikator;

    public void OppdaterEnergi(string nyEnergi)
    {
        energiScore.text = nyEnergi + "/30";
    }

    public void SetCurrentPanel(int panelIndex)
    {
        for(int i = 0; i < uiPanels.Length; i++)
        {
            if(i == panelIndex)
            {
                uiPanels[i].SetActive(true);
            }
            else
            {
                uiPanels[i].SetActive(false);
            }
        }
    }

    public void SetHendelseskortVerdi(string beskrivelse, string vanskelighetsgrad)
    {
        TextMeshProUGUI[] tekster = uiPanels[1].GetComponentsInChildren<TextMeshProUGUI>();

        tekster[0].text = beskrivelse;
        tekster[1].text = vanskelighetsgrad;
    }

    public void SetSjansekortVerdi(string beskrivelse, string energi, string[] modifikatorer, string spørsmål)
    {
        TextMeshProUGUI[] tekster = uiPanels[2].GetComponentsInChildren<TextMeshProUGUI>();
        tekster[0].text = beskrivelse;
        tekster[1].text = energi;

        string s = "";
        for(int i = 0; i < modifikatorer.Length; i++)
        {
            s += modifikatorer[i] + "\n";
        }
        tekster[2].text = s;


        tekster[3].text = spørsmål;
    }

    public void SetVisTerningSkjerm(bool status)
    {
        Debug.Log("Vi er i vis terning: " + status);
        rulleTerningPanel.SetActive(status);
    }

    public void EndreHumørmodifikatorVerdi(int verdi)
    {
        humørModifikator.value = verdi;
    }

    public void SetVisResultatSkjerm(bool status)
    {
        
        resultatTerningPanel.SetActive(status);
    }



}
