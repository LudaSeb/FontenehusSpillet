using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ruter : MonoBehaviour
{
    public void Activate()
    {
        GetComponentInChildren<Renderer>().material.color = Color.red;
    }
}
