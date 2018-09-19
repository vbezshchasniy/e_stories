using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Object For Preview Panel", menuName = "Preview Panel Item")]
public class PreviewPanelItem : ScriptableObject
{
	public Item[] Items;
}

[Serializable]
public class Item
{
	public string Name;
	public int Id;
	public Sprite Picture;
	public string Text;
	public Sprite Background;
}
