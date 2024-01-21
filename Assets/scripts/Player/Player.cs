using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

namespace Scripts.Player
{

  public class Player : MonoBehaviour
  {
    // Start is called before the first frame update

    public InputAction moveAction;
    public InputAction hitAction;
    public InputAction jumpAction;

    [SerializeField]
    private float jumpForce = 2;

    [SerializeField]
    private float MaxHP = 100f;

    [SerializeField]
    private float currentHP = 100f;

    [SerializeField]
    private float atkSpeed = 1f;
    [SerializeField]
    private float damage;
    private bool canAtk = true;
    public float speed = 5f;
    Rigidbody2D rigidbody;

    private int coinCount = 0;

    Vector2 move;
    private Vector2 initialPosition;
    Animator animator;
    Vector2 moveDirection = new Vector2(1, 0);

    private bool isDead = false;
    void Start()
    {
      initialPosition = transform.position;
      hitAction.Enable();
      moveAction.Enable();
      jumpAction.Enable();
      rigidbody = GetComponent<Rigidbody2D>();
      animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
      move = moveAction.ReadValue<Vector2>();
      isDead = animator.GetBool("isDead");


      if (canAtk && hitAction.triggered && !isDead && !animator.GetBool("isHurt"))
      {
        animator.SetBool("isAttack", true);
        canAtk = false;
        StartCoroutine(AttackCooldown());
      }
      if (!isDead)
      {
        animator.SetFloat("speedX", move.magnitude);
        animator.SetFloat("speedY", rigidbody.velocity.y);
        jumpHandler();
        movementHandler();
      }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
      if (other.tag == "ground")
      {
        animator.SetBool("isJump", false);
      }
      else if (other.tag == "DeathArea")
      {
        Debug.Log("Player is dead");
      }

    }

    void OnCollisionEnter2D(Collision2D other)
    {
      // if (other.gameObject.tag == "Enemy")
      // {
      //   if (!isDead)
      //   {
      //     animator.SetBool("isHurt", true);
      //     takeDamage(other.gameObject.GetComponent<Enemy>().getDamage());
      //     transform.Translate(new Vector2(-moveDirection.x * 1f, 0));
      //     moveDirection = new Vector2(-moveDirection.x * 1f, 0);
      //   }
      // }
    }
    void OnTriggerExit2D(Collider2D other)
    {
      if (other.tag == "ground")
      {
        animator.SetBool("isJump", true);
      }
    }

    void movementHandler()
    {
      if (!Mathf.Approximately(move.x, 0.0f))
      {
        animator.SetBool("isMove", true);
        moveDirection.Set(move.x, move.y);
        moveDirection.Normalize();

        if (move.x < 0.0f)
        {
          transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (move.x > 0.0f)
        {
          transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        if (move.x != 0)
        {
          transform.Translate(new Vector2(moveDirection.x * speed * Time.deltaTime, 0));
        }
      }
      else
      {
        animator.SetBool("isMove", false);
      }
    }

    void jumpHandler()
    {
      if (jumpAction.triggered)
      {
        rigidbody.velocity = Vector2.up * jumpForce;
      }
    }


    void FixedUpdate()
    {


    }

    public void dead()
    {
      animator.SetBool("isHurt", false);
      animator.SetBool("isDead", true);
    }

    public void deadCallBack()
    {
      SceneManager.LoadScene("deathScene");
    }

    void resetAttack()
    {
      animator.SetBool("isAttack", false);
    }
    void resethurt()
    {
      animator.SetBool("isHurt", false);
    }
    IEnumerator AttackCooldown()
    {
      float cooldown = 1f / atkSpeed;
      yield return new WaitForSeconds(cooldown);
      canAtk = true;
    }

    void disableAttackArea()
    {
      EdgeCollider2D collider = transform.GetChild(0).GetComponent<EdgeCollider2D>();
      collider.enabled = false;
    }

    void enableAttackArea()
    {
      EdgeCollider2D collider = transform.GetChild(0).GetComponent<EdgeCollider2D>();
      collider.enabled = true;
    }

    public void takeDamage(float dmg)
    {
      if (currentHP > 0)
      {
        currentHP -= dmg;
        UIHandler.instance.SetHealthBar(currentHP / MaxHP * 100);
      }

      if (currentHP <= 0)
      {
        dead();
      }
      else
      {

        animator.SetBool("isHurt", true);
      }

    }

    public void respawn()
    {
      transform.position = initialPosition;
      currentHP = MaxHP;
      UIHandler.instance.SetHealthBar(currentHP / MaxHP * 100);
      animator.SetBool("isDead", false);
    }

    public float getCurrentHP()
    {
      return currentHP;
    }

    public float getDamage()
    {
      return damage;
    }

    public void addHP(float amount)
    {
      currentHP += amount;
      if (currentHP > MaxHP)
      {
        currentHP = MaxHP;
      }
      UIHandler.instance.SetHealthBar((currentHP) / MaxHP * 100);
    }

    public void addCoin(int amount)
    {
      coinCount += amount;
      UIHandler.instance.setCoin(coinCount);
    }

  }
}