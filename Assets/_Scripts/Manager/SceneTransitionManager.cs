using UnityEngine;

public class SceneTransitionManager : Singleton<SceneTransitionManager>
{
    public void TransitionToScene(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
