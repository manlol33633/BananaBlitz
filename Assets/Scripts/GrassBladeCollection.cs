using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GrassBladeCollection : MonoBehaviour
{
    private int grassBladeCount = 0;
    private TMP_Text grassBladeCountText;
    void Start()
    {
        grassBladeCountText = GetComponent<TMP_Text>();
    }

    void Update()
    {
        
    }
}
