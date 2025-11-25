using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

[InitializeOnLoad]
public static class ForceStartScene
{
    // CHANGE THIS string to the exact path of your desired start scene
    static string pathOfFirstScene = "Assets/Scenes/SampleScene.unity";

    static ForceStartScene()
    {
        // This event runs every time you press the Play button
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
    }

    private static void OnPlayModeStateChanged(PlayModeStateChange state)
    {
        // Only run this logic when we are actively switching to Play Mode
        if (state == PlayModeStateChange.ExitingEditMode)
        {
            // Save the currently open scene so you don't lose work
            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        }
        
        // Once we are fully in Play Mode (but before the game starts)
        if (state == PlayModeStateChange.EnteredPlayMode)
        {
            // Check if we are already in the correct scene to avoid infinite reloading
            if (SceneManager.GetActiveScene().path != pathOfFirstScene)
            {
                EditorSceneManager.LoadScene(0); 
                // Or use EditorSceneManager.LoadScene(pathOfFirstScene); 
            }
        }
    }
}