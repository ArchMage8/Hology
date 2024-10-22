using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCode : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            
            PostProcessingHandler.instance.NoonEffect();
        } 
        
        if (Input.GetKeyDown(KeyCode.B))
        {
           
            PostProcessingHandler.instance.EveningEffect();
        }
       
        if (Input.GetKeyDown(KeyCode.C))
        {
            PostProcessingHandler.instance.DuskEffect();
        }

        
    }
}
