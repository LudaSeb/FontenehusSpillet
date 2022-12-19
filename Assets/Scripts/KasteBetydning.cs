using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KasteBetydning : MonoBehaviour
{
    int[] fint = {-2, 0, 0, 2, 4, 6};
    int[] normal = {-4, -2, 0, 0, 2, 4};
    int[] kjip = {-6, -4, -2, 0, 0, 2};
    int[] sovn = { -2, -1, 0, 0, 1, 2};
    int[] safe = { -1, 1, 1, 1, 1, 1};

    public int SjekkTabell(int terningkast, string status)
    {
        string trimmedStatus = status.ToLower().Trim();
        //Debug.Log($"Sjekker tabellen: terning: {terningkast}, vanskelighetsgrad: {trimmedStatus}");
        switch (trimmedStatus)
        {
            case "fin":
                //Debug.Log("fin " + (terningkast - 1));
                return fint[terningkast - 1];
            case "normal":
                //Debug.Log("normal " + (terningkast - 1));
                return normal[terningkast - 1];
            case "kjip":
                //Debug.Log("kjipt " + (terningkast - 1));
                return kjip[terningkast - 1];
            case "sovn":
                //Debug.Log("sovn " + (terningkast - 1));
                return sovn[terningkast - 1];
            case "safe":
                //Debug.Log("safe " + (terningkast - 1));
                return safe[terningkast - 1];
            default:
                return 0;
        }
    }






}
