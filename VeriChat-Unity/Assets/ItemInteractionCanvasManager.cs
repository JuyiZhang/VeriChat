using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInteractionCanvasManager : MonoBehaviour
{
    public GameObject InteractionUIPrefab;
    public static ItemInteractionCanvasManager current { get; private set; }
    public GameObject _mainCamera;

    private GameObject InteractionUIObject;
    // Start is called before the first frame update
    void Start()
    {
      if(current == null) { current = this; }
      else { Destroy(gameObject); }
      InteractionUIPrefab.TryGetComponent<Canvas>(out Canvas InteractionUICanvasComponent);
			InteractionUICanvasComponent.renderMode = RenderMode.WorldSpace;
			InteractionUICanvasComponent.worldCamera = _mainCamera.GetComponent<Camera>();
      InteractionUIObject = Instantiate(InteractionUIPrefab);
      InteractionUIObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setCanvasLocation(Vector3 Position, float yAngle, GameObject ParentItem) {
      InteractionUIObject.SetActive(true);
      InteractionUIObject.transform.parent = ParentItem.transform;
      InteractionUIObject.TryGetComponent<RectTransform>(out RectTransform CanvasTransform);
      CanvasTransform.rotation = Quaternion.Euler(0, -yAngle/2/Mathf.PI*360, 0);
      CanvasTransform.localPosition = Position;
      CanvasTransform.anchoredPosition = Position;
      setCanvasContent(ParentItem.GetComponent<ItemObject>().referenceItem.interactions);
    }

    public void setCanvasContent(GameObjectInteraction[] buttons) {
      GameObject FirstButtonObject = InteractionUIObject.transform.Find("FirstButton").gameObject;
      GameObject SecondButtonObject = InteractionUIObject.transform.Find("SecondButton").gameObject;
      GameObject ThirdButtonObject = InteractionUIObject.transform.Find("ThirdButton").gameObject;
      switch (buttons.Length) {
        case 1:
          FirstButtonObject.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = buttons[0].InteractionTitle;
          FirstButtonObject.SetActive(true);
          SecondButtonObject.SetActive(false);
          ThirdButtonObject.SetActive(false);
          break;
        case 2:
          FirstButtonObject.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = buttons[0].InteractionTitle;
          SecondButtonObject.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = buttons[1].InteractionTitle;
          FirstButtonObject.SetActive(true);
          SecondButtonObject.SetActive(true);
          ThirdButtonObject.SetActive(false);
          break;
        case 3:
          FirstButtonObject.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = buttons[0].InteractionTitle;
          SecondButtonObject.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = buttons[1].InteractionTitle;
          ThirdButtonObject.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = buttons[2].InteractionTitle;
          FirstButtonObject.SetActive(true);
          SecondButtonObject.SetActive(true);
          ThirdButtonObject.SetActive(true);
          break;
        default:
          FirstButtonObject.SetActive(false);
          SecondButtonObject.SetActive(false);
          ThirdButtonObject.SetActive(false);
          break;
      }


    }

}
