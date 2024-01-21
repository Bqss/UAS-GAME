using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

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



    private void deadCallback(){
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
      
      HP -= damage;
      if (HP <= 0)
      {
        animator.SetBool("is_iddle",false);
        animator.SetBool("is_dead", true);
      }else{
        animator.SetBool("is_hitted", true);
        animator.SetBool("is_iddle",false);
      }
    }

    public void attack()
    {
      canAtk = false;
      Vector2 pos = new Vector2(transform.position.x, transform.position.y);
      if (attackDirection > 0)
      {
        pos.x += 0.4f;
      }
      else
      {
        pos.x -= 0.4f;
      }
      GameObject projectileObject = Instantiate(projectilePrefab, pos - (Vector2.up * 0.19f), Quaternion.identity);
      Projectile projectile = projectileObject.GetComponent<Projectile>();
      projectile.Launch(new Vector2(attackDirection, 0), 100);
      animator.SetBool("attack", true);
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

    public void resetAttack()
    {
      animator.SetBool("attack", false);
      animator.SetBool("is_iddle",true);
    }

    public void resetHitted()
    {
      animator.SetBool("is_hitted", false);
    }



    void OnCollisionEnter2D(Collision2D other)
    {

    }

    

  }
}
