using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PanelController : MonoBehaviour
{
    public GameObject GameObj;
    //public Text Text;
    public Button ButtonClose;
    //public float FadeDuration = 0.15f;
    //private RectTransform Rect;
    //private Image PanelImg;

    private void Start()
    {
        //Rect = GameObj.GetComponent<RectTransform>();
        //PanelImg = GameObj.GetComponent<Image>();
        ButtonClose.onClick.AddListener(OnCloseClickHandler);
    }

    private void OnCloseClickHandler()
    {
        GameObj.SetActive(false);
    }

    /*public void SetState(bool state)
    {
        int colorA = state ? 1 : 0;
        Color newImageColor = new Color(PanelImg.color.r, PanelImg.color.g, PanelImg.color.b, colorA);
        Color newTextColor = new Color(Text.color.r, Text.color.g, Text.color.b, colorA);
        ButtonClose.gameObject.SetActive(state);
        PanelImg.color = newImageColor;
        Text.color = newTextColor;
    }*/

    //IEnumerator UpdateColor(Color element, Color oldColor, Color newColor)
    //{
    //    yield return new WaitForEndOfFrame();
    //    float t = 0;
    //    while (t < 1)
    //    {
    //        element = Color.Lerp(newColor, oldColor, t);
    //        t += Time.deltaTime / FadeDuration;
    //    }
    //}
}
