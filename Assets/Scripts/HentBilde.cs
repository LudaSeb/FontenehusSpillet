using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System.Linq;

public class HentBilde : MonoBehaviour
{
    //[SerializeField] Image bilde;
    [SerializeField]Image img;


    string bildePathOrig = "Bilder/";
    string bildeMappeIndeks; //tallet som bildet har i bildelisten.


   

    private void Start()
    {
        img = GetComponent<Image>();
    }

    public void FinnBilde(string bildeNummer)
    {
        bildeMappeIndeks = bildeNummer;
        string bildeMappePath = bildePathOrig + bildeMappeIndeks;

        //string path = GetRandomFile(bildeMappePath);
        
        //Debug.Log(bildeMappePath);

        //RecursiveFileProcessor rfp = new RecursiveFileProcessor();
        //rfp.Process(bildeMappePath);

        //string bildeMappePath = bildePathOrig + bildeMappeIndeks;
        //Texture2D tex = new Texture2D(400,400);
        Texture2D tex = GetRandomFile(bildeMappePath);
        //tex.LoadImage(bildeMappeIndeks.bytes);


        /*byte[] fileData;

        if (File.Exists(path))
        {
            fileData = File.ReadAllBytes(path);
            tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
            
        }
        else
        {
            Debug.Log("filepath wrong: " + path);
        }*/


        //GetComponent<Image>().sprite = Sprite.Create(tex, new Rect(0,0,tex.width, tex.height), new Vector2(0.5f, 0.5f));
        //bildeMesh.material.mainTexture = tex;
        img.sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
            

        //bilde.sprite = Sprite.Create(tex, new Rect(0,0,tex.width, tex.height), new Vector2(0.5f, 0.5f));
        //bilde.sprite = sprite;
    }

    Texture2D GetRandomFile(string path)
    {
        Texture2D[] potentialTextures = Resources.LoadAll<Texture2D>(path);
        Texture2D chosenTexture;
        if (potentialTextures.Length > 0)
        {
            chosenTexture = potentialTextures[Random.Range(0, potentialTextures.Length)];
        }
        else
        {
            chosenTexture = Resources.Load<Texture2D>("Bilder/Default/Default");
        }
        
        /*string file = null;
        if (!string.IsNullOrEmpty(path))
        {
            var extensions = new string[] { ".png", ".jpg", ".gif", ".jpeg" };
            try
            {
                var di = new DirectoryInfo(path);
                var rgFiles = di.GetFiles("*.*").Where(f => extensions.Contains(f.Extension.ToLower()));
                
                file = rgFiles.ElementAt(Random.Range(0, rgFiles.Count())).FullName;
            }
            // probably should only catch specific exceptions
            // throwable by the above methods.
            catch { }
        }*/
        return chosenTexture;
    }
}

public class RecursiveFileProcessor
{
    public void Process(string [] args)
    {
        foreach (string path in args)
        {
            if (File.Exists(path))
            {
                // This path is a file
                ProcessFile(path);
            }
            else if (Directory.Exists(path))
            {
                // This path is a directory
                ProcessDirectory(path);
            }
            else
            {
                Debug.Log("{0} is not a valid file or directory. " + path);
            }
        }
    }

    // Process all files in the directory passed in, recurse on any directories
    // that are found, and process the files they contain.
    public static void ProcessDirectory(string targetDirectory)
    {
        // Process the list of files found in the directory.
        string[] fileEntries = Directory.GetFiles(targetDirectory);
        foreach (string fileName in fileEntries)
            ProcessFile(fileName);

        // Recurse into subdirectories of this directory.
        string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
        foreach (string subdirectory in subdirectoryEntries)
            ProcessDirectory(subdirectory);
    }

    // Insert logic for processing found files here.
    public static void ProcessFile(string path)
    {
        Debug.Log("Processed file '{0}'. " + path);
    }
}
