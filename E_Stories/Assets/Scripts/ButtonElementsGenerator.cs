using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonElementsGenerator : Generator
{
    public GameObject MainObject;
    public CategoryType ButtonElements;
    private PanelsController PanelController;
    private List<ButtonItem> ButtonItems;

    private void Start()
    {
        ButtonItems = new List<ButtonItem>(ButtonElements.CategoryItems.Length);
        Generate(MainObject, ButtonElements.CategoryItems.Length);
        PanelController = FindObjectOfType<PanelsController>();
    }

    public override void Generate(GameObject obj, int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject item = Instantiate(obj, transform);
            int j = i;
            item.GetComponent<Button>().onClick.AddListener(() => OnButtonClickHandler(j));
            item.AddComponent<ButtonItem>();
            ButtonItems.Add(item.GetComponent<ButtonItem>());
            ButtonItems[i].Id = GetRandomId();
            item.GetComponent<Image>().sprite = GetPicture(i);
        }
    }

    private void OnButtonClickHandler(int i)
    {
        PanelController.PreviewPanel.SetActive(true);
        PanelController.SetPreviewPanelItem(ButtonItems[i].Id);
    }

    private int GetRandomId()
    {
        return UnityEngine.Random.Range(1, ButtonElements.CategoryItems.Length + 1);
    }

    private Sprite GetPicture(int index)
    {
        int j = 0;
        for (int i = 0; i < ButtonElements.CategoryItems.Length; i++)
        {
            if (ButtonElements.CategoryItems[i].Id == ButtonItems[index].Id)
            {
                j = i;
                break;
            }
        }
        return ButtonElements.CategoryItems[j].Picture;
    }
}