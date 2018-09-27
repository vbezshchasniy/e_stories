using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WTController : MonoBehaviour
{
	public List<GameObject> LoadingContent;
	public List<Button> WtButtons;
	public ContentType ContentType;
	public TextMeshProUGUI BotText;

	public List<GameObject> VideoContent;
	public List<GameObject> ImageContent;
	public List<GameObject> AudioContent;
	public List<GameObject> Content3d;
	private int CurrentNode;
	
	private void Start()
	{
		SetButtons();
	}

	private void SetButtons()
	{
		DataDialogues dataDialogues = ContentController.instance.DataDialogues;
		CurrentNode = ContentController.instance.CurrentNode;
		int length = dataDialogues.Nodes[CurrentNode].PlayerAnswer.Length;

		//TODO check if buttons.lengh == to buttons.lenght on server side
		for (int i = 0; i < length; i++)
		{
			if (WtButtons.Count == length)
			{
				int j = i;
				WtButtons[i].onClick.AddListener(() => ContentController.instance.Respondent(j));
				WtButtons[i].GetComponentInChildren<Text>().text = dataDialogues.Nodes[CurrentNode].PlayerAnswer[j].Text;
			}
		}
	}

	public void UpdateContent()
	{
		ContentController.instance.TriggerVideoPlay(true);
		CurrentNode = ContentController.instance.CurrentNode;
		ContentType = ContentController.instance.DataDialogues.Nodes[CurrentNode].TypeLoadingContent;
		switch (ContentType)
		{
			case ContentType.Model3D:
				SetLoadingContentState(false, "3d", Content3d);
				break;
			case ContentType.Video:
				ContentController.instance.TriggerVideoPlay(false);
				SetLoadingContentState(false, "video", VideoContent);
				break;
			case ContentType.Image:
				SetLoadingContentState(false, "image", ImageContent);
				break;
			case ContentType.Sound:
				SetLoadingContentState(false, "audio", AudioContent);
				break;
			case ContentType.Empty:
				SetLoadingContentState(false, "empty");
				break;
		}
	}

	private void SetLoadingContentState(bool state, string type, List<GameObject> currentContent = null)
	{
		foreach (var item in LoadingContent)
		{
			if (item.CompareTag(type))
			{
				if (currentContent != null)
				{
					if (!currentContent.Contains(item))
					{
						currentContent.Add(item);
					}
				}
				//continue;
			}
			item.SetActive(state);
		}

		if (currentContent != null)
		{
			if (currentContent.Count > 1)
			{
				int randomNumContentItem = Random.Range(0, currentContent.Count);
				currentContent[randomNumContentItem].SetActive(!state);
			}
			else
			{
				currentContent[0].SetActive(!state);
			}
		}

	}
}