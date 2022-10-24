using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bakgrunn : MonoBehaviour
{
    //Bilde mittBilde;
    //Bilde mittBilde2;
    //Bilde mittBilde3;
    [SerializeField] Material[] bakgrunnsMatrialer; 
    
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MeshRenderer>().material = bakgrunnsMatrialer[Random.Range(0, bakgrunnsMatrialer.Length)];
    }
}
