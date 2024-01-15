using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{

    public UIDocument gui;
    // Start is called before the first frame update
    void Start()
    {
        if(gui == null)
        {
            gui = GetComponent<UIDocument>();
        }
        gui.rootVisualElement.Add(new Button(() => { Debug.Log("Hello World!"); }) { text = "Click me!" });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
