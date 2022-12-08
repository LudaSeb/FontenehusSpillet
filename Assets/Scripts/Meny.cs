using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Meny : MonoBehaviour
{
    
    public void TilbakeTilStart()
    {
        SceneManager.LoadScene("StartScene");
    }
}
