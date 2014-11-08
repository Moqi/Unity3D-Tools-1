using UnityEngine;
using UnityEditor;

// Iniciar sempre que apertar play
[InitializeOnLoad]
public class AutoSave
{
    static AutoSave()
    {
        EditorApplication.playmodeStateChanged = delegate
        {
            if (!EditorApplication.isPlayingOrWillChangePlaymode || EditorApplication.isPlaying)
                return;

            Debug.Log("Salvando cena " + EditorApplication.currentScene);

            EditorApplication.SaveAssets();
            EditorApplication.SaveScene();
        };
    }
}
