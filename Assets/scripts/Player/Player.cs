using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

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
    private float currentHP  = 100f;

    [SerializeField]
    private float atkSpeed = 1f;
    [SerializeField]
    private float damage;
    private bool canAtk = true;
    public float speed = 5f;
    Rigidbody2D rigidbody;

    Vector2 move;
    Animator animator;
    Vector2 moveDirection = new Vector2(1, 0);


    void Start()
    {
      hitAction.Enable();
      moveAction.Enable();
      jumpAction.Enable();
      rigidbody = GetComponent<Rigidbody2D>();
      animator = GetComponent<Animator>();
      changeHealth(currentHP);
    }

    // Update is called once per frame
    void Update()
    {
      move = moveAction.ReadValue<Vector2>();
      movementHandler();
      jumpHandler();
      if (canAtk && hitAction.triggered)
      {
        animator.SetBool("isAttack", true);
        canAtk = false;
        StartCoroutine(AttackCooldown());
      }
      animator.SetFloat("speedX", move.magnitude);
      animator.SetFloat("speedY", rigidbody.velocity.y);

    }

    void OnTriggerEnter2D(Collider2D other)
    {
      if (other.tag == "ground")
      {
        animator.SetBool("isJump", false);
      }
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
      if (move.x != 0)
      {
        transform.Translate(new Vector2(moveDirection.x * speed * Time.deltaTime, 0));
      }

    }

    void resetAttack()
    {
      animator.SetBool("isAttack", false);
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

    public void changeHealth(float amount)
    {
      UIHandler.instance.SetHealthBar(amount/MaxHP* 100);
    }

  }
}