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
    int humørModifikator = 0;
    int forrigeHumørModifikator = 0;

    string vanskelighetsgrad = "safe";

    Ruter[] ruter;

    NødstiltakKort aktivtNodKort;

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

                dagLagrer.LeggTilTekstDel("Energi etter søvn: " + energi + " + " + energiLagtTil + " = " + (energi + energiLagtTil) +  "\n");
                LeggTilEnergi(energiLagtTil);

                FlyttTilNesteDag();
                

                break;
            case "HendelsesKort":
                //UI.LeggTilRullInfo($" {humørModifikator}({HumorModifikatorTekstVerdi(humørModifikator)})");
                int rull = GetComponent<Terning>().forrigeRull;
                int energiViSkalLeggeTil = GetComponent<KasteBetydning>().SjekkTabell(RullMedHumørmodifikator(rull + humørModifikator), vanskelighetsgrad);
                
                int humorForRull = humørModifikator;
                
                int humorEtterRull = SjekkHumørModifikator(GetComponent<Terning>().forrigeRull);

                dagLagrer.LeggTilTekstDel("Energi etter dagens hendelse: " + energi + " + " + energiViSkalLeggeTil + " = " + (energi + energiViSkalLeggeTil) + "\n" +
                    $"Rull: {rull} med humør {HumorModifikatorTekstVerdi(forrigeHumørModifikator)}({forrigeHumørModifikator})\n" 
                    + "Humørmodifikator var : " + humorForRull + " og er nå: " + humorEtterRull + "\n");

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

                //SjekkHumørModifikator(GetComponent<Terning>().forrigeRull);
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
                        GetComponent<Karakter>().SetFargePåEgenskaper(i, new Color32(0, 255, 0, 255));
                    }
                    else if (m[j].Value < 0)
                    {
                        GetComponent<Karakter>().SetFargePåEgenskaper(i, new Color32(255, 0, 0, 255));
                    }
                }
            }
        }

        int humorModFor = humørModifikator;
        int humorModEtter = EndreHumørModifikator(k.energi);

        dagLagrer.LeggTilTekstDel($"Energi etter sjansekort: {energi} + grunnenergi({k.energi}) + modifikatorenergi({totalEnergiFraModifikatorer}) = {energi + k.energi + totalEnergiFraModifikatorer} \n Humørmodifikator var {humorModFor} og er nå {humorModEtter}");

        
        

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

    public void SetStatusGåVidereTilNesteDag()
    {
        if (sisteDag)
        {

        }
        else
        {
            UI.SetCurrentPanel(4);
        }

    }

    public void SetStatusSjansekort(string beskrivelse, string energi, string[] modifikatorer, string spørsmål, string kortnummer)
    {
        UI.SetCurrentPanel(2);

        UI.SetSjansekortVerdi(beskrivelse, energi, modifikatorer, spørsmål, kortnummer);
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
            NødstiltakKort[] nodTiltakKort = GameObject.FindGameObjectWithTag("KortStokkene").GetComponent<Kortstokkene>().nødstiltakKort.ToArray();
            aktivtNodKort = nodTiltakKort[Random.Range(0, nodTiltakKort.Length)];

            UI.OppdaterNødKortSkjerm($"Du har trukket et nødkort, der står det \n{aktivtNodKort.beskrivelse}\nDu hviler i {aktivtNodKort.dager} dager og starter med 10 i energi");
            
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
            humørModifikator = 0;
            UI.EndreHumørmodifikatorVerdi(humørModifikator);

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
                    bool erDetKortPåRuten;
                    erDetKortPåRuten = ruter[ruteNummer].Activate();

                    if (!erDetKortPåRuten)
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

    int EndreHumørModifikator(int endring)
    {

        if (!(humørModifikator + endring < -2) && !(2 < humørModifikator + endring))
        {
            humørModifikator += endring;
        }
        else
        {
            if (humørModifikator + endring < -2)
            {
                humørModifikator = -2;
            }
            else
            {
                humørModifikator = 2;
            }
        }

        UI.EndreHumørmodifikatorVerdi(humørModifikator);
        return humørModifikator;

    }

    int SjekkHumørModifikator(int terningKast)
    {
        forrigeHumørModifikator = humørModifikator;
        //Debug.Log(terningKast);
        if (terningKast > 4)
        {
            return EndreHumørModifikator(1);
        }
        else if (terningKast < 3)
        {
            return EndreHumørModifikator(-1);
        }

        return 0;
    }

    int RullMedHumørmodifikator(int terningkast)
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
