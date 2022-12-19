using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kortstokk : MonoBehaviour
{
    public List<Kort> Trekk(int antallKort, List<Kort> trekkeBunke)
    {
        
        List<Kort> trukkedeKort = new List<Kort>();
        List<int> trukkedeKortIndekser = new List<int>();
        for (int i = 0; i < antallKort; i++)
        {
            //Debug.Log("Trukket kort indeks: " + i);
            int trukketKortIndeks = Random.Range(0, trekkeBunke.Count);

            //Debug.Log("Kort som ble trukket: " + trukketKortIndeks);
            
            bool leggTilKort = true;
            for(int j = 0; j < trukkedeKortIndekser.Count; j++)
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
                trukkedeKort.Add(trekkeBunke[trukketKortIndeks]);
                trukkedeKortIndekser.Add(trukketKortIndeks);
            }
            
        }

        return trukkedeKort;
        
    } 
}
