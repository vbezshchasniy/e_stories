using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class ContentController : MonoBehaviour
{
    public string URL;
    public Transform ContentPanel;
    public DataDialogues DataDialogues;
    public bool IsShowDialogue = true;
    public int CurrentNode;
    public GameObject CurrentContent;
    public GameObject LoaderPanel;
    public GameObject PreviewPanel;
    public GameObject TopPanel;
    public GameObject ScrollView;
    public Image StoryPanelBack;
    public List<VideoClip> Clips;

    //TODO fix this
    //Just for fun
    public Text BytesDownloadedText;
    //

    private WTController WtController;

    public static ContentController instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        LoaderPanel.SetActive(true);
        //WtController = AssetBundle.GetComponent<WTController>();
		StartCoroutine(LoadBundle());
    }

    private IEnumerator LoadBundle()
    {
        var www = new WWW(URL);
        while (!www.isDone)
        {
            yield return new WaitForSeconds(3f);
            BytesDownloadedText.text = www.bytesDownloaded.ToString();
            Debug.Log(string.Format("Bytes Downloaded: {0}", www.bytesDownloaded));
        }

        var myAsset = www.assetBundle;
        var bundle = myAsset.LoadAsset<GameObject>("AssetBundle");
        
        //TRY TEST WITH LIST
        //Clips = myAsset.LoadAllAssets<VideoClip>().ToList();
        
        CurrentContent = Instantiate(bundle, ContentPanel.transform.position, transform.rotation);
        CurrentContent.transform.SetParent(ContentPanel.transform, false);
        LoaderPanel.SetActive(false);
        WtController = CurrentContent.GetComponent<WTController>();
        UpdateData();
    }

    public void Respondent(int answer)
    {
        if (!IsShowDialogue)
            return;

        if (DataDialogues.Nodes[CurrentNode].PlayerAnswer[answer].SpeakEnd)
        {
            IsShowDialogue = false;
        }

        CurrentNode = DataDialogues.Nodes[CurrentNode].PlayerAnswer[answer].ToNode;
        CurrentContent.GetComponent<WTController>().BotText.text = DataDialogues.Nodes[CurrentNode].NpcText;
        WtController.UpdateContent();
        AnimateText();
    }

    public void AnimateText()
    {
        var mySequence = DOTween.Sequence();
        mySequence.Append(WtController.BotText.DOFade(0, .25f));
        mySequence.AppendCallback(UpdateData);
        mySequence.AppendInterval(.5f);
        mySequence.Append(WtController.BotText.DOFade(1, .25f));
    }

    private void UpdateData()
    {
        WtController.BotText.text = DataDialogues.Nodes[CurrentNode].NpcText;
    }

    public void TriggerVideoPlay(bool state)
    {
        ScrollView.SetActive(state);
        PreviewPanel.SetActive(state);
        TopPanel.SetActive(state);
        StoryPanelBack.color = new Color(StoryPanelBack.color.r, StoryPanelBack.color.g, StoryPanelBack.color.b, state ? 1 : 0);
    }
}

public enum ContentType
{
    Model3D,
    Video,
    Image,
    Sound,
    Empty
}