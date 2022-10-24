using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bevegelse : MonoBehaviour
{
    [SerializeField] float speedy = 1;
    [SerializeField] float LRSpeedy = 1;
    [SerializeField] float brettkantX;
    [SerializeField] float brettkantZ;

    [SerializeField] GameObject fridagPanel;
    [SerializeField] UiManager UI;

    bool canMove = true;

    float startXPosisjon;

    GameObject ruteForeldre;

    string status = "";
    int ruteNummer = -1;
    int energi = 15;

    Ruter[] ruter;
    // Start is called before the first frame update
    void Start()
    {
        startXPosisjon = transform.position.x;
        ruteForeldre = GameObject.FindGameObjectWithTag("Ruter");

        ruter = ruteForeldre.GetComponentsInChildren<Ruter>();

        status = "Sovn";
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(GetComponent<Terning>().RullTerning());

            switch (status)
            {
                case "Sovn":
                    energi += GetComponent<KasteBetydning>().SjekkTabell(GetComponent<Terning>().forrigeRull, "sovn");
                    Debug.Log("Energi etter s¯vn er: " + energi);
                    ruteNummer += 1;
                    transform.position = ruter[ruteNummer].transform.position;
                    bool erDetKortPÂRuten;
                    erDetKortPÂRuten = ruter[ruteNummer].Activate();

                    if (erDetKortPÂRuten)
                    {
                        SetStatusHendelsesKort();
                    }
                    else
                    {
                        UI.SetCurrentPanel(0);
                    }
                    

                    break;
                case "Dag":
                    // code block
                    break;
                case "RoligDag":
                    energi += GetComponent<KasteBetydning>().SjekkTabell(GetComponent<Terning>().forrigeRull, "safe");
                    SetStatusSovn();
                    Debug.Log("Energi etter dagen: " + energi);
                    
                    
                    //status = "Sovn";

                    break;
                default:
                    // code block
                    break;
            }
        }

        
        /*if(transform.position.x < brettkantX)
        {
            if (transform.position.z > brettkantZ)
            {
                Debug.Log("Du vant");
                canMove = false;
                
            }
            else
            {
                transform.position = new Vector3(startXPosisjon, transform.position.y, transform.position.z + 1);
            }
        }*/


    }

    void SetStatusSovn()
    {
        //RulleTerningRute;
        status = "Sovn";
    }

    public void SetStatusRoligDag()
    {
        status = "RoligDag";
        UI.SetCurrentPanel(1);
    }

    public void SetStatusDag()
    {

    }

    public void SetStatusHendelsesKort()
    {
        UI.SetCurrentPanel(2);
    }

    
}

