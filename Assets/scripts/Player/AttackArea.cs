using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts;
using Unity.VisualScripting;

namespace Scripts.Player
{

  
  public class AttackArea : MonoBehaviour
  {
    // Start is called before the first frame update
    private Player player ;
  
    void Awake(){
      player = GetComponentInParent<Player>();
    }
    void Start()
    { 

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
      if (other.tag.Equals("Enemy"))
      { 
        Debug.Log("enemy got hitted");
        Debug.Log(player.getDamage());
        Enemy enemy = other.GetComponent<Enemy>();
        Debug.Log(enemy);
        enemy.takeDamage(player.getDamage());
      }
    }
  }
}
