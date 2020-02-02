using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleLoader : MonoBehaviour
{
    public string level;
    void Update()
    {
        if (Input.anyKeyDown)
        {
            // Only specifying the sceneName or sceneBuildIndex will load the Scene with the Single mode
            SceneManager.LoadSceneAsync(level, LoadSceneMode.Single); 
        }
    }
}
