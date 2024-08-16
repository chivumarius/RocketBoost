using UnityEngine;
using UnityEngine.SceneManagement;


public class CollisionHandler : MonoBehaviour
{
    // ▼ "Variables" ▼
    [SerializeField] float levelLoadDelay = 2f;
    
    // ▼ "Audio Clips" ▼
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip crash;

    // ▼ "Particle Effects" ▼
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;

    // ▼ "References" to the "Audio Source" Component of "Rocket" ▼
    AudioSource audioSource;

    // ▼ "Variable" for "State" Transition ▼
    bool isTransitioning = false;

    // ▼ "Enabling" the "Collisions" → "Game Cheat" ▼
    // bool collisionDisabled = false;  





    // ▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬ "Start()" Method ▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬
    void Start() 
    {
        // ▼ "Getting" the "Audio Source" Component of "Rocket" ▼
        audioSource = GetComponent<AudioSource>();
    }




    // ▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬ "Update()" Method → for "Game Cheat" ▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬
    // void Update() 
    // {
    //    // ▼ "Calling" the "Method" ▼
    //    RespondToDebugKeys();    
    // }





    // ▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬ "Respond To DebugKeys()" Method for "Game Cheat"  ▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬
    // void RespondToDebugKeys()
    // {
    //     // ▼ "Checking" if the "L" Key is "Pressed" ▼
    //     if (Input.GetKeyDown(KeyCode.L))
    //     {
    //         // ▼ "Calling" the "Method" ▼
    //         LoadNextLevel();
    //     }

    //     // ▼ "Checking" if the "C" Key is "Pressed" ▼
    //     else if (Input.GetKeyDown(KeyCode.C))
    //     {
    //         // ▼ "Disabling" the "Collision State" ▼
    //         collisionDisabled = !collisionDisabled;  
    //     } 
    // }





    // ▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬ "On Cllision Enter()" Method ▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬
    void OnCollisionEnter(Collision other) 
    {
        // ▼ "Checking" if the "State" is "Transitioning" or "Collision Disabled" → for "Game Cheat"  ▼
        //if (isTransitioning || collisionDisabled) 
        
        if (isTransitioning)
        { 
            // ▼ "Leaving" the "Method" if the "State" is "Transitioning" ▼
            return; 
        }


        // ▼ "Switch" Statement  Game 
        //  → in whitch we "Compare" our "Object Tag"  
        //  → with "Case Constants Tags" ▼
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("This thing is friendly");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
           
            default:  //  ◄◄ "Everything Else" ◄◄
                StartCrashSequence();
                break;
        }
    }





    
    // ▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬ "Reload Level()" Method ▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬
    void ReloadLevel()
    {
        // ▼ "Getting" the "Scene Index" of "Current Scene"
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        
        // ▼ "Loading" the "Current Scene" ▼
        SceneManager.LoadScene(currentSceneIndex);
    }




    // ▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬ "Load Next Level()" Method ▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬
    void LoadNextLevel()
    {
        // ▼ "Getting" the "Current Scene Index" ▼
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        
        // ▼ "Getting" the "Next Scene Index"
        int nextSceneIndex = currentSceneIndex + 1;
        
        
        // ▼ "Checking" if "Next Scene Index" is the "Last Scene" ▼
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            // ▼ "Reloading" the "First Scene" ▼
            nextSceneIndex = 0;
        }
        
        // ▼ "Loading" the "Next Scene" ▼
        SceneManager.LoadScene(nextSceneIndex);
    }




    // ▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬ "Start Success Sequence()" Method ▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬
    void StartSuccessSequence()
    {
        // ▼ "Setting" the "State" of "Transitioning" to "True" ▼
        isTransitioning = true;
        
        // ▼ "Stop" the "Audio Sound Effect" ▼
        audioSource.Stop();


        // ▼ "Play One Shot" the "Success Audio Clip" ▼
        audioSource.PlayOneShot(success);

        // ▼ "Playing" the "Success Particles" ▼
        successParticles.Play();
        
        // ▼ Getting the "Movement Script" of "Rocket" 
        //      → and "Disabling" it (the "Player" has no "Control") ▼
        GetComponent<Movement>().enabled = false;
        
        // ▼ Creting a "Delay" for "Load Next Level()" Method 
        //      → by using "Invoke()" Method 
        //      → (we  will use "Coroutine" later) ▼
        Invoke("LoadNextLevel", levelLoadDelay);
    }





    // ▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬ "Start rash Sequence()" Method ▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬
    void StartCrashSequence()
    {
        // ▼ "Setting" the "State" of "Transitioning" to "True" ▼
        isTransitioning = true;
        
        // ▼ "Stop" the "Audio Sound Effect" ▼
        audioSource.Stop();
        
        // ▼ "Play One Shot" the "Crash Audio Clip" ▼
        audioSource.PlayOneShot(crash);

        // ▼ "Playing" the "Crash Particles" ▼
        crashParticles.Play();
        
        
        // ▼ Getting the "Movement Script" of "Rocket" 
        //      → and "Disabling" it (the "Player" has no "Control") ▼
        GetComponent<Movement>().enabled = false;
        
        // ♥ "Creating" a "Delay" for "Reload Level()" Method
        //      → by using "Invoke()" Method ▼
        Invoke("ReloadLevel", levelLoadDelay);
    }
}
