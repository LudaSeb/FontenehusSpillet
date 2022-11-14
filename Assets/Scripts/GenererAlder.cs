using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GenererAlder : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI alderText;
    public void LagAlder()
    {
        string alder = Random.Range(15, 66).ToString();
        alderText.text = alder;
        GameObject.FindGameObjectWithTag("Brikke").GetComponent<Karakter>().EndreAlder(alder);
    }
}
