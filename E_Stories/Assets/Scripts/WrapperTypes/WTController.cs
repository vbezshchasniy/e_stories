using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WTController : MonoBehaviour
{
	public List<GameObject> LoadingContent;
	public List<Button> WtButtons;
	[SerializeField]
	public TextMeshProUGUI BotText;
	
	private void Start()
	{
		DataDialogues dataDialogues = ContentController.instance.DataDialogues;
//		BotText = FindObjectOfType<DialogueSystem>().GetComponentInChildren<TextMeshProUGUI>();
		int currentNode = ContentController.instance.CurrentNode;
		int length = dataDialogues.Nodes[currentNode].PlayerAnswer.Length;

		//TODO check if buttons.lengh == to buttons.lenght on server side
		for (int i = 0; i < length; i++)
		{
			if (WtButtons.Count == length)
			{
				int j = i;
				WtButtons[i].onClick.AddListener(() => ContentController.instance.Respondent(j));
				WtButtons[i].GetComponentInChildren<Text>().text = dataDialogues.Nodes[currentNode].PlayerAnswer[j].Text;
			}
		}
	}

	public void UpdateContent()
	{
		switch (ContentController.instance.DataDialogues.Nodes[ContentController.instance.CurrentNode].TypeLoadingContent)
		{
			case ContentType.Model3D:
				SetLoadingContentState(false, "3d");
//				foreach (var item in LoadingContent)
//				{
//					if (item.tag == "3d")
//					{
//						item.SetActive(false);
//					}
//				}
				break;
			case ContentType.Video:
				SetLoadingContentState(false, "video");
				break;
			case ContentType.Image:
				SetLoadingContentState(false, "image");
				break;
			case ContentType.Sound:
				SetLoadingContentState(false, "audio");
				break;
			case ContentType.Empty:
				SetLoadingContentState(false, "empty");
				break;
			default:
				break;
		}
	}

	private void SetLoadingContentState(bool state, string type)
	{
		foreach (var item in LoadingContent)
		{
			if (item.CompareTag(type))
			{
				item.SetActive(!state);
				continue;
			}
			item.SetActive(state);
		}

	}
}
