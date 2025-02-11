using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5;

    void Update()
    {
        Vector3 move = new Vector3 (Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        transform.position += move.normalized * speed * Time.deltaTime;

    }
}
