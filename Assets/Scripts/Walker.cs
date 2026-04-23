using System.Collections;
using UnityEngine;

public class Walker : PersonOpacity
{
    public bool seen = false;
    public bool prepWalking = false;
    public bool walking = false;
    public float walkTimer = 5f;
    public float walkSpeed = 0.1f;

    float timer;

    private void Start()
    {
        timer = walkTimer;
    }
    void Update()
    {
        LookAt();

        if (seen)
        {
            if (!prepWalking)
            {
                prepWalking = true;
                StartCoroutine(Walk());
            }
        }
        if (walking) {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                Vector3 targetVector = playerTF.position - parentTF.position;
                targetVector = targetVector.normalized;
                targetVector *= walkSpeed;
                parentTF.position += targetVector;
                Debug.Log(":)");
            }
            else
            {
                walking = false;
                timer = walkTimer;
                Debug.Log(":(");
            }
        }
    }

    public IEnumerator Walk()
    {
        Debug.Log(":|");
        yield return new WaitForSeconds(2f);
        walking = true;
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
