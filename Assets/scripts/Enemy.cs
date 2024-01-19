using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Scripts
{
  public class Enemy : MonoBehaviour
  {
    // inputable variables
    [SerializeField]
    private float speed;
    [SerializeField]
    private float changeTime = 3.0f;
    [SerializeField]
    private float detectionRange = 5f;
    [SerializeField]
    private float HP = 100f;
    [SerializeField]
    private float delay = 1.0f;
    [SerializeField]
    private float damage = 10f;

    [SerializeField]
    private float attackSPD = 1f;
    // Private variables
    Rigidbody2D rigidbody2d;
    Animator animator;
    public GameObject projectilePrefab;

    float timer;
    int direction = 1;
    int attackDirection = 1;
    private Transform player;
    private bool isDetectPlayer = false;
    private bool canAtk = true;

    // private initialPosition;
    // Start is called before the first frame update


    public float getHP()
    {
      return HP;
    }

    public float getDamage()
    {
      return damage;
    }

    void Start()
    {
      rigidbody2d = GetComponent<Rigidbody2D>();
      animator = GetComponent<Animator>();
      timer = changeTime + delay;
      player = GameObject.FindGameObjectWithTag("Player").transform;
    }



    // FixedUpdate has the same call rate as the physics system
    void FixedUpdate()
    {
      float distanceToPlayerX = player.position.x - transform.position.x;
      float distanceToPlayerY = Math.Abs(player.position.y - 0.425f - transform.position.y);
      isDetectPlayer = Math.Abs(distanceToPlayerX) <= detectionRange && distanceToPlayerY <= 0.05f;


      if (isDetectPlayer)
      {
        if (distanceToPlayerX < 0)
        {
          faceLeft();
          attackDirection = -1;
        }
        else
        {
          faceRight();
          attackDirection = 1;
        }
        if (canAtk)
        {
          attack();
        }
      }
      else
      {
        patrol();
      }
    }

    private void patrol()
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
        faceRight();
      }
      else
      {
        faceLeft();
      }
    }

    public void takeDamage(float damage)
    {
      changeHealth(-damage);
    }

    public void attack()
    {
      canAtk = false;
      animator.SetTrigger("Launch");
      Vector2 pos = rigidbody2d.position;
      GameObject projectileObject = Instantiate(projectilePrefab, pos - (Vector2.up * 0.19f), Quaternion.identity);
      Projectile projectile = projectileObject.GetComponent<Projectile>();
      projectile.Launch(new Vector2(attackDirection, 0), 100);
      StartCoroutine(AttackCooldown());
    }

    private void faceRight()
    {
      transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    }

    private void faceLeft()
    {
      transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    }
    IEnumerator AttackCooldown()
    {
      float cooldown = 1f / attackSPD;
      yield return new WaitForSeconds(cooldown);
      canAtk = true;
    }

  }
}
