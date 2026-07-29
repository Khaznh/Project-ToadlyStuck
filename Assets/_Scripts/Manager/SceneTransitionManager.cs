using UnityEngine;

public class SceneTransitionManager : Singleton<SceneTransitionManager>
{
    private void OnEnable()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void TransitionToScene(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
