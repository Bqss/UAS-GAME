using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class retryGame : MonoBehaviour
{
  // Start is called before the first frame update
  private Button retryGameBtn;
  private UIDocument document;
  void Start()
  {
    document = GetComponent<UIDocument>();
    retryGameBtn = document.rootVisualElement.Q<Button>("retry_btn");
    retryGameBtn.clicked += () =>
    {
      SceneManager.LoadScene(UIHandler.instance.getCurrentScene());
    };
  }

  // Update is called once per frame
  void Update()
  {

  }
}
