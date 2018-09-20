using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Loader : MonoBehaviour
{
	public Color[] Colors;
	public float Speed;
	private Image Img;
	
	private void Start ()
	{
		Img = GetComponent<Image>();
		Speed = 1f;
	}
	
	private void Update ()
	{		
		Img.color = Color.Lerp(Colors[0], Colors[1], Mathf.PingPong(Time.time * Speed, 1));
		transform.Rotate(Vector3.forward * Speed);
	}
}