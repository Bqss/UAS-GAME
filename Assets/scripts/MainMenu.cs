using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame 
    UIDocument document ;
    Button aboutBtn, playBtn, quitBtn;
    void Start()
    {
        document = GetComponent<UIDocument>();
        aboutBtn = document.rootVisualElement.Q<Button>("about_btn");
        playBtn = document.rootVisualElement.Q<Button>("play_btn");
        quitBtn = document.rootVisualElement.Q<Button>("exit_btn");

        aboutBtn.RegisterCallback<ClickEvent>(ev => {
            SceneManager.LoadScene("about");
        });

        playBtn.RegisterCallback<ClickEvent>(ev => {
            SceneManager.LoadScene("level 1");
        });

        quitBtn.RegisterCallback<ClickEvent>(ev => {
            Application.Quit();
        });


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
