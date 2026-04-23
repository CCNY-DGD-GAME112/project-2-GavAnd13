using UnityEngine;
using UnityEngine.UIElements;

public class PersonOpacity : MonoBehaviour
{
    public Transform playerTF;
    public Transform parentTF;
    public float range = 5;
    void Update()
    {
        LookAt();
    }

    public void LookAt()
    {
        Vector3 lookAt = new Vector3(playerTF.position.x, this.gameObject.transform.position.y, playerTF.position.z);
        parentTF.LookAt(lookAt);

        Vector3 distanceVector = this.gameObject.transform.position - playerTF.position;
        float distanceFloat = Mathf.Sqrt(Mathf.Pow(distanceVector.x, 2) + Mathf.Pow(distanceVector.y, 2) + Mathf.Pow(distanceVector.z, 2));
        float opacity = distanceFloat / range;
        if (opacity > 1)
            opacity = 1;
        this.gameObject.GetComponent<MeshRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, opacity);
    }
}
