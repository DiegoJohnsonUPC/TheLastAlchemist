using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Controller : MonoBehaviour
{
    //Stats
    public float health;
    //UI
    public Text healthUI;
    //
    public float speed = 5f;
    public Animator animator;
    public Rigidbody2D rb;
    private float direction;
    Vector2 movement;
    //attack weapon
    private float timeBtwAttack;
    public float startTimeBtwAttack;
    Vector2 attackPosition;
    public LayerMask whatIsEnemies;
    public float attackRange;
    public int damage;
    //
    float xchange;
    float ychange;
   
    void Start()
    {
        attackPosition.x = transform.position.x + 0;
        Debug.Log(attackPosition.x);
        attackPosition.y = transform.position.y - 0.12f;
        Debug.Log(attackPosition.y);
    }
    void Update()
    {
        if(health == 0){
            Destroy(gameObject);
        }
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        animator.SetFloat("X",movement.x);
        animator.SetFloat("Y",movement.y);
        animator.SetFloat("Speed",movement.sqrMagnitude);
        if(movement.x > 0){
            direction = 0.53f;
            xchange = 0.12f;
            ychange = 0;
        }
        if(movement.x < 0){
            direction = 0.87f;
            xchange = -0.12f;
            ychange = 0;
        }
        if(movement.y < 0){
            direction = 0.01f;
            xchange = 0;
            ychange = -0.12f;
        }
        if(movement.y > 0){
            direction = 0.26f;
            xchange = 0;
            ychange = 0.12f;
        }
        if(timeBtwAttack >= 0){
            if(Input.GetKey(KeyCode.Space)){
                

                attackPosition.x = transform.position.x + xchange;
                attackPosition.y = transform.position.y + ychange;
                
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPosition, attackRange, whatIsEnemies);
                for(int i = 0; i < enemiesToDamage.Length;i++){
                    enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(damage);
                }
                timeBtwAttack = startTimeBtwAttack;
            }
            timeBtwAttack = startTimeBtwAttack;
        }
        else{
            timeBtwAttack -= Time.deltaTime;
        }
        animator.SetFloat("Direction",direction);
    }

    void FixedUpdate() {
        rb.MovePosition(rb.position+movement*speed*Time.fixedDeltaTime);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Enemy")){
            Vector2 difference = transform.position - other.transform.position;
            transform.position = new Vector2(transform.position.x + (difference.x/2), transform.position.y + (difference.y/2));
            health -= 5;
            healthUI.text = "Vida = " + health;
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPosition,attackRange);
    }
}
