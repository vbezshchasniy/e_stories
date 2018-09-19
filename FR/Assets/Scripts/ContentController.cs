using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEditor;
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
	
	[SerializeField]
	private WTController WtController;
	[SerializeField]
	private DialogueSystem DialogueSystem;

	public static ContentController instance;

	private void Awake()
	{
		instance = this;
	}

	private void Start()
	{
		StartCoroutine(LoadBundle());
		UpdateData();
	}
	
	public IEnumerator LoadBundle ()
	{
		WWW www = new WWW(URL);
		while (!www.isDone)
		{
			yield return null;
		}
			
		AssetBundle myasset = www.assetBundle;
		GameObject bundle = myasset.LoadAsset<GameObject>("AssetBundle");
		yield return new WaitForSeconds(2f);
		CurrentContent = Instantiate(bundle, ContentPanel.transform.position, transform.rotation);
		CurrentContent.transform.SetParent(ContentPanel.transform, false);
		WtController = CurrentContent.GetComponent<WTController>();
		DialogueSystem = CurrentContent.GetComponentInChildren<DialogueSystem>();
		
	}
	
	public void Respondent(int answer)
	{
		if (IsShowDialogue)
		{
			if (DataDialogues.Nodes[CurrentNode].PlayerAnswer[answer].SpeakEnd)
			{
				IsShowDialogue = false;
			}

			CurrentNode = DataDialogues.Nodes[CurrentNode].PlayerAnswer[answer].ToNode;
			DialogueSystem.instance.ContentType = DataDialogues.Nodes[CurrentNode].TypeLoadingContent;
			CurrentContent.GetComponent<WTController>().BotText.text = DataDialogues.Nodes[CurrentNode].NpcText;
			WtController.UpdateContent();
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
		//CurrentContent.GetComponent<WTController>().BotText.text = DataDialogues.Nodes[CurrentNode].NpcText;
	}
}