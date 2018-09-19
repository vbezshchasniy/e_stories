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
		BotText = FindObjectOfType<DialogueSystem>().GetComponentInChildren<TextMeshProUGUI>();
		int currentNode = ContentController.instance.CurrentNode;
		int length = dataDialogues.Nodes[currentNode].PlayerAnswer.Length;

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
				foreach (var item in LoadingContent)
				{
					if (item.tag == "3d")
					{
						item.SetActive(true);
					}
				}
				break;
		}
	}
}
