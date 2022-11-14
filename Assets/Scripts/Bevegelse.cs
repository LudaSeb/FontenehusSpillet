using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bevegelse : MonoBehaviour
{
    [SerializeField] float speedy = 1;
    [SerializeField] float LRSpeedy = 1;
    [SerializeField] float brettkantX;
    [SerializeField] float brettkantZ;

    [SerializeField] GameObject fridagPanel;
    [SerializeField] UiManager UI;

    bool canMove = true;
    bool sisteDag = false;

    float startXPosisjon;

    GameObject ruteForeldre;

    string status = "";
    int ruteNummer = -1;
    int energi = 15;
    int hum�rModifikator = 0;

    string vanskelighetsgrad = "safe";

    Ruter[] ruter;

    //string[] personlighetsTrekk = new string[5];
    //string[] personlighetsTrekk = {"stressa", "sint"};
    // Start is called before the first frame update
    void Start()
    {
        startXPosisjon = transform.position.x;
        ruteForeldre = GameObject.FindGameObjectWithTag("Ruter");

        ruter = ruteForeldre.GetComponentsInChildren<Ruter>();

        status = "Sovn";
        
    }


    public void TerningKastet()
    {
        switch (status)
        {
            case "Sovn":
                LeggTilEnergi(GetComponent<KasteBetydning>().SjekkTabell(GetComponent<Terning>().forrigeRull, "sovn"));
                Debug.Log("Energi etter s�vn er: " + energi);
                FlyttTilNesteDag();
                bool erDetKortP�Ruten;
                erDetKortP�Ruten = ruter[ruteNummer].Activate();

                if (!erDetKortP�Ruten)
                {
                    UI.SetCurrentPanel(0);
                }
                


                break;
            case "HendelsesKort":
                LeggTilEnergi(GetComponent<KasteBetydning>().SjekkTabell(RullMedHum�rmodifikator(GetComponent<Terning>().forrigeRull + hum�rModifikator), vanskelighetsgrad));
                SjekkHum�rModifikator(GetComponent<Terning>().forrigeRull);
                SetStatusNyDag();
                Debug.Log("Energi etter hendelse, med vanskelighetsgrad: " + energi.ToString() + ", " + vanskelighetsgrad);
                break;
            case "RoligDag":
                SjekkHum�rModifikator(GetComponent<Terning>().forrigeRull);
                LeggTilEnergi(GetComponent<KasteBetydning>().SjekkTabell(GetComponent<Terning>().forrigeRull, "safe"));
                SetStatusNyDag();
                Debug.Log("Energi etter dagen: " + energi);

                break;
            default:
                // code block
                break;
        }
    }

    public void TrekkSjanseKort()
    {
        SjanseKort[] sjansekort = GameObject.FindGameObjectWithTag("KortStokkene").GetComponent<Kortstokkene>().sjanseKort.ToArray();
        SjanseKort k = sjansekort[Random.Range(0, sjansekort.Length)];
        //SjanseKort k = sjansekort[1];
        Debug.Log(k.beskrivelse);
        KeyValuePair<string, int>[] m = new KeyValuePair<string, int>[k.modifikatorer.Length];
        for(int i = 0; i < k.modifikatorer.Length; i++)
        {
            string[] splittaString = k.modifikatorer[i].Split('(');

            string s = splittaString[1].Trim();
            
            m[i] = new KeyValuePair<string, int>(s.Trim(')'), int.Parse(splittaString[0]));
        }

        LeggTilEnergi(k.energi);
        EndreHum�rModifikator(k.energi);

        for (int i = 0; i < GetComponent<Karakter>().personlighetsTrekk.Length; i++)
        {
            for(int j = 0; j < m.Length; j++)
            {
                if (GetComponent<Karakter>().personlighetsTrekk[i].ToLower() == m[j].Key.ToLower())
                {
                    LeggTilEnergi(m[j].Value);
                    Debug.Log("Fikk energi Johoo!");
                    if(m[j].Value > 0)
                    {
                        Debug.Log("Var positiv");
                        GetComponent<Karakter>().SetFargeP�Egenskaper(i, new Color32(0, 255, 0, 255));
                    }
                    else if(m[j].Value < 0)
                    {
                        Debug.Log("Var negativ");
                        GetComponent<Karakter>().SetFargeP�Egenskaper(i, new Color32(255, 0, 0, 255));
                    }
                }
            }
        }

        SetStatusSjansekort(k.beskrivelse, k.energi.ToString(), k.modifikatorer, k.sporsmal);
        Debug.Log("Energi etter kort er: " + energi);

    }

    public void RullFysiskTerning()
    {
        GetComponent<Terning>().RullTerning();
        UI.SetVisTerningSkjerm(false);
        UI.SetVisResultatSkjerm(true);
        Debug.Log("Du rullet " + GetComponent<Terning>().forrigeRull + " forrige rull");
        GetComponent<Terning>().RullTerningFysisk();
    }

    public void TerningLest()
    {
        UI.SetVisResultatSkjerm(false);
        TerningKastet();
    }

    public void SetStatusSovn()
    {
        //RulleTerningRute;
        status = "Sovn";
        UI.SetCurrentPanel(3);
        UI.SetVisTerningSkjerm(true);
    }

    public void SetStatusRoligDag()
    {
        status = "RoligDag";
        UI.SetCurrentPanel(5);
        UI.SetVisTerningSkjerm(true);

    }

    public void SetStatusG�VidereTilNesteDag()
    {
        if (sisteDag)
        {

        }
        else
        {
            UI.SetCurrentPanel(4);
        }
        
    }

    public void SetStatusSjansekort(string beskrivelse, string energi, string[] modifikatorer, string sp�rsm�l)
    {
        UI.SetCurrentPanel(2);

        UI.SetSjansekortVerdi(beskrivelse, energi, modifikatorer, sp�rsm�l);
    }

    public void SetStatusHendelsesKort()
    {
        UI.SetCurrentPanel(1);
        UI.SetVisTerningSkjerm(true);
        status = "HendelsesKort";
    }

    public void SetVanskelighetsgrad(string vanskelighetsGrad)
    {
        vanskelighetsgrad = vanskelighetsGrad;
    }

    public void LeggTilEnergi(int verdi)
    {
        energi += verdi;
        UI.OppdaterEnergi(energi.ToString());
    }

    public void SetStatusNyDag()
    {
        
        UI.SetCurrentPanel(4);
    }

    void FlyttTilNesteDag()
    {
        ruteNummer += 1;
        if (!sisteDag)
        {
            if (ruteNummer == 27)
            {
                transform.position = ruter[ruteNummer].transform.position;
                sisteDag = true;
            }
            else
            {
                transform.position = ruter[ruteNummer].transform.position;
            }
            
        }
        else
        {
            Debug.Log("Hurra spillet er ferdig");
        }
        
        
    }

    void EndreHum�rModifikator(int endring)
    {
        
        if(!(hum�rModifikator + endring < -2) && !(2 < hum�rModifikator + endring))
        {
            hum�rModifikator += endring;
        }
        else
        {
            if(hum�rModifikator + endring < -2)
            {
                hum�rModifikator = -2;
            }
            else
            {
                hum�rModifikator = 2;
            }
        }

        UI.EndreHum�rmodifikatorVerdi(hum�rModifikator);
        
    }

    void SjekkHum�rModifikator(int terningKast)
    {
        Debug.Log(terningKast);
        if(terningKast > 4)
        {
            EndreHum�rModifikator(1);
        }
        else if(terningKast < 3)
        {
            EndreHum�rModifikator(-1);
        }
    }

    int RullMedHum�rmodifikator(int terningkast)
    {
        if(terningkast < 1)
        {
            return 1;
        }else if(terningkast > 6)
        {
            return 6;
        }
        else
        {
            return terningkast;
        }

    }

    

    
}

