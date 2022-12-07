using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bevegelse : MonoBehaviour
{
    [SerializeField] float speedy = 1;
    [SerializeField] float LRSpeedy = 1;
    [SerializeField] float brettkantX;
    [SerializeField] float brettkantZ;
    [SerializeField] float brikkeFart;

    [SerializeField] GameObject fridagPanel;
    [SerializeField] UiManager UI;
    [SerializeField] DagLagrer dagLagrer;

    bool canMove = true;
    bool sisteDag = false;

    float startXPosisjon;

    GameObject ruteForeldre;

    string status = "";
    [SerializeField]int ruteNummer = -1;
    [SerializeField]int energi = 15;
    int hum�rModifikator = 0;
    int forrigeHum�rModifikator = 0;

    string vanskelighetsgrad = "safe";

    Ruter[] ruter;

    N�dstiltakKort aktivtNodKort;

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
                int energiLagtTil = GetComponent<KasteBetydning>().SjekkTabell(GetComponent<Terning>().forrigeRull, "sovn");

                dagLagrer.LeggTilTekstDel("Energi etter s�vn: " + energi + " + " + energiLagtTil + " = " + (energi + energiLagtTil) +  "\n");
                LeggTilEnergi(energiLagtTil);

                FlyttTilNesteDag();
                

                break;
            case "HendelsesKort":
                //UI.LeggTilRullInfo($" {hum�rModifikator}({HumorModifikatorTekstVerdi(hum�rModifikator)})");
                int rull = GetComponent<Terning>().forrigeRull;
                int energiViSkalLeggeTil = GetComponent<KasteBetydning>().SjekkTabell(RullMedHum�rmodifikator(rull + hum�rModifikator), vanskelighetsgrad);
                
                int humorForRull = hum�rModifikator;
                
                int humorEtterRull = SjekkHum�rModifikator(GetComponent<Terning>().forrigeRull);

                dagLagrer.LeggTilTekstDel("Energi etter dagens hendelse: " + energi + " + " + energiViSkalLeggeTil + " = " + (energi + energiViSkalLeggeTil) + "\n" +
                    $"Rull: {rull} med hum�r {HumorModifikatorTekstVerdi(forrigeHum�rModifikator)}({forrigeHum�rModifikator})\n" 
                    + "Hum�rmodifikator var : " + humorForRull + " og er n�: " + humorEtterRull + "\n");

                LeggTilEnergi(energiViSkalLeggeTil);

                SetStatusNyDag();

                //Debug.Log("Energi etter hendelse, med vanskelighetsgrad: " + energi.ToString() + ", " + vanskelighetsgrad);
                break;
            case "RoligDag":
                int forrigeRull = GetComponent<Terning>().forrigeRull;
                int energiLagtTilRoligDag = GetComponent<KasteBetydning>().SjekkTabell(forrigeRull, "safe");

                
                dagLagrer.LeggTilTekstDel($"Energi etter rolig dag: {energi} + {energiLagtTilRoligDag} = {energi + energiLagtTilRoligDag} \n" +
                    $"Rull : {forrigeRull}\n");
                    
                LeggTilEnergi(energiLagtTilRoligDag);

                //SjekkHum�rModifikator(GetComponent<Terning>().forrigeRull);
                SetStatusNyDag();
                //Debug.Log("Energi etter dagen: " + energi);

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
        
        KeyValuePair<string, int>[] m = new KeyValuePair<string, int>[k.modifikatorer.Length];
        for (int i = 0; i < k.modifikatorer.Length; i++)
        {
            string[] splittaString = k.modifikatorer[i].Split('(');

            string s = splittaString[1].Trim();

            m[i] = new KeyValuePair<string, int>(s.Trim(')'), int.Parse(splittaString[0]));
        }

        //dagLagrer.LeggTilTekstDel("Energi etter sjansekort: " + energi + " + " + k.energi + " = " + (energi + k.energi) + "\n");


        int totalEnergiFraModifikatorer = 0;

        for (int i = 0; i < GetComponent<Karakter>().personlighetsTrekk.Length; i++)
        {
            for (int j = 0; j < m.Length; j++)
            {
                if (GetComponent<Karakter>().personlighetsTrekk[i].ToLower() == m[j].Key.ToLower())
                {
                    totalEnergiFraModifikatorer += m[j].Value;

                    if (m[j].Value > 0)
                    {
                        GetComponent<Karakter>().SetFargeP�Egenskaper(i, new Color32(0, 255, 0, 255));
                    }
                    else if (m[j].Value < 0)
                    {
                        GetComponent<Karakter>().SetFargeP�Egenskaper(i, new Color32(255, 0, 0, 255));
                    }
                }
            }
        }

        int humorModFor = hum�rModifikator;
        int humorModEtter = EndreHum�rModifikator(k.energi);

        dagLagrer.LeggTilTekstDel($"Energi etter sjansekort: {energi} + grunnenergi({k.energi}) + modifikatorenergi({totalEnergiFraModifikatorer}) = {energi + k.energi + totalEnergiFraModifikatorer} \n Hum�rmodifikator var {humorModFor} og er n� {humorModEtter}");

        
        

        SetStatusSjansekort(k.beskrivelse, k.energi.ToString(), k.modifikatorer, k.sporsmal, k.kortTall.ToString());
        //Debug.Log("Energi etter kort er: " + energi);
        LeggTilEnergi(k.energi + totalEnergiFraModifikatorer);

    }

    public void RullFysiskTerning()
    {
        //GetComponent<Terning>().RullTerning();
        GetComponent<Terning>().RullTerningFysisk();
        UI.SetVisTerningSkjerm(false);
        UI.SetVisResultatSkjerm(true);
        //Debug.Log("Du rullet " + GetComponent<Terning>().forrigeRull + " forrige rull");

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
        if (sisteDag)
        {
            AvsluttSpillet();
        }
    }

    

    public void SetStatusRoligDag()
    {
        status = "RoligDag";
        UI.SetCurrentPanel(5);
        UI.SetVisTerningSkjerm(true);

    }

    public void RapportDager()
    {
        dagLagrer.LeggTilDagITotalen(ruteNummer, energi);
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

    public void SetStatusSjansekort(string beskrivelse, string energi, string[] modifikatorer, string sp�rsm�l, string kortnummer)
    {
        UI.SetCurrentPanel(2);

        UI.SetSjansekortVerdi(beskrivelse, energi, modifikatorer, sp�rsm�l, kortnummer);
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
        if(energi <= 0)
        {
            UI.OppdaterEnergi("0");
            UI.SetCurrentPanel(8);
            UI.SetKanByttePanel(false);
            status = "NodTiltak";
            N�dstiltakKort[] nodTiltakKort = GameObject.FindGameObjectWithTag("KortStokkene").GetComponent<Kortstokkene>().n�dstiltakKort.ToArray();
            aktivtNodKort = nodTiltakKort[Random.Range(0, nodTiltakKort.Length)];

            UI.OppdaterN�dKortSkjerm($"Du har trukket et n�dkort, der st�r det \n{aktivtNodKort.beskrivelse}\nDu hviler i {aktivtNodKort.dager} dager og starter med 10 i energi");
            
        }
        else if(energi > 30)
        {
            energi = 30;
            UI.OppdaterEnergi("30");
            

        }
        else
        {
            UI.OppdaterEnergi(energi.ToString());
        }
        
    }

    public void SetStatusNyDag()
    {

        UI.SetCurrentPanel(4);
    }

    public void Nodstiltak()
    {
        UI.SetKanByttePanel(true);
        
        if(ruteNummer + aktivtNodKort.dager >= ruter.Length)
        {
            AvsluttSpillet();
        }
        else
        {
            ruteNummer += aktivtNodKort.dager - 1;

            energi = 10;
            UI.OppdaterEnergi(energi.ToString());
            hum�rModifikator = 0;
            UI.EndreHum�rmodifikatorVerdi(hum�rModifikator);

            FlyttTilNesteDag();
        }

        
    }

    void FlyttTilNesteDag()
    {
        
        ruteNummer += 1;
        if (!sisteDag)
        {
            if (ruteNummer == 27)
            {
                //transform.position = ruter[ruteNummer].transform.position;
                sisteDag = true;
            }
            
            

                

                StartCoroutine(FlyttSmud());
                IEnumerator FlyttSmud()
                {
                    float timePassed = 0;
                    float pos = 0;
                    float lerpDuration = 1.5f;
                    Vector3 startPos = gameObject.transform.position;
                    Vector3 endPos = new Vector3(ruter[ruteNummer].transform.position.x, gameObject.transform.position.y, ruter[ruteNummer].transform.position.z);
                    while (timePassed < lerpDuration)
                    {

                        float placeInSequence = timePassed / lerpDuration;

                        transform.position = Vector3.Lerp(startPos, endPos, placeInSequence);


                        timePassed += Time.deltaTime;
                        yield return null;
                        

                    }
                    
                    transform.position = ruter[ruteNummer].transform.position;
                    bool erDetKortP�Ruten;
                    erDetKortP�Ruten = ruter[ruteNummer].Activate();

                    if (!erDetKortP�Ruten)
                    {
                        UI.SetCurrentPanel(0);
                    }

                }

            
            
            

        }

    }

    void FlyttSmudEllers(Vector3 endPos)
    {
        StartCoroutine(FlyttSmud());
        IEnumerator FlyttSmud()
        {
            float timePassed = 0;
            float pos = 0;
            float lerpDuration = 1.5f;
            Vector3 startPos = gameObject.transform.position;
            
            while (timePassed < lerpDuration)
            {

                float placeInSequence = timePassed / lerpDuration;

                transform.position = Vector3.Lerp(startPos, endPos, placeInSequence);


                timePassed += Time.deltaTime;
                yield return null;


            }

            transform.position = endPos;
        }
    }

    void AvsluttSpillet() 
    {
        UI.SetCurrentPanel(7);
        Vector3 sisteRutePos = ruter[ruter.Length - 1].transform.position;

        FlyttSmudEllers(new Vector3(sisteRutePos.x + -10, transform.position.y, sisteRutePos.z));
        
    }

    int EndreHum�rModifikator(int endring)
    {

        if (!(hum�rModifikator + endring < -2) && !(2 < hum�rModifikator + endring))
        {
            hum�rModifikator += endring;
        }
        else
        {
            if (hum�rModifikator + endring < -2)
            {
                hum�rModifikator = -2;
            }
            else
            {
                hum�rModifikator = 2;
            }
        }

        UI.EndreHum�rmodifikatorVerdi(hum�rModifikator);
        return hum�rModifikator;

    }

    int SjekkHum�rModifikator(int terningKast)
    {
        forrigeHum�rModifikator = hum�rModifikator;
        //Debug.Log(terningKast);
        if (terningKast > 4)
        {
            return EndreHum�rModifikator(1);
        }
        else if (terningKast < 3)
        {
            return EndreHum�rModifikator(-1);
        }

        return 0;
    }

    int RullMedHum�rmodifikator(int terningkast)
    {
        if (terningkast < 1)
        {
            return 1;
        }
        else if (terningkast > 6)
        {
            return 6;
        }
        else
        {
            return terningkast;
        }

    }


    string HumorModifikatorTekstVerdi(int i)
    {
        if(i == -2)
        {
            return "veldig kjip";   
        }
        else if(i == -1)
        {
            return "kjip";
        }
        else if (i == 0)
        {
            return "ok";
        }
        else if (i == 1)
        {
            return "bra";
        }
        else if (i == 2)
        {
            return "veldig bra";
        }

        return "Ingen verdi";
    }



}
