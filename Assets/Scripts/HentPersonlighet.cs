using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class HentPersonlighet : MonoBehaviour
{

    
     
     

//<<< End of EditorApplication.isPlaying check


//Declare bool that will be used as a "button", it's best used with preprocessor directive so you don't have this in final build 
#if UNITY_EDITOR
public bool reloadInitialData = false;

//You can have multiple booleans here
private void OnValidate()
{
    if (reloadInitialData)
    {
            // Your function here
            LagKort();

            //When its done set this bool to false
            //This is useful if you want to do some stuff only when clicking this "button"
            Debug.Log("Generer kort");
        reloadInitialData = false;
    }
}
#endif

private static string kortsti = "/Resources/Personlighet/Personlighet.csv";

    public static void LagKort()
    {

   
        
        
            string[] allelinjer = File.ReadAllLines(Application.dataPath + kortsti);
            foreach (string s in allelinjer)
            {
                string[] splitData = s.Split(',');
                ScriptableObjectPersonlighetsKort personlighetskortene = ScriptableObject.CreateInstance<ScriptableObjectPersonlighetsKort>();
                personlighetskortene.beskrivelse = splitData[0];
                personlighetskortene.kortType = "Personlighetskort";

                AssetDatabase.CreateAsset(personlighetskortene, $"Assets/Resources/Personlighet/{personlighetskortene.beskrivelse}.asset");
            }
            AssetDatabase.SaveAssets();
        
        
        
    }
}
