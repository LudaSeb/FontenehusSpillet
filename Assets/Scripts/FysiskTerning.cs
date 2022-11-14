using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FysiskTerning : MonoBehaviour
{
    [SerializeField] Vector3 StartPosisjon;
    //[SerializeField] Vector3 kraft;
    
    public void KastTerningen()
    {
        gameObject.SetActive(true);
        transform.position = StartPosisjon;
        Vector3 kraft = new Vector3(250, 0, -250);
        GetComponent<Rigidbody>().AddForce(kraft);
    }

    public int LesTerningen()
    {
        Debug.Log(transform.up);
        //1 på terningen
        if(CalculateUpAxis(transform.up.y) == 1)
        {
            return 1;
        }
        //6 på terningen
        else if(CalculateUpAxis(transform.up.y) == -1)
        {
            return 6;
        }
        //2 på terningen
        else if(CalculateUpAxis(transform.up.z) == 1)
        {
            return 2;
        }
        //5 på terningen
        else if (CalculateUpAxis(transform.up.z) == -1)
        {
            return 5;
        }
        //3 på terningen
        else if (CalculateUpAxis(transform.up.x) == -1)
        {
            return 3;
        }
        //4 på terningen
        else if (CalculateUpAxis(transform.up.x) == 1)
        {
            return 4;
        }
        Debug.Log($"transform.up.x: {(int)transform.up.x}, transform.up.y: {(int)transform.up.y}, transform.up.z: {(int)transform.up.z}");

        return 0;
        
    }

    int CalculateUpAxis(float numberToCheck)
    {
        if(numberToCheck > 0.8f)
        {
            return 1;
        }
        else if(numberToCheck < -.8f)
        {
            return -1;
        }
        return 0;
    }
    
}
