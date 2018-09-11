using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Object For All Elements", menuName = "Button Elements Object")]
public class DataButtonElements : ScriptableObject
{
	public List<string> Names;
	public List<Sprite> Pictures;
	public int Id;
}
