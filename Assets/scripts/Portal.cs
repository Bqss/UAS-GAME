using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private string dest;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D other){
      if(other.gameObject.tag =="Player"){
        UIHandler.instance.setCurrentScene(dest);
        SceneManager.LoadScene(dest);
      }
    }
}
