using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    public float speed = 5f;
    public Animator animator;
    public Rigidbody2D rb;

    Vector2 movement;
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        animator.SetFloat("X",movement.x);
        animator.SetFloat("Y",movement.y);
        animator.SetFloat("Speed",movement.sqrMagnitude);

    }

    void FixedUpdate() {
        rb.MovePosition(rb.position+movement*speed*Time.fixedDeltaTime);
    }
}
