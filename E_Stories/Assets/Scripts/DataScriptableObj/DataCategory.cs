using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Object For Category", menuName = "Category Object")]
public class DataCategory : ScriptableObject
{
	public List<string> Names;
}
