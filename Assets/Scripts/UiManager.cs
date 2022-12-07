using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
//using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] GameObject[] uiPanels;

    [SerializeField] GameObject rulleTerningPanel;
    [SerializeField] GameObject resultatTerningPanel;
    [SerializeField] GameObject menyPanel;

    [SerializeField] TextMeshPro energiScore;
    [SerializeField] TextMeshPro hum�rModifikator;
    [SerializeField] TextMeshProUGUI n�dKortBeskrivelse;
    [SerializeField] TextMeshProUGUI rullVerdi;

    bool kanByttePanel = true;
    //[SerializeField] Slider hum�rModifikator;

    public void OppdaterEnergi(string nyEnergi)
    {
        energiScore.text = nyEnergi + "/30";
    }

    public void LeggTilRullInfo(string s)
    {
        rullVerdi.text += s;
    }

    public void OppdaterN�dKortSkjerm(string t)
    {
        n�dKortBeskrivelse.text = t;
    }

    public void VisMeny()
    {
        menyPanel.SetActive(true);
    }

    public void SetCurrentPanel(int panelIndex)
    {
        if (kanByttePanel)
        {
            for (int i = 0; i < uiPanels.Length; i++)
            {
                if (i == panelIndex)
                {
                    uiPanels[i].SetActive(true);
                }
                else
                {
                    uiPanels[i].SetActive(false);
                }
            }
        }
        
    }

    public void SetKanByttePanel(bool state)
    {
        kanByttePanel = state;
    }

    public void SetHendelseskortVerdi(string beskrivelse, string vanskelighetsgrad, string kortnummer)
    {
        TextMeshProUGUI[] tekster = uiPanels[1].GetComponentsInChildren<TextMeshProUGUI>();

        tekster[0].text = beskrivelse;
        tekster[1].text = vanskelighetsgrad;

        uiPanels[1].GetComponentInChildren<HentBilde>().FinnBilde(kortnummer);
    }

    public void SetSjansekortVerdi(string beskrivelse, string energi, string[] modifikatorer, string sp�rsm�l, string kortnummer)
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


        tekster[3].text = sp�rsm�l;

        uiPanels[2].GetComponentInChildren<HentBilde>().FinnBilde(kortnummer);

        
    }

    public void SetVisTerningSkjerm(bool status)
    {
        Debug.Log("Vi er i vis terning: " + status);
        rulleTerningPanel.SetActive(status);
    }

    public void EndreHum�rmodifikatorVerdi(int verdi)
    {
        string nyHModTekst = "Jeg f�ler meg ";
        
        switch (verdi)
        {
            case -2:
                nyHModTekst += "veldig kjip";
                break;
            case -1:
                nyHModTekst += "kjip";
                break;
            case 0:
                nyHModTekst += "ok";
                break;
            case 1:
                nyHModTekst += "bra";
                break;
            case 2:
                nyHModTekst += "veldig bra";
                break;
            default:
                break;
        }
        
        hum�rModifikator.text = nyHModTekst;
        
        
    }

    public void SetVisResultatSkjerm(bool status)
    {
        
        resultatTerningPanel.SetActive(status);
    }



}
