using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New Object For Dialogue", menuName = "Dialogue Object")]
public class DataDialogues : ScriptableObject 
{
	public DialogueNode[] Nodes;
}

[Serializable]
public class DialogueNode
{
	public string NodeId;
	public string NpcText;
	public ContentType TypeLoadingContent;
	public string AssetBundeURL;
	public string NameAssetBundleObject;
	public Answer[] PlayerAnswer;	
}

[Serializable]
public class Answer
{
	public string Text;
	public int ToNode;
	public bool SpeakEnd;
}


