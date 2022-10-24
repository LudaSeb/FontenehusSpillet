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
        switch (status)
        {
            case "fint":
                return fint[terningkast - 1];
            case "normal":
                return normal[terningkast - 1];
            case "kjip":
                return kjip[terningkast - 1];
            case "sovn":
                return sovn[terningkast - 1];
            case "safe":
                return safe[terningkast - 1];
            default:
                return 0;
        }
    }






}
