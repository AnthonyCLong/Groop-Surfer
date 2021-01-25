using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Globalization;
using UnityEngine.SceneManagement;

public class GroopSurferInterface : MonoBehaviour {
    
    public void saveAndClose() 
    {
        //SceneManager.UnloadSceneAsync("Customization Scene");
        //SceneManager.SetActiveScene(SceneManager.GetSceneByName("Main"));
        SceneManager.LoadScene("Main");

    }
}