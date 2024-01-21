using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InitialScene : MonoBehaviour
{
  // Start is called before the first frame update
  private Button startGameBtn;
  private UIDocument document;

  void Start()
  {
    document = GetComponent<UIDocument>();
    startGameBtn = document.rootVisualElement.Q<Button>("startgame_btn");
    startGameBtn.clicked += () =>
    {
      UnityEngine.SceneManagement.SceneManager.LoadScene("menu");
    };
  }

  // Update is called once per frame
  void Update()
  {

  }
}
