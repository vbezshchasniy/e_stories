using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ContentController : MonoBehaviour 
{
	public DataDialogues DataDialogues;
	public bool IsShowDialogue = true;
	public int CurrentNode;
	[SerializeField]
	private TextMeshProUGUI BotText;
	[SerializeField]
	private WTController WtController;
	[SerializeField]
	private DialogueSystem DialogueSystem;

	private void Start()
	{
		WtController = FindObjectOfType<WTController>();
		DialogueSystem = FindObjectOfType<DialogueSystem>();
		BotText = DialogueSystem.GetComponentInChildren<TextMeshProUGUI>();
		Init();
	}

	private void Init()
	{
		print("HII");
		UpdateData();
		int currentNode = CurrentNode;
		int length = DataDialogues.Nodes[currentNode].PlayerAnswer.Length;
		if (WtController != null)
		{
			for (int i = 0; i < length; i++)
			{
				if (WtController.WtButtons.Count == length)
				{
					int j = i;
					WtController.WtButtons[i].onClick.AddListener(() => Respondent(j));
					WtController.WtButtons[i].GetComponentInChildren<Text>().text = DataDialogues.Nodes[currentNode].PlayerAnswer[j].Text;
				}
			}
		}
	}
	
	private void Respondent(int answer)
	{
		if (IsShowDialogue)
		{
			if (DataDialogues.Nodes[CurrentNode].PlayerAnswer[answer].SpeakEnd)
			{
				IsShowDialogue = false;
			}

			CurrentNode = DataDialogues.Nodes[CurrentNode].PlayerAnswer[answer].ToNode;
			DialogueSystem.instance.ContentType = DataDialogues.Nodes[CurrentNode].TypeLoadingContent;
			//LoadBundle.StartCoroutine(LoadBundle.LoadingAssetBundle());
			UpdateData();
			AnimateText();
		}
	}
	
	
	public void AnimateText()
	{
		Sequence mySequence = DOTween.Sequence();
		mySequence.Append(DialogueSystem.instance.BotText.DOFade(0, .25f));
		//mySequence.AppendCallback(UpdateData);
		mySequence.AppendInterval(.5f);
		mySequence.Append(DialogueSystem.instance.BotText.DOFade(1, .25f));
	}

	private void UpdateData()
	{
		BotText.text = DataDialogues.Nodes[CurrentNode].NpcText;
	}
}