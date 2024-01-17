using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIHandler : MonoBehaviour
{
    // Start is called before the first frame 
    private VisualElement healthBar;
    private UIDocument uIDocument;

    public static UIHandler instance { get; private set; }

    private void Awake()
    {
        instance = this;
    }



    void Start()
    {
        uIDocument = GetComponent<UIDocument>();
        healthBar = uIDocument.rootVisualElement.Q<VisualElement>("healtBar");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetHealthBar(float percentage)
    {
        healthBar.style.width = Length.Percent(percentage);
    } 
}
