using UnityEngine;

public class DeathPlane : MonoBehaviour
{
    private void OnTriggerEnter(UnityEngine.Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.instance.Lose();
        }
    }
}
