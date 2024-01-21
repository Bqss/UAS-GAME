using System.Collections;
using System.Collections.Generic;
using Scripts.Player;
using UnityEngine;

public class Coin : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]private int coinValue = 1;
    Player player;
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

      void OnTriggerEnter2D(Collider2D collider){
        if (collider.CompareTag("Player")){
          player.addCoin(coinValue);
          Destroy(gameObject);
        }
    }
}
