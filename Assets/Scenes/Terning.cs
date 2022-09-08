using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terning : MonoBehaviour
{
    [SerializeField] int maksTerning = 13;
    public int forrigeRull;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public int RullTerning()
    {
        int resultat;
        
        resultat = Random.Range(1, maksTerning+1);

        forrigeRull = resultat;
        
        return resultat;
    }



    

}
