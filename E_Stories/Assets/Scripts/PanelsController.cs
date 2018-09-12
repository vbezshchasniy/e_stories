using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelsController : MonoBehaviour
{
    public bool IsServer;
    public GameObject PreviewPanel;
    public GameObject StoryPanel;
    public Button ButtonClosePreviewPanel;
    public Button ButtonCloseStory;
    public Button ButtonOpenStory;
    public PreviewPanelItem PreviewPanelItems;
    [SerializeField]
    private string ServerURL;
    [SerializeField]
    private string LocalPath;
    private WWW www;
    private Image PreviewImage;
    private Image PreviewPanelBackground;
    private TextMeshProUGUI PreviewPanelDescriptionText;
    private Text PreviewPanelTitleText;

    private void Start()
    {
        if (IsServer)
        {
            SendStringState(ServerURL, "Server URL is empty");
            StartCoroutine(LoadImageFromServer());
        }
        else
        {
            SendStringState(LocalPath, "Local path is empty");
            StartCoroutine(LoadImageFromLocalPath());
        }
        PreviewPanel.SetActive(false);
        StoryPanel.SetActive(false);
        ButtonClosePreviewPanel.onClick.AddListener(OnClosePreviewPanelClickHandler);
        ButtonOpenStory.onClick.AddListener(OnOpenStoryClickHandler);
        ButtonCloseStory.onClick.AddListener(OnCloseStoryClickHandler);
        PreviewImage = GetChildGameObjectByName(PreviewPanel, "PreviewImage").GetComponent<Image>();
        PreviewPanelDescriptionText = GetChildGameObjectByName(PreviewPanel, "Description").GetComponent<TextMeshProUGUI>();
        PreviewPanelTitleText = GetChildGameObjectByName(PreviewPanel, "TitleTxt").GetComponent<Text>();
        PreviewPanelBackground = PreviewPanel.GetComponent<Image>();
    }

    private void OnClosePreviewPanelClickHandler()
    {
        PreviewPanel.SetActive(false);
    }

    private void OnOpenStoryClickHandler()
    {
        StoryPanel.SetActive(true);
    }

    private void OnCloseStoryClickHandler()
    {
        StoryPanel.SetActive(false);
    }
    
    private void SendStringState(string purpose, string sendText)
    {
        if (string.IsNullOrEmpty(purpose))
        {
            Debug.Log(sendText);
        }
    }

    private IEnumerator LoadImageFromLocalPath()
    {
        www = new WWW(LocalPath);
        yield return www;
        SetSprite();
    }

    private IEnumerator LoadImageFromServer()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            Debug.Log("Network not reachable");
            yield return null;
        }
        www = new WWW(ServerURL);
        Debug.Log("Download image on progress");
        yield return www;
        if (string.IsNullOrEmpty(www.text))
        {
            Debug.Log("Download is failed");
        }
        else
        {
            Debug.Log("Download is success");
            SetSprite();
        }
    }

    private void SetSprite()
    {
        Texture2D texture2D = new Texture2D(1, 1);
        www.LoadImageIntoTexture(texture2D);
        Sprite sprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), Vector2.one / 2);
        PreviewImage.sprite = sprite;
    }
    
    private static GameObject GetChildGameObjectByName(GameObject fromGameObject, string withName)
    {
        Transform[] ts = fromGameObject.transform.GetComponentsInChildren<Transform>(true);
        foreach (Transform item in ts)
        {
            if (item.gameObject.name == withName)
            {
                return item.gameObject;
            }
        }
        return null;
    }

    public void SetPreviewPanelItem(int id)
    {
        for (int i = 0; i < PreviewPanelItems.Items.Length; i++)
        {
            if (PreviewPanelItems.Items[i].Id == id)
            {
                PreviewImage.sprite = PreviewPanelItems.Items[i].Picture;
                PreviewPanelDescriptionText.text = PreviewPanelItems.Items[i].Text;
                PreviewPanelTitleText.text = PreviewPanelItems.Items[i].Name;
                PreviewPanelBackground.sprite = PreviewPanelItems.Items[i].Background;
                break;
            }
        }
        
    }
}
