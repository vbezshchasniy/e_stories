﻿using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelsController : MonoBehaviour
{
    public ContentController ContentControllerInst;
    public GameObject PreviewPanel;
    public GameObject StoryPanel;
    public GameObject NotConnectedPanel;
    public Button ExitBtn;
    public Button TryBtn;
    public Button ButtonClosePreviewPanel;
    public Button ButtonCloseStory;
    public Button ButtonOpenStory;
    public PreviewPanelItem PreviewPanelItems;
    private Image PreviewImage;
    private Image PreviewPanelBackground;
    private TextMeshProUGUI PreviewPanelDescriptionText;
    private Text PreviewPanelTitleText;

    private void Start()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            NotConnectedPanel.SetActive(true);
            TryBtn.onClick.AddListener(() => Application.LoadLevel(Application.loadedLevel));
            ExitBtn.onClick.AddListener(Application.Quit);
            return;
        }
        SetButtons();
        PreviewPanel.SetActive(false);
        StoryPanel.SetActive(false);
        NotConnectedPanel.SetActive(false);
        PreviewImage = GetChildGameObjectByName(PreviewPanel, "PreviewImage").GetComponent<Image>();
        PreviewPanelDescriptionText = GetChildGameObjectByName(PreviewPanel, "Description").GetComponent<TextMeshProUGUI>();
        PreviewPanelTitleText = GetChildGameObjectByName(PreviewPanel, "TitleTxt").GetComponent<Text>();
        PreviewPanelBackground = PreviewPanel.GetComponent<Image>();
    }

    private void Update()
    {
        #if UNITY_ANDROID
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        #endif
    }

    private void SetButtons()
    {
        ButtonClosePreviewPanel.onClick.AddListener(OnClosePreviewPanelClickHandler);
        ButtonOpenStory.onClick.AddListener(OnOpenStoryClickHandler);
        ButtonCloseStory.onClick.AddListener(OnCloseStoryClickHandler);
    }

    private void OnClosePreviewPanelClickHandler()
    {
        PreviewPanel.SetActive(false);
    }

    private void OnOpenStoryClickHandler()
    {
        StoryPanel.SetActive(true);
        ContentControllerInst.gameObject.SetActive(true);
    }

    private void OnCloseStoryClickHandler()
    {
        StoryPanel.SetActive(false);
        ContentControllerInst.TriggerVideoPlay(true);
        //TestCharacter.SetActive(false);
        //DialogueSystem.instance.IsShowDialogue = false;

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