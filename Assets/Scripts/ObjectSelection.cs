using UnityEngine;
using UnityEngine.UI;

public class ObjectSelection : MonoBehaviour
{
    const int maxObjects = 20;

    const float selectorHeight = 100.0f;
    const float selectorWidth = 100.0f;
    const float initialSelectorX = 150.0f;
    int selectorNum = 0;
    GameObject[] selectors = new GameObject[maxObjects];

    public GameObject objectsParent;
    public GameObject uiParent;
    MovableObject[] objects = new MovableObject[maxObjects];
    MovableObject selectedObject;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       foreach (Transform currChild in objectsParent.transform) {
            MovableObject currMovableObject = currChild.GetComponent<MovableObject>();
            objects[selectorNum] = currMovableObject;

            GameObject newSelector = new GameObject("Selector" + selectorNum);
            RectTransform rectTransform = newSelector.AddComponent<RectTransform>();
            Image image = newSelector.AddComponent<Image>();
            image.color = Color.white;
            rectTransform.SetParent(uiParent.transform);
            rectTransform.sizeDelta = new Vector2(selectorWidth, selectorHeight);
            rectTransform.anchoredPosition = new Vector2(0, initialSelectorX + selectorNum * selectorHeight);
            selectors[selectorNum] = newSelector;

            selectorNum++;
       } 
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < selectorNum; i++)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(selectors[i].GetComponent<RectTransform>(), Input.mousePosition))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (selectedObject != null)
                    {
                        selectedObject.isSelected = false;
                    }

                    selectedObject = objects[i];
                    selectedObject.isSelected = true;
                }
            }
        }
    }
}
