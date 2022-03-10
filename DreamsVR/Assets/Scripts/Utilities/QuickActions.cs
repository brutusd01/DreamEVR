using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using Utilities;


public class QuickActions
{

    private const string MenuName = "QuickActions";

    [MenuItem(MenuName + "/Scenes/Open Bootstrap")]
    private static void BootScenes()
    {
        CloseAndOpenScenes(new[]
        {
            ScenePaths.BootstrapPath,
            ScenePaths.TestScenePath
        });
    }

    static void CloseAndOpenScenes(IEnumerable<string> scenePaths)
    {
        if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
        {
            var i = 0;
            foreach (string scene in scenePaths)
            {
                if (i == 0) EditorSceneManager.OpenScene(scene, OpenSceneMode.Single);
                else EditorSceneManager.OpenScene(scene, OpenSceneMode.Additive);
                i++;
            }
        }
    }
}
