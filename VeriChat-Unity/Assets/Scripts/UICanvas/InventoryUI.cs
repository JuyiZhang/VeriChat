using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryUI : MonoBehaviour
{
    public Canvas ThisCanvas;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void onButtonSelected() {
      var buttonName = EventSystem.current.currentSelectedGameObject.name;
      switch(buttonName) {
        case "FirstButton":
          break;
        case "SecondButton":
          break;
        case "ThirdButton":
          break;
        default:
          break;
      }
    }


}
