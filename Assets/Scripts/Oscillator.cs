using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Oscillator : MonoBehaviour
{
    // ▼ "Reference Variables" for "Obstacles Movement" ▼
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    
    float movementFactor;
    [SerializeField] float period = 2f;




    // ▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬ "Start()" Method ▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬
    void Start()
    {
        // ▼ "Geting" the "Starting Position" of "Object" ▼
        startingPosition = transform.position;
    }





    // ▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬ "StaUpdatert()" Method ▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬
    void Update()
    {
        // ▼ "Protection" against "Dividing by Zero" ("NaN" - "Not a Number" Error) ▼
        if (period <= Mathf.Epsilon) { 
            // ▼ "Do nothing" and "Exit" from "Method" ▼
            return; 
        }

        // ▼ ThE "Mechanism" to "Measure" the "Time" 
        //      → "Continually Growing" over "Time" ▼
        float cycles = Time.time / period;  
        
        // ▼ "Constant Value" of "6.283" (PI = 3.14) ▼
        const float tau = Mathf.PI * 2;  
        
        // ▼ "Going" from "-1" to "1" ▼
        float rawSinWave = Mathf.Sin(cycles * tau);  

        // ▼ "Recalculated" to "Go" from "0" to "1" so "Cleaner" ▼
        movementFactor = (rawSinWave + 1f) / 2f;   
      

        // ▼ "Offset" Variable → for "Storing" the "Oscillant Movement" of "Object" ▼
        Vector3 offset = movementVector * movementFactor;
        
        // ▼ "Setting" the "Position" of "Object" with "Offset" Variable ▼
        transform.position = startingPosition + offset;
    }
}
