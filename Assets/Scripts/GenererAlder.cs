using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GenererAlder : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI alderText;
    [SerializeField] TextMeshProUGUI knappeTekst;
    [SerializeField] Slider aldersSlider;
    public void LagAlder()
    {
        string alder = Random.Range(15, (int)aldersSlider.value + 1 ).ToString();
        alderText.text = alder;
        GameObject.FindGameObjectWithTag("Brikke").GetComponent<Karakter>().EndreAlder(alder);
    }

    public void OppdaterKnappeTeksten()
    {
        knappeTekst.text = "Rull alder: 15 -" + aldersSlider.value.ToString();
    }
}
