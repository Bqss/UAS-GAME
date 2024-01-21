using System.Collections;
using System.Collections.Generic;
using Scripts.Player;
using UnityEngine;

public class Potion : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private int healAmount = 10;

    [SerializeField]
    private string potionName = "Base Potion";
    private Player player;
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (player != null)
            {
                player.addHP(10);
                Destroy(gameObject);
            }
        }
    }
}
