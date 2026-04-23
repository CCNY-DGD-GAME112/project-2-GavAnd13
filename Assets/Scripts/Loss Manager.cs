using TMPro;
using UnityEngine;

public class LossManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        scoreText.text = $"Your score was: {GameManager.instance.score}";
    }
}
