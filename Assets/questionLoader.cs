using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class questionLoader : MonoBehaviour {
    public Text questionText;
    public Text anwer1Text;
    public Text anwer2Text;
    public Text anwer3Text;
    public Text anwer4Text;
    private int correctValue = -1;
    // Use this for initialization
    void Start () {
        string stringAsset = (Resources.Load("questions") as TextAsset).ToString();
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(new StringReader(stringAsset));
        string xmlPathPattern = "//Questions/Question";
        XmlNodeList myNodeList = xmlDoc.SelectNodes(xmlPathPattern);
        string qText = "";
        string answer1Text = "";
        string answer2Text = "";
        string answer3Text = "";
        string answer4Text = "";
        foreach (XmlNode node in myNodeList)
        {
            string corr = node.Attributes.GetNamedItem("correct").Value;
            correctValue = int.Parse(corr);
            XmlNode question = node.FirstChild;
            qText = question.InnerText;
            Debug.Log(qText);
            XmlNode o1 = question.NextSibling;
            answer1Text = o1.InnerText;
            XmlNode o2 = o1.NextSibling;
            answer2Text = o2.InnerText;
            XmlNode o3 = o2.NextSibling;
            answer3Text = o3.InnerText;
            XmlNode o4 = o3.NextSibling;
            answer4Text = o4.InnerText;
        }
        questionText.text = qText;
        anwer1Text.text = answer1Text;
        anwer2Text.text = answer2Text;
        anwer3Text.text = answer3Text;
        anwer4Text.text = answer4Text;

    }
	
	// Update is called once per frame
	void Update () {
        
        if(Input.GetKeyUp("1"))
        {
            Debug.Log(answerCorrect(1));
        }
        else if (Input.GetKeyUp("2"))
        {
            Debug.Log(answerCorrect(2));
        }
        else if (Input.GetKeyUp("3"))
        {
            Debug.Log(answerCorrect(3));
        }
        else if (Input.GetKeyUp("4"))
        {
            Debug.Log(answerCorrect(4));
        }
    }

    private bool answerCorrect(int answer)
    {
        //TODO Doe wat er ook moet gebeuren wanneer het antwoord goed is.
        return answer == correctValue;
    }
}
