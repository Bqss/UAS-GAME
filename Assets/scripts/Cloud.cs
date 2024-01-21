using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float velocity = 0.1f;
    private Vector2 position;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        position.x = transform.position.x - velocity * Time.deltaTime;
        transform.position = position;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "BoundLeft")
        {
          Transform transform = GameObject.FindWithTag("BoundRight").transform;
          position.x = transform.position.x;
          transform.position = position;
        }
    }
}
