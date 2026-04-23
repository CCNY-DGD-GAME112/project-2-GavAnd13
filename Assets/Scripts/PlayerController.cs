using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    //The camera is inside the player
    public Camera eyes;

    public Rigidbody RB;

    //Character stats
    public float MouseSensitivity = 3;
    public float WalkSpeed = 10;
    public float JumpPower = 7;

    //A list of all the solid objects I'm currently touching
    public List<GameObject> Floors;



    void Start()
    {
        //Turn off my mouse and lock it to center screen
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    void Update()
    {
        //If my mouse goes left/right my body moves left/right
        float xRot = Input.GetAxis("Mouse X") * MouseSensitivity;
        transform.Rotate(0, xRot, 0);

        //If my mouse goes up/down my aim (but not body) go up/down
        float yRot = -Input.GetAxis("Mouse Y") * MouseSensitivity;
        eyes.transform.Rotate(yRot, 0, 0);

        //Movement code
        if (WalkSpeed > 0)
        {
            //My temp velocity variable
            Vector3 move = Vector3.zero;

            //transform.forward/right are relative to the direction my body is facing
            if (Input.GetKey(KeyCode.W))
                move += transform.forward;
            if (Input.GetKey(KeyCode.S))
                move -= transform.forward;
            if (Input.GetKey(KeyCode.A))
                move -= transform.right;
            if (Input.GetKey(KeyCode.D))
                move += transform.right;
            //I reduce my total movement to 1 and then multiply it by my speed
            move = move.normalized * WalkSpeed;

            //If I hit jump and am on the ground, I jump
            if (JumpPower > 0 && Input.GetKeyDown(KeyCode.Space) && OnGround())
                move.y = JumpPower;
            else  //Otherwise, my Y velocity is whatever it was last frame
                move.y = RB.linearVelocity.y;

            //Plug my calculated velocity into the rigidbody
            RB.linearVelocity = move;
        }

        for (float i = 0; i <= .5; i += 0.05f)
        {
            SendRayPlane(eyes.transform.forward.y + i);
            SendRayPlane(eyes.transform.forward.y - i);
        }
    }

    //I count as being on the ground if I'm touching at least one solid object
    //This isn't a perfect way of doing this. Can you think of at least one way it might go wrong?
    public bool OnGround()
    {
        return Floors.Count > 0;
    }

    private void OnCollisionEnter(Collision other)
    {
        //If I touch something and it's not already in my list of things I'm touching. . .
        //Add it to the list
        if (!Floors.Contains(other.gameObject))
            Floors.Add(other.gameObject);
    }

    private void OnCollisionExit(Collision other)
    {
        //When I stop touching something, remove it from the list of things I'm touching
        Floors.Remove(other.gameObject);
    }

    public void OnDrawGizmos()
    {
        for (float i = 0; i <= .5; i += 0.05f)
        {
            Gizmos.color = Color.red;
            DrawRayPlane(eyes.transform.forward.y + i);
            DrawRayPlane(eyes.transform.forward.y - i);
        }
    }

    void DrawRayPlane(float y)
    {
        for (float i = 0; i <= 1; i += 0.05f)
        {
            Vector3 angle = new Vector3(eyes.transform.forward.x, y, eyes.transform.forward.z + i);
            angle = angle.normalized * 50;
            Gizmos.DrawRay(eyes.transform.position, angle);
            angle = new Vector3(eyes.transform.forward.x, y, eyes.transform.forward.z - i);
            angle = angle.normalized * 50;
            Gizmos.DrawRay(eyes.transform.position, angle);
        }
    }

    void SendRayPlane(float y)
    {
        for (float i = 0; i <= 1; i += 0.05f)
        {
            Vector3 angle = new Vector3(eyes.transform.forward.x, eyes.transform.forward.y, eyes.transform.forward.z + i);
            angle = angle.normalized * 50;
            Ray ray = new Ray(eyes.transform.position, angle);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform.GetComponent<WeepingAngel>() != null)
                    hit.transform.GetComponent<WeepingAngel>().seen = true;
                else if (hit.transform.GetComponent<Walker>() != null)
                    hit.transform.GetComponent<Walker>().seen = true;
            }
            angle = new Vector3(eyes.transform.forward.x, eyes.transform.forward.y, eyes.transform.forward.z - i);
            angle = angle.normalized * 50;
            ray = new Ray(eyes.transform.position, angle);
            if (Physics.Raycast(ray, out RaycastHit hitNeg))
            {
                if (hitNeg.transform.GetComponent<WeepingAngel>() != null)
                    hitNeg.transform.GetComponent<WeepingAngel>().seen = true;
                else if (hitNeg.transform.GetComponent<Walker>() != null)
                    hitNeg.transform.GetComponent<Walker>().seen = true;
            }
        }
    }
}