using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FysiskTerning : MonoBehaviour
{
    [SerializeField] Vector3 StartPosisjon;
    //[SerializeField] Vector3 kraft;

    public int selectedVector;
    public Vector3[] vectorPoints;
    
    public int[] resultater = { 1, 2, 3, 4, 5, 6 };
    public int valgtResultat;
    

    //readonly List<int> FaceRepresent = new List<int>() { 0, 1, 2, 3, 4, 5, 6 };

    
    //readonly List<string> FaceRepresent = new List<string>() {"","I","II","III","IV","V","VI"};
    void Start()
    {
        
    }

    public void KastTerningen()
    {
        gameObject.SetActive(true);
        transform.position = StartPosisjon;
        Vector3 kraft = new Vector3(250, 0, -250);
        GetComponent<Rigidbody>().AddForce(kraft);
    }


    public int LesTerningen()
    {
        return valgtResultat;
    }

    void Update()
    {
        float bestDot = -1;
        for (int i = 0; i < vectorPoints.Length; i++)
        {

            var valueVector = vectorPoints[i];
            var worldSpaceValueVector = transform.localToWorldMatrix.MultiplyVector(valueVector);
            float dot = Vector3.Dot(worldSpaceValueVector, Vector3.up);

            if (dot > bestDot)
            {
                bestDot = dot;
                selectedVector = i;
            }
        }

        valgtResultat = resultater[selectedVector];

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > 2)
        {
            GetComponent<AudioSource>().Play();
        }
        
    }
    

}
    

    

