using UnityEngine;
using UniSpeech;

public class KidoManager : MonoBehaviour, ISpeechRecognizer
{    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (SpeechRecognizer.StartRecord())
            {
                Debug.Log("StartRecord");
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (SpeechRecognizer.StopRecord())
            {
                Debug.Log("StopRecord");
            }
        }
    }

    public void OnRecognized(string transcription)
    {
        Debug.Log($"Recognized: {transcription}");
    }

    public void OnError(string description)
    {
        Debug.LogWarning($"Error: {description}");
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
