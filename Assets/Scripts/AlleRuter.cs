using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlleRuter : MonoBehaviour
{
    Ruter[] rutene;
    HendelsesKort[] hendelsesKortene;
    int trukketKortIndex = 0;

    // Start is called before the first frame update
    void Start()
    {

        rutene = GetComponentsInChildren<Ruter>();
        hendelsesKortene = GameObject.FindGameObjectWithTag("KortStokkene").GetComponent<Kortstokkene>().hendelsesKort.ToArray();

        for(int i = 0; i < rutene.Length; i++)
        {
            rutene[i].SetDayNumber(i + 1);
            //hendelsesKortene[i] = new HendelsesKort(i, "Beskrivelse", "HendelsesKort", "Normal");
        }

        LeggeUtKort(0);
        LeggeUtKort(7);
        LeggeUtKort(13);
        LeggeUtKort(20);
        
    }

    

    void LeggeUtKort(int ukedag)
    {
        List<Ruter> uke = new List<Ruter>();

        for(int i = ukedag; i < ukedag + 7; i++)
        {
            uke.Add(rutene[i]);
        }

        List<Ruter> valgteDager = RandomDay(new List<Ruter>(), uke);

        foreach(Ruter r in valgteDager)
        {
            
            r.GetComponentInChildren<MeshRenderer>().material.color = Color.red;
            r.SetKort(hendelsesKortene[trukketKortIndex]);
            
            trukketKortIndex++;


        }

            //


    }

    List<Ruter> RandomDay(List<Ruter> valgteDager, List<Ruter> gjenstaendeDager)
    {
        if (valgteDager.Count < 3)
        {
            int valgtDag = Random.Range(0, gjenstaendeDager.Count);
            
            valgteDager.Add(gjenstaendeDager[valgtDag]);
            gjenstaendeDager.RemoveAt(valgtDag);

            return RandomDay(valgteDager, gjenstaendeDager);
        }
        else
        {
            return valgteDager;
        }
    }


    void SorterKort()
    {
        GameObject[] Botte = { };

        foreach(GameObject godteri in Botte)
        {
            /*if (sjokolade)
            {
                LeggInnISjokoladeboks;
            }
            else if (sukkertøy)
            {
                LeggInnISukkertøyBoks
            }
            else
            {
                KastDenIBøtta!
            }*/

            switch (godteri.name)
            {
                case "Sjokolade":
                    // code block
                    break;
                case "Sukkertøy":
                    // code block
                    break;
                case "CheeseDoodles":
                    // code block
                    break;
                default:
                    // code block
                    break;
            }
        }
    }
}
