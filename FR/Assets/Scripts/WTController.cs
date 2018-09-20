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
	
	private void Start()
	{
		SetButtons();
	}

	private void SetButtons()
	{
		DataDialogues dataDialogues = ContentController.instance.DataDialogues;
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
		int currentNode = ContentController.instance.CurrentNode;
		ContentType = ContentController.instance.DataDialogues.Nodes[currentNode].TypeLoadingContent;
		switch (ContentType)
		{
			case ContentType.Model3D:
				SetLoadingContentState(false, "3d");
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