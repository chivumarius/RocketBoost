using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class QuitApplication : MonoBehaviour
{
    // ▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬ "Update()" Method ▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬
    void Update()
    {
        // ▼ "Enable" the "Escape" Key to "Quit" the "Game" ▼
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            // ▼ "Printing" to the "Console" Window ▼
            Debug.Log("we pushed escape");
            
            // ▼ "Calling" the "Quit()" Method of the "Application" Class ▼
            Application.Quit();
        }
    }
}
