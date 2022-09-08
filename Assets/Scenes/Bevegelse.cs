using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bevegelse : MonoBehaviour
{
    [SerializeField] float speedy = 1;
    [SerializeField] float LRSpeedy = 1;
    [SerializeField] float brettkantX;
    [SerializeField] float brettkantZ;

    bool canMove = true;

    float startXPosisjon;

    GameObject ruteForeldre;

    Ruter[] ruter;
    // Start is called before the first frame update
    void Start()
    {
        startXPosisjon = transform.position.x;
        ruteForeldre = GameObject.FindGameObjectWithTag("Ruter");

        ruter = ruteForeldre.GetComponentsInChildren<Ruter>();

        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(GetComponent<Terning>().RullTerning());
            int terningKast = GetComponent<Terning>().forrigeRull - 1;
            //transform.position = new Vector3(0, 0, 0);//ruter[0].transform.position;
            transform.position = ruter[terningKast].transform.position;

            //ruter[terningKast - 1].GetComponentInChildren<Renderer>().material.color = Color.red;
            ruter[terningKast].Activate();
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
}
