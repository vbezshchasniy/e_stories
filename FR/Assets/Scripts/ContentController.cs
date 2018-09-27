﻿using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ContentController : MonoBehaviour
{
    public string URL;
    public Transform ContentPanel;
    public DataDialogues DataDialogues;
    public bool IsShowDialogue = true;
    public int CurrentNode;
    public GameObject CurrentContent;
    public GameObject LoaderPanel;
    public GameObject AssetBundle;
    public GameObject PreviewPanel;
    public GameObject TopPanel;
    public GameObject ScrollView;
    public Image StoryPanelBack;

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

        /////
        AssetBundle.SetActive(true);
        LoaderPanel.SetActive(false);
        WtController = AssetBundle.GetComponent<WTController>();
        UpdateData();
////


//		StartCoroutine(LoadBundle());
    }

    private IEnumerator LoadBundle()
    {
        var www = new WWW(URL);
        while (!www.isDone)
        {
            yield return null;
            BytesDownloadedText.text = www.bytesDownloaded.ToString();
            Debug.Log(string.Format("Bytes Downloaded: {0}", www.bytesDownloaded));
        }


        var myAsset = www.assetBundle;
        var bundle = myAsset.LoadAsset<GameObject>("AssetBundle");
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


        ////
        TriggerVideoPlay(false);
        ///
    }


    public void TriggerVideoPlay(bool state)
    {
        ScrollView.SetActive(state);
        PreviewPanel.SetActive(state);
        TopPanel.SetActive(state);
        StoryPanelBack.color = new Color(StoryPanelBack.color.r, StoryPanelBack.color.g, StoryPanelBack.color.b,
            state ? 1 : 0);
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