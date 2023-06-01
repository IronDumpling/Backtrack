using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetRes : MonoBehaviour
{
    public Resolution[] res;
    void Start()
    {
        res = Screen.resolutions;
        Screen.SetResolution(2560,1440,true,144);

    }


}
