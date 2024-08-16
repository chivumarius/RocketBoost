using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Movement : MonoBehaviour
{
    // • "PARAMETERS" → for "Tuning", Typically S"et in the "Editor"
    // • "CACHE" → e.g. "References" for "Readability" or "Speed"
    // • "STATE" → "Private Instance Variables" ("Member") 


    // ▼ "Settings" the "Force/Speed" for "Pushing" and "Rotating" the "Rocket" ▼
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotationThrust = 1f;
    [SerializeField] AudioClip mainEngine;

    // ▼ "Particles Emitters" for "Pushing" and "Rotating" the "Rocket" ▼
    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem leftThrusterParticles;
    [SerializeField] ParticleSystem rightThrusterParticles;

    // ▼ "Variable" as "Reference" to "Rigidbody" Component ▼
    Rigidbody rb;

    // ▼ "Reference" to "Audio Source" Component of "Rocket" ▼
    AudioSource audioSource;

   



    // ▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬ "Start()" Method 
    //      → is "Called Only Once" before the "First Execution" of "Update()" Method ▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬
    void Start()
    {
        // ▼ "Getting" the "Rigidbody" Component ▼
        rb = GetComponent<Rigidbody>();

        // ▼ "Getting" the "Audio Source" Component of "Rocket" ▼
        audioSource = GetComponent<AudioSource>();
    }




    // ▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬ "Update()" Method 
    //      → is "Called Only Once" per "Frame" ▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬
    void Update()
    {
        // ▼ "Calling" the "Methods" ▼
        ProcessThrust();
        ProcessRotation();
    }





    // ▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬ "Process Thrust()" Method
    //      → for "Push/Thrust" the "Spaceship" ▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬
    void ProcessThrust()
    {        // ▼ "Checking" if the "Space" Key (for "Push/Thrust") is "Pressed" ▼
        if (Input.GetKey(KeyCode.Space))
        {           
            // ▼ "Calling" the "Method" ▼
            StartThrusting();
        }
        else
        {
            // ▼ "Stop Playing" the "Audio Source" ▼
            StopThrusting();
        }
    }

       
    






    // ▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬ "Process Rotation()" Method 
    //      → for "Rotate" the "Spaceship" ▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬
    void ProcessRotation()
    {
        // ▼ "Checking" if the "A" Key (for "Left") 
        //      → or "D" Key (for "Right") is "Pressed" ▼
        if (Input.GetKey(KeyCode.A))
        {
            // ▼ "Calling" the "Method" ▼
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            // ▼ "Calling" the "Method" ▼
            RotateRight();
        }
        else 
        {
            // ▼ "Calling" the "Method" ▼
            StopRotating();
        }

    }





    // ▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬ "Start Thrusting()" Method ▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬
    void StartThrusting()
    {
        // ▼ "Accessing" the "Add Relative Force()" Method 
        //      → to "Set" the "Upward" Force 
        //      → for "Pushing" the "Spaceship" on the "Y-Axis" 
        //      → and "Multiplied" with "Main Thrust/Push Force"
        //      → and "Multiplied" with "Time.deltaTime" for "Freme Rate Independence" ▼
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);    // ◄◄ "(Vector3.up)" = "(0, 1, 0)" ◄◄             
        
        
        // ▼ "Checking" if the "Audio Source" is "Not Playing" ▼
        if (!audioSource.isPlaying)
        {              
            // ▼ "Play One Shot" the "Audio Clip" ▼
            audioSource.PlayOneShot(mainEngine);
        }

        // ▼ "Checking" if the "Main Engine Particles" are "Not Playing" ▼
        if (!mainEngineParticles.isPlaying)
        {
            mainEngineParticles.Play();
        }
    }




    // ▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬ "Stop Thrusting()" Method ▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬
    private void StopThrusting()
    {
         // ▼ "Stop Playing" the "Audio Source" ▼
        audioSource.Stop();

        // ▼ "Stop Playing" the "Main Engine Particles" ▼
        mainEngineParticles.Stop();
    }





    // ▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬ "Rotate Left()" Method ▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬
    private void RotateLeft()
    {
        // ▼ "Calling" the "Method" with a "Positive Argument" ▼
        ApplyRotation(rotationThrust);

        // ▼ "Checking" if the "Right Thruster Particles" are "Not Playing" ▼
        if (!rightThrusterParticles.isPlaying)
        {
            rightThrusterParticles.Play();
        }
    }





    // ▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬ "Rotate Right()" Method ▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬
    private void RotateRight()
    {
         // ▼ "Calling" the "Method" with a "Negative Argument" ▼
        ApplyRotation(-rotationThrust);

        // ▼ "Checking" if the "Left Thruster Particles" are "Not Playing" ▼ 
        if (!leftThrusterParticles.isPlaying)
        {
            leftThrusterParticles.Play();
        }
    }




    // ▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬ "Stop Rotating()" Method ▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬
    private void StopRotating()
    {
        // ▼ "Stop Playing" the "Right Thruster Particles" ▼
        rightThrusterParticles.Stop();

        // ▼ "Stop Playing" the "Left Thruster Particles" ▼
        leftThrusterParticles.Stop();
    }





    // ▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬ "Apply Rotation()" Method ▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬
    void ApplyRotation(float rotationThisFrame)
    {
        // ▼ "Freezing" the "Rotation" of the "Spaceship"
        //      → to allow us to "Rotate" it "Manually" ▼
        rb.freezeRotation = true;  


        // ▼ "Accessing" the "Rotate" Property of the "Transform" Component 
        //      → to "Rotate" the "Spaceship" on the "Z-Axis"
        //      → and "Multiplied" with "Rotation Thrust" 
        //      → and "Multiplied" with "Time.deltaTime" for "Freme Rate Independence" ▼
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);  // ◄◄ "(Vector3.forward)" = "(0, 0, 1)" ◄◄
    
    
        // ▼ "Unfreezing" the "Rotation" of the "Spaceship" 
        //      → to allow to the "Physics System" to "Take Over" ▼
        rb.freezeRotation = false;  
    }
}
