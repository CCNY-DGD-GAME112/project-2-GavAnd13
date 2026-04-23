using UnityEngine;
using UnityEngine.SceneManagement;

public class SetSceneMain : MonoBehaviour
{
    public void OnClick()
    {
        if (GameManager.instance != null)
            GameManager.instance.score = 0;
        SceneManager.LoadScene("Main");
    }
}
