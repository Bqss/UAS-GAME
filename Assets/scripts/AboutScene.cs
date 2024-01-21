using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AboutScene : MonoBehaviour
{
  // Start is called before the first frame update
  UIDocument document;
  void Start()
  {
    document = GetComponent<UIDocument>();
    Button backBtn = document.rootVisualElement.Q<Button>("back_btn");
    backBtn.RegisterCallback<ClickEvent>(ev =>
    {
      UnityEngine.SceneManagement.SceneManager.LoadScene("menu");
    });
  }

  // Update is called once per frame
  void Update()
  {

  }
}
