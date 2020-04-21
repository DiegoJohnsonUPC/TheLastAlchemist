using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Stats
    public float health;
    public GameObject[] Drops;
    //Inmunity Time
    private int inmunityTime;
    private bool inmunity;
    private int count;
    
    void Start() {
        count = 0; 
        inmunityTime = 15;  
        inmunity = false;
    }
    void FixedUpdate()
    {
        if(health <= 0){
            for(int i = 0; i < Drops.Length; i++){
                Vector3 newPosition = transform.position;
                newPosition.z = 0;
                Instantiate(Drops[i],newPosition,Quaternion.identity);
            }
            
            Destroy(gameObject);

        }
        if(inmunity){
            if(count >= inmunityTime){
                inmunity = false;
                count = 0;
            }
            else{
                count++;
            }
        }
    }

    
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player")){
            Vector2 difference = transform.position - other.transform.position;
            transform.position = new Vector2(transform.position.x + (difference.x/2), transform.position.y + (difference.y/2));
        }
    }
    public void TakeDamage(int damage){
        if(inmunity == false){
            health -= damage;
            Debug.Log("Attack!");
            inmunity = true;
        }
        
    }
}
