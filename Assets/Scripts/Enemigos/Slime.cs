using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    GameObject player;
    //Variables de movimiento
    public float speed = 5f;
    public Rigidbody2D rb;
    public SpriteRenderer sr;
    private int randx;
    private int randy;
    private int count = 0;
    Vector2 movement;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
    }
    void FixedUpdate() {
        
        if(count==30){
            
            speed = 0.2f;
            randx = Random.Range(-1,2);
            movement.x = (float)randx;
            randy = Random.Range(-1,2);
            movement.y = (float)randy;
            
            if(movement.x < 0){
                sr.flipX = true;
            }
            else{
                sr.flipX = false;
            }
            
            //Debug.Log(movement);
            rb.MovePosition(rb.position+movement*speed*Time.fixedDeltaTime);
            count = 0;
        }
        else{
            count++;
            rb.MovePosition(rb.position+movement*speed*Time.fixedDeltaTime);
            
        }
        
    }
}