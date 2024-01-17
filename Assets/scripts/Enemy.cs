using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
  public class Enemy : MonoBehaviour
  {
    // Start is called before the first frame update
    [SerializeField]
    private float speed;
    [SerializeField]
    private float changeTime = 3.0f;
    [SerializeField]

    private float HP = 100f;
    private float delay = 1.0f;

    // Private variables
    Rigidbody2D rigidbody2d;
    Animator animator;
    float timer;
    int direction = 1;


    // Start is called before the first frame update
    void Start()
    {
      rigidbody2d = GetComponent<Rigidbody2D>();
      animator = GetComponent<Animator>();
      timer = changeTime + delay;
    }

    public float health(){
      return HP;
    }


    // FixedUpdate has the same call rate as the physics system
    void FixedUpdate()
    {
      timer -= Time.deltaTime;


      if (timer <= 0)
      {
        direction = -direction;
        timer = changeTime + delay;
      }
      if (timer >= delay)
      {
        Vector2 position = rigidbody2d.position;
        position.x = position.x + speed * direction * Time.deltaTime;
        rigidbody2d.MovePosition(position);
        charFacingHandler();
      }
    }

    private void changeHealth(float amount)
    {
      HP -= amount;
      if (HP <= 0)
      {
        death();
      }
    }

    private void death()
    {
      Destroy(gameObject);
    }
    void charFacingHandler()
    {
      if (direction == 1)
      {
        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
      }
      else
      {
        transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
      }
    }

    public void takeDamage(float damage)
    {
      changeHealth(-damage);
    }

  }
}
