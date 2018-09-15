using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    public bool IsShowDialogue = true;
    public Button NegativeBtn;
    public Button PositiveBtn;
    public TextMeshProUGUI BotText;
    public DataDialogues SystemDialogue;
    public int CurrentNode;
    public static DialogueSystem instance;

    private DialogueSystem()
    {
    }

    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        else if (instance == this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        NegativeBtn.onClick.AddListener(() => Respondent(1));
        PositiveBtn.onClick.AddListener(() => Respondent(0));
        PositiveBtn.GetComponentInChildren<Text>().text = SystemDialogue.Nodes[CurrentNode].PlayerAnswer[0].Text;
        NegativeBtn.GetComponentInChildren<Text>().text = SystemDialogue.Nodes[CurrentNode].PlayerAnswer[1].Text;
    }

    private void Respondent(int answer)
    {
        if (IsShowDialogue)
        {
            if (SystemDialogue.Nodes[CurrentNode].PlayerAnswer[answer].SpeakEnd)
            {
                IsShowDialogue = false;
            }

            CurrentNode = SystemDialogue.Nodes[CurrentNode].PlayerAnswer[answer].ToNode;
            AnimateText();
        }
    }

    private void AnimateText()
    {
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(BotText.DOFade(0, .25f));
        mySequence.AppendCallback(UpdateData);
        mySequence.AppendInterval(.5f);
        mySequence.Append(BotText.DOFade(1, .25f));
    }

    private void UpdateData()
    {
        BotText.text = SystemDialogue.Nodes[CurrentNode].NpcText;
    }

    private void Update()
    {
//        if (IsShowDialogue)
//        {
//            BotText.text = SystemDialogue.Nodes[CurrentNode].NpcText;



//			for (int i = 0; i < SystemDialogue.Nodes[CurrentNode].PlayerAnswer.Length; i++)
//			{
//				if (SystemDialogue.Nodes[CurrentNode].PlayerAnswer[i].SpeakEnd)
//					{
//						IsShowDialogue = false;
//					}
//					CurrentNode = SystemDialogue.Nodes[CurrentNode].PlayerAnswer[i].ToNode;
//			}

            //GUI.Box(new Rect(Screen.width / 2 - 300, Screen.height - 300, 600, 250), "");
            //GUI.Label(new Rect(Screen.width / 2 - 250, Screen.height - 280, 500, 90), Nodes[CurrentNode].NpcText);
//			for (int i = 0; i < Nodes[CurrentNode].PlayerAnswer.Length; i++)
//			{
//				if (GUI.Button(new Rect(Screen.width / 2 - 250, Screen.height - 200 + 25 * i, 500, 25),
//					Nodes[CurrentNode].PlayerAnswer[i].Text))
//				{
//					if (Nodes[CurrentNode].PlayerAnswer[i].SpeakEnd)
//					{
//						IsShowDialogue = false;
//					}
//					CurrentNode = Nodes[CurrentNode].PlayerAnswer[i].ToNode;
//				}
//			}
        }
//    }
}