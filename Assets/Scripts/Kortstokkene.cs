using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

public class Kortstokkene : MonoBehaviour
{
    public List<HendelsesKort> hendelsesKort = new List<HendelsesKort>();
    public List<SjanseKort> sjanseKort = new List<SjanseKort>();
    public List<PersonlighetsKort> personlighetsKort = new List<PersonlighetsKort>();
    public List<NødstiltakKort> nødstiltakKorts = new List<NødstiltakKort>();


    //Dette er for testing
    string[] vanskelighetsgrader = { "Fin", "Normal", "Kjip" };


    //public List<HendelsesKort> hendelsesKort = new List<>();

    // Start is called before the first frame update
    void Awake()
    {
        LagKort();
    }

    private static string kortstiPersonlighet = "/Resources/Personlighet/Personlighet.csv";
    private static string kortstiHendelser = "/Resources/Hendelser/Hendelser.csv";
    private static string kortstiNødstiltak = "/Resources/Nødstiltakkort/Nødstiltakkort.csv";
    private static string korstiSjansekort = "/Resources/Sjansekort/Sjansekort.csv";

    public void LagKort()
    {
        //Personlighetskortene
        string[] allelinjer = File.ReadAllLines(Application.dataPath + kortstiPersonlighet);
        int kortTall = 0;
        foreach (string s in allelinjer)
        {
            string[] splitData = s.Split(',');
            PersonlighetsKort kort = new PersonlighetsKort(kortTall, splitData[0], "PersonlighetsKort");
            personlighetsKort.Add(kort);
            kortTall++;
        }

        //Hendelseskortene
        allelinjer = File.ReadAllLines(Application.dataPath + kortstiHendelser);
        foreach (string s in allelinjer)
        {
            string[] splitData = s.Split(',');
            HendelsesKort kort = new HendelsesKort(kortTall, splitData[0], "HendelsesKort", splitData[1]);
            hendelsesKort.Add(kort);
            kortTall++;
        }

        allelinjer = File.ReadAllLines(Application.dataPath + kortstiNødstiltak);
        foreach (string s in allelinjer)
        {
            string[] splitData = s.Split(',');

            NødstiltakKort kort = new NødstiltakKort(kortTall, splitData[0], "NødstiltakKort", int.Parse(splitData[1]), int.Parse(splitData[2]));
            nødstiltakKorts.Add(kort);
            kortTall++;
        }

        allelinjer = File.ReadAllLines(Application.dataPath + korstiSjansekort);
        foreach (string s in allelinjer)
        {
            string[] splitData = s.Split(',');

            List<string> modifikatorer = new List<string>();
            for(int i = 0; i < 3; i++)
            {
                if(splitData[i+2] != "")
                {
                    string data = splitData[i + 2].Trim('+');
                    modifikatorer.Add(data.Trim());
                }
            }


            SjanseKort kort = new SjanseKort(kortTall, splitData[0], "SjanseKort", int.Parse(splitData[1]),modifikatorer.ToArray(), splitData[5]);
            sjanseKort.Add(kort);

            Debug.Log($"{kort.beskrivelse}, {kort.energi}, modifikatorlengde: {kort.modifikatorer.Length}, {kort.sporsmal}");
            kortTall++;
        }
        AssetDatabase.SaveAssets();



    }


}
