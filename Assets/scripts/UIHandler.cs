using System.Collections;
using System.Collections.Generic;
using Scripts.Player;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UIHandler : MonoBehaviour
{
  // Start is called before the first frame 
  private VisualElement healthBar;
  private Label coinCount;
  private string currentLevelScene = "level 1";
  private UIDocument uIDocument;

  public static UIHandler instance { get; private set; }


  Player player;

  private void Awake()
  {
    instance = this;
  }

  void Start()
  {
    uIDocument = GetComponent<UIDocument>();
    player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    healthBar = uIDocument.rootVisualElement.Q<VisualElement>("healtBar");
    coinCount = uIDocument.rootVisualElement.Q<Label>("coinCount");
    SetHealthBar(player.getCurrentHP());
  }

  // Update is called once per frame
  void Update()
  {

  }

  public string getCurrentScene()
  {
    return currentLevelScene;
  }

  public void setCurrentScene(string scene)
  {
    currentLevelScene = scene;
  }

  public void SetHealthBar(float percentage)
  {
    healthBar.style.width = Length.Percent(percentage);
  }

  public void setCoin(float amount)
  {
    coinCount.text = amount.ToString();
  }
}
