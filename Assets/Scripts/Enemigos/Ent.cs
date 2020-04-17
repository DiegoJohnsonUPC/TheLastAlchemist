using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ent : MonoBehaviour
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
        
        if(count==15){
            Vector2 prb = player.GetComponent<Rigidbody2D>().position;
            float total = 1.570796f;


            float distx = Mathf.Sqrt(Mathf.Pow(prb.x-rb.position.x,2));
            float disty = Mathf.Sqrt(Mathf.Pow(prb.y-rb.position.y,2));

            float d = Mathf.Sqrt(Mathf.Pow(prb.x-rb.position.x,2)+Mathf.Pow(prb.y-rb.position.y,2));
            
            


            if(d <= 5f){
                float xmoveangle = Mathf.Acos(distx/d);
                float ymoveangle = Mathf.Acos(disty/d);
                float ymove = ((xmoveangle*100)/total)/100;
                float xmove = ((ymoveangle*100)/total)/100;
                if(prb.x >= rb.position.x){
                    movement.x = xmove;
                }
                else{
                    movement.x = -1*xmove;
                }
                if(prb.y <= rb.position.y){
                    movement.y = -1*ymove;
                }
                else{
                    movement.y = ymove;
                }
                
                speed = 0.15f;
            }
            else{
                speed = 0.2f;
                randx = Random.Range(-1,2);
                movement.x = (float)randx;
                randy = Random.Range(-1,2);
                movement.y = (float)randy;
            }
            
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