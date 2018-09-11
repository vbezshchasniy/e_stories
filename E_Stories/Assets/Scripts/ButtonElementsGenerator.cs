using UnityEngine;
using UnityEngine.UI;

public class ButtonElementsGenerator : Generator
{
    public DataButtonElements ButtonElements;
    public GameObject MainObject;
    public int ObjectsCount;

    private PanelController Panel;

    private void Start()
    {
        Generate(MainObject, ObjectsCount);
        Panel = FindObjectOfType<PanelController>();
    }

    public override void Generate(GameObject obj, int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject item = Instantiate(obj, transform);
            item.GetComponent<Image>().sprite = GetRandomPicture();
            item.GetComponent<Button>().onClick.AddListener(OnButtonClickHandler);
        }
    }

    private void OnButtonClickHandler()
    {
        Panel.GameObj.SetActive(true);
        //Panel.SetState(true);
        //TODO
        // Use Server to download text;
    }

    private Sprite GetRandomPicture()
    {
        int i = UnityEngine.Random.Range(0, ButtonElements.Pictures.Count);
        return ButtonElements.Pictures[i];
    }
}