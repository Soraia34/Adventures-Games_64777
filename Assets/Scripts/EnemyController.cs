using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyController : MonoBehaviour
{
    public float speed;
    Rigidbody2D rigidbody2d;
    public bool vertical;
    public float changeTime = 3.0f;
    float timer;
    int direction = 1;
    Animator animator;
    bool broken = true;
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        timer = changeTime;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if(timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!broken)
      {
           return;
      }

        Vector2 position = rigidbody2d.position;
        if (vertical)
        {
            position.y = position.y + speed * direction * Time.deltaTime;
            animator.SetFloat("Mover X", 0);
            animator.SetFloat("Mover Y", direction);
        }
        else
        {
            position.x = position.x + speed * direction * Time.deltaTime;
            animator.SetFloat("Mover X", direction);
            animator.SetFloat("Mover Y", 0);
        }

         rigidbody2d.MovePosition(position);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();
        
        if( player != null)
        {
            player.ChangeHealth(-1);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
  {
     Destroy(gameObject);
  }

    public void Fix()
   {
       broken = false;
       rigidbody2d.simulated = false;
       animator.SetTrigger("Fixed");
   }
}
