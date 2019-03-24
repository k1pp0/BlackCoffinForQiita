using UnityEngine;
using UniSpeech;
using UnityEngine.UI;

public class KidoManager : MonoBehaviour, ISpeechRecognizer
{
    [SerializeField] private BlackCoffin blackCoffin;
    [SerializeField] private Text transcriptionText;
    [SerializeField] private Transform focusedSquare;
    private bool inAria;
    
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
                transcriptionText.text = "";
            }
        }
    }

    public void OnRecognized(string transcription)
    {
        Debug.Log($"Recognized: {transcription}");
        transcriptionText.text = transcription;
        if (transcription.Contains("黒棺"))
        {
            CreateBlackCoffin();
        }
    }
    
    private void CreateBlackCoffin()
    {
        Debug.Log("CreateBlackCoffin");
        if (!inAria) return;
        inAria = false;
        Instantiate(blackCoffin, focusedSquare.position, focusedSquare.rotation);
    }

    public void OnError(string description)
    {
        Debug.LogWarning($"Error: {description}");
        transcriptionText.text = "";
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
