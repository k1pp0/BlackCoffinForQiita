using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniSpeech;
using UnityEngine.UI;

public class KidoManager : MonoBehaviour, ISpeechRecognizer
{
    [SerializeField] private BlackCoffin blackCoffin;
    [SerializeField] private Text transcriptionText;
    [SerializeField] private Transform focusedSquare;
    private bool inAria;
    
    private const string Spell90 = "滲み出す混濁の紋章\n不遜なる狂気の器\n湧き上がり・否定し・痺れ・瞬き\n眠りを妨げる\n爬行する鉄の王女\n絶えず自壊する泥の人形\n結合せよ 反発せよ\n地に満ち 己の無力を知れ";
    private readonly char[] twoWordList =
    {
        '滲', '出', '混', '濁', '紋', '章', '不', '遜', '狂', '気', 
        '器', '湧', '上', '否', '定', '痺', '瞬', '眠', '妨', '爬',
        '行', '鉄', '王', '女', '絶', '自', '壊', '泥', '人', '形', 
        '結', '合', '反', '発', '地', '満', '己', '無', '力', '知'
    };
    private readonly Dictionary<string, string> replaceWordList = new Dictionary<string, string>()
    {
        {"にじみ", "滲み"}
    };
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (SpeechRecognizer.StartRecord())
            {
                Debug.Log("StartRecord");
                inAria = true;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (SpeechRecognizer.StopRecord())
            {
                Debug.Log("StopRecord");
                inAria = false;
                transcriptionText.text = Spell90;
            }
        }
    }

    public void OnRecognized(string transcription)
    {
        Debug.Log($"Recognized: {transcription}");
        foreach (var pair in replaceWordList)
        {
            transcription = transcription.Replace(pair.Key, pair.Value);
        }

        var score = twoWordList.Count(transcription.Contains);
        if (transcription.Contains("黒棺"))
        {
            CreateBlackCoffin(score);
        }
        
        var tmp = "";
        foreach (var word in Spell90)
        {
            if (twoWordList.Contains(word) && transcription.Contains(word))
            {
                tmp += $"<color=red>{word}</color>";
            }
            else
            {
                tmp += word;
            }
        }
        transcriptionText.text = tmp;
    }
    
    private void CreateBlackCoffin(int score)
    {
        Debug.Log("CreateBlackCoffin");
        if (!inAria) return;
        inAria = false;
        var bc = Instantiate(blackCoffin, focusedSquare.position, focusedSquare.rotation);
        bc.transform.localScale *= score / 10f + 1;
    }

    public void OnError(string description)
    {
        Debug.LogWarning($"Error: {description}");
        transcriptionText.text = Spell90;
    }

    public void OnAuthorized()
    {
        Debug.Log("Authorized");
    }

    public void OnUnauthorized()
    {
        Debug.LogWarning("Unauthorized");
    }

    public void OnAvailable()
    {
        Debug.Log("Available");
    }

    public void OnUnavailable()
    {
        Debug.LogWarning("Unavailable");
    }
}
