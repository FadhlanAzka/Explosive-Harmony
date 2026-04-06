using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNavigation : MonoBehaviour
{

    public void PlayBeginner()
    {
        SceneManager.LoadScene("GameBeginner");
    }
    
    public void PlayIntermediate()
    {
        SceneManager.LoadScene("GameIntermediate");
    }
    
    public void PlayExpert()
    {
        SceneManager.LoadScene("GameExpert");
    }

    public void Office()
    {
        SceneManager.LoadScene("Main Menu");
    }


    public void Quit()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                        Application.Quit();
        #endif
    }
}