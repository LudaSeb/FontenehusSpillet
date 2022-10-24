using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField] GameObject[] uiPanels;


    public void SetCurrentPanel(int panelIndex)
    {
        for(int i = 0; i < uiPanels.Length; i++)
        {
            if(i == panelIndex)
            {
                uiPanels[i].SetActive(true);
            }
            else
            {
                uiPanels[i].SetActive(false);
            }
        }
    }
    
    
}
