using UnityEngine;
using UnityEngine.UI;

public class PanelController : MonoBehaviour
{
    public GameObject GameObj;
    public RectTransform Rect;
    public Image Image;
    public Text Text;
    public Button ButtonClose;

    private void Start()
    {
        ButtonClose.onClick.AddListener(OnCloseClickHandler);
        GameObj.SetActive(true);
        SetState(false);
    }

    private void OnCloseClickHandler()
    {
        SetState(false);
    }

    public void SetState(bool state)
    {
        int colorA = state ? 1 : 0;
        Color newImageColor = new Color(Image.color.r, Image.color.g, Image.color.b, colorA);
        Color newTextColor = new Color(Text.color.r, Text.color.g, Text.color.b, colorA);

        Rect.localScale = state ? Vector3.one : Vector3.zero;
        Image.color = newImageColor;
        Text.color = newTextColor;
        ButtonClose.gameObject.SetActive(state);
    }
}
