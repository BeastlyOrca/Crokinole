using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Required to work with scenes

public class ResetButton : MonoBehaviour
{
    // Public function to reset the scene
    public void ResetScene()
    {
        // Reload the currently active scene
        SceneManager.LoadScene(1);
    }
}
