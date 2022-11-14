using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terning : MonoBehaviour
{
    [SerializeField] int maksTerning = 13;
    [SerializeField] float fysiskKastVenteTid = 4;
    [SerializeField] GameObject fysiskTerning;
    public int forrigeRull;

    GameObject rulletTerning;

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

    private void Update()
    {
        
    }

    public int RullTerningFysisk()
    {
        int resultat = 0;
        StartCoroutine(Kast());

        IEnumerator Kast()
        {
            rulletTerning = Instantiate(fysiskTerning, Vector3.zero, Quaternion.identity);

            int startPunkt = Random.Range(0, maksTerning);
            Debug.Log(startPunkt + 1);

            switch (startPunkt)
            {
                case 0:
                    rulletTerning.transform.rotation = Quaternion.Euler(0, 0, 0);
                    break;
                case 1:
                    rulletTerning.transform.rotation = Quaternion.Euler(90, 0, 0);
                    break;
                case 2:
                    rulletTerning.transform.rotation = Quaternion.Euler(0, 0, 90);
                    break;
                case 3:
                    rulletTerning.transform.rotation = Quaternion.Euler(0, 0, 270);
                    break;
                case 4:
                    rulletTerning.transform.rotation = Quaternion.Euler(270, 0, 0);
                    break;
                case 5:
                    rulletTerning.transform.rotation = Quaternion.Euler(180, 0, 0);
                    break;
            }
            

            FysiskTerning fTer = rulletTerning.GetComponent<FysiskTerning>();
            fTer.GetComponent<FysiskTerning>().KastTerningen();
            yield return new WaitForSeconds(fysiskKastVenteTid);
            resultat = fTer.LesTerningen();
            Debug.Log(resultat);
            
        }
        return resultat;
    }

    public void GÂVidere()
    {
        if(rulletTerning != null)
        {
            Destroy(rulletTerning);
            rulletTerning = null;
        }
        
    }



    

}
