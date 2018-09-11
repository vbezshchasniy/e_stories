using UnityEngine;
using UnityEngine.UI;

public class ItemGenerator : Generator
{
    public CategoryButtons Buttons;
    public GameObject MainObject;
    public int ObjectsCount;

    private PanelController Panel;

    private void Start()
    {
        Generate(MainObject, ObjectsCount);

        GameObject PanelGO = GameObject.FindWithTag(Constants.ReadPanel);
        Panel = PanelGO.GetComponent<PanelController>();
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
        Panel.SetState(true);
        //TODO
        // Use Server to donwload text;
    }

    private Sprite GetRandomPicture()
    {
        int i = UnityEngine.Random.Range(0, Buttons.Pictures.Count);
        return Buttons.Pictures[i];
    }
}