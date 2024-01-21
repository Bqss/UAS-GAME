using System.Collections;
using System.Collections.Generic;
using Scripts.Player;
using UnityEngine;

public class DeathArea : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other){
      if(other.tag == "Enemy"){
        Destroy(other.gameObject);
      }else if(other.gameObject.tag == "Player"){
        Player player = other.GetComponent<Player>();
        player.dead();
      }
    }
}
