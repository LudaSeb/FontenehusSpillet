using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ViseKort : MonoBehaviour
{
    HendelsesKort mittKort;
    // Start is called before the first frame update
    void Start()
    {
        //SetMittKort(new HendelsesKort(0, "Jeg er en beskrivelse", "KortType", "(Vanskelighetsgrad)"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void SetMittKort(Kort mittKort)
    {
        this.mittKort = (HendelsesKort)mittKort;
        TextMeshPro[] alleTekstenePaaEtKort = GetComponentsInChildren<TextMeshPro>();
        
        alleTekstenePaaEtKort[0].text = this.mittKort.beskrivelse;
        alleTekstenePaaEtKort[1].text = this.mittKort.vanskelighetsgrad;
    }

    


}
