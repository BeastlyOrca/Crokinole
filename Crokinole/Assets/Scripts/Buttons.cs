using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons : MonoBehaviour
{

    public GameObject btnMove;
    public GameObject btnDone;
    // Start is called before the first frame update
    void Start()
    {
        btnMove.SetActive(true);
        btnDone.SetActive(false);
        
    }
    
}
