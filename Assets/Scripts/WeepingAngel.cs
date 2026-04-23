using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class WeepingAngel : PersonOpacity
{
    public TextMeshProUGUI scoreText;
    public float speed = 0.01f;
    public bool seen = false;
    float effectiveSpeed;

    void Update()
    {
        LookAt();

        if (seen)
            effectiveSpeed = 0;
        else
            effectiveSpeed = speed;

        Vector3 targetVector = playerTF.position - parentTF.position;
        targetVector = targetVector.normalized;
        targetVector *= effectiveSpeed;
        parentTF.position += targetVector;
        seen = false;
    }

    private void OnTriggerEnter(UnityEngine.Collider other)
    {
        if (other.gameObject.CompareTag("Rope"))
        {
            GameManager.instance.score++;
            GameManager.instance.scoreText.text = $"Score: {GameManager.instance.score}";
            Destroy(this.gameObject);
        }

        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.instance.Lose();
        }
    }
}
