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
    public List<NødstiltakKort> nødstiltakKort = new List<NødstiltakKort>();


    //Dette er for testing
    string[] vanskelighetsgrader = { "Fin", "Normal", "Kjip" };


    //public List<HendelsesKort> hendelsesKort = new List<>();

    // Start is called before the first frame update
    void Awake()
    {
        LagKort();
    }

    private static string kortstiPersonlighet = "Personlighet/Personlighet";
    private static string kortstiHendelser = "Hendelser/Hendelser";
    private static string kortstiNødstiltak = "Nodstiltakkort/Nødstiltakkort";
    private static string korstiSjansekort = "Sjansekort/Sjansekort";

    public void LagKort()
    {
        string[] allelinjer;
        int kortTall = 0;

        TextAsset currentFileBeingProcessed;

        //Debug.Log(textFile);

        //Hendelseskortene //Resources.load burde det stå her
        //Resources.Load();
        //allelinjer = File.ReadAllLines(Application.dataPath + kortstiHendelser);
        currentFileBeingProcessed = Resources.Load<TextAsset>(kortstiHendelser);
        
        allelinjer = currentFileBeingProcessed.text.Split('\n');
        foreach (string s in allelinjer)
        {
            string[] splitData = s.Split(',');
            HendelsesKort kort = new HendelsesKort(kortTall, splitData[0], "HendelsesKort", splitData[1]);
            hendelsesKort.Add(kort);
            kortTall++;
        }


        //Sjansekortene
        currentFileBeingProcessed = Resources.Load<TextAsset>(korstiSjansekort);
        allelinjer = currentFileBeingProcessed.text.Split('\n');
        foreach (string s in allelinjer)
        {
            string[] splitData = s.Split(',');

            List<string> modifikatorer = new List<string>();
            for (int i = 0; i < 3; i++)
            {
                if (splitData[i + 2] != "")
                {
                    string data = splitData[i + 2].Trim('+');
                    modifikatorer.Add(data.Trim());
                }
            }


            SjanseKort kort = new SjanseKort(kortTall, splitData[0], "SjanseKort", int.Parse(splitData[1]), modifikatorer.ToArray(), splitData[5]);
            sjanseKort.Add(kort);

            //Debug.Log($"{kort.beskrivelse}, {kort.energi}, modifikatorlengde: {kort.modifikatorer.Length}, {kort.sporsmal}");
            kortTall++;
        }


        //Nødkort
        currentFileBeingProcessed = Resources.Load<TextAsset>(kortstiNødstiltak);
        allelinjer = currentFileBeingProcessed.text.Split('\n');
        foreach (string s in allelinjer)
        {
            string[] splitData = s.Split(',');

            NødstiltakKort kort = new NødstiltakKort(kortTall, splitData[0], "NødstiltakKort", int.Parse(splitData[1]), int.Parse(splitData[2]));
            nødstiltakKort.Add(kort);
            kortTall++;
        }

        //Personlighetskortene
        currentFileBeingProcessed = Resources.Load<TextAsset>(kortstiPersonlighet);
        allelinjer = currentFileBeingProcessed.text.Split('\n');
        foreach (string s in allelinjer)
        {
            string[] splitData = s.Split(',');
            PersonlighetsKort kort = new PersonlighetsKort(kortTall, splitData[0], "PersonlighetsKort");
            personlighetsKort.Add(kort);
            kortTall++;
        }

        //AssetDatabase.SaveAssets();

        StokkKortListe(hendelsesKort);
        StokkKortListe(sjanseKort);
        StokkKortListe(personlighetsKort);
        StokkKortListe(nødstiltakKort);

    }

    public void StokkKortListe<T>(IList<T> list)
    {
        System.Random rng = new System.Random();

        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }


}
