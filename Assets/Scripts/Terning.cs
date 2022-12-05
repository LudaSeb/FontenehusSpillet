using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class Terning : MonoBehaviour
{
    [SerializeField] int maksTerning = 13;
    [SerializeField] float fysiskKastVenteTid = 4;
    [SerializeField] GameObject fysiskTerning;
    [SerializeField] GameObject gÂVidereKnappObject;
    [SerializeField] TextMeshProUGUI resultatText;

    Button gÂVidereKnapp;
    public int forrigeRull;

    GameObject rulletTerning;

    // Start is called before the first frame update
    void Start()
    {
        gÂVidereKnapp = gÂVidereKnappObject.GetComponent<Button>();
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
            //Debug.Log(startPunkt + 1);

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

            bool fterStillMoving = true ;
            yield return new WaitForSeconds(1);
            while (fterStillMoving)
            {
                Vector3 firstTPos = fTer.transform.position;
                Quaternion firstTRot = fTer.transform.rotation;

                yield return new WaitForSeconds(0.1f);
                Vector3 secondTPos = fTer.transform.position;
                Quaternion secondTRot = fTer.transform.rotation;

                if (firstTPos == secondTPos && firstTRot == secondTRot)
                {
                    fterStillMoving = false;
                }
               
            }
            
            resultat = fTer.LesTerningen();
            forrigeRull = resultat;
            resultatText.text = "Du rullet " + forrigeRull;

            //gÂVidereKnapp.SetEnabled(true);
            gÂVidereKnappObject.SetActive(true);
            
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
