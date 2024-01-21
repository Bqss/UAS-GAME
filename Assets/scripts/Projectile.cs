using System.Collections;
using System.Collections.Generic;
using Scripts;
using Scripts.Player;
using UnityEngine;

public class Projectile : MonoBehaviour
{
  // Start is called before the first frame update

  Rigidbody2D rb;
  Player player;
  Enemy enemy;
  void Start()
  {

  }

  void Awake()
  {
    rb = GetComponent<Rigidbody2D>();
    player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>();
  }

  public void Launch(Vector2 direction, float force)
  {
    rb.AddForce(direction * force);
  }

  // Update is called once per frame
  void Update()
  {

  }

  void OnCollisionEnter2D(Collision2D other)
  {
    string otherTag = other.gameObject.tag;
    if (otherTag == "Player")
    {
      player.takeDamage(enemy.getDamage());
      Destroy(gameObject);
    }else if(otherTag == "BoundLeft" || otherTag == "BoundRight")
    {
      Destroy(gameObject);
    }
    
  }
}
