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
	public string NpcText;
	public Answer[] PlayerAnswer;
}

[Serializable]
public class Answer
{
	public string Text;
	public int ToNode;
	public bool SpeakEnd;
	public bool IsHaveContent;
	public string AssetBundleURL;
}
