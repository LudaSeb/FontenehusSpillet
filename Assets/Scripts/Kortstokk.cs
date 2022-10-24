using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kortstokk : MonoBehaviour
{
    List<Kort> kortene = new List<Kort>();

    private void Start()
    {
        
        for(int i = 0; i < 20; i++)
        {
            kortene.Add(new Kort(i, "Jeg er et kort", "kort"));
            //char[] minTekst = { 'J', 'e', 'g' };
        }

        List<Kort> trukkedeKort = Trekk(5);

        for(int i = 0; i < trukkedeKort.Count; i++)
        {
            Debug.Log("Trukket kort: " + trukkedeKort[i].kortTall);
        }
    }

    

    public List<Kort> Trekk(int antallKort)
    {
        
        List<Kort> trukkedeKort = new List<Kort>();
        List<int> trukkedeKortIndekser = new List<int>();
        for (int i = 0; i < antallKort; i++)
        {
            int trukketKortIndeks = Random.Range(0, kortene.Count);
            bool leggTilKort = true;
            for(int j = 0; j < trukkedeKort.Count; j++)
            {
                if(trukketKortIndeks == trukkedeKortIndekser[j])
                {
                    i--;
                    leggTilKort = false;
                    break;
                }
                leggTilKort = true;
                

            }
            
            if (leggTilKort)
            {
                trukkedeKort.Add(kortene[trukketKortIndeks]);
                trukkedeKortIndekser.Add(trukketKortIndeks);
                Debug.Log("trekk");
            }
            
        }

        return trukkedeKort;
        
    } 
}
