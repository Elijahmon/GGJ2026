using UnityEngine;
using UnityEngine.SceneManagement;

public class UITitleScreenController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public void OnStartWithGPSPressed()
    {
        SceneManager.LoadScene(1);
    }

    public void OnStartWithoutGPSPressed()
    {
        SceneManager.LoadScene(2);
    }
}
