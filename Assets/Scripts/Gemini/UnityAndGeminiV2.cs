using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class UnityAndGeminiKey
{
    public string key;
}

[System.Serializable]
public class Response
{
    public Candidate[] candidates;
}

[System.Serializable]
public class Candidate
{
    public Content content;
}

[System.Serializable]
public class Content
{
    public Part[] parts;
}

[System.Serializable]
public class Part
{
    public string text;
}

public class UnityAndGeminiV2: MonoBehaviour
{
    public TextAsset jsonApi;
    private string apiKey = ""; 
    private string apiEndpoint = "https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash-latest:generateContent"; // Edit it and choose your prefer model
    
    public string prompt = "What is your name?";
    [SerializeField]
    private TextMeshProUGUI output;

    void Start()
    {
        UnityAndGeminiKey jsonApiKey = JsonUtility.FromJson<UnityAndGeminiKey>(jsonApi.text);
        apiKey = jsonApiKey.key;
    }

    public void GetGeminiResponse(string promptText)
    {
        StartCoroutine(SendRequestToGemini(promptText));
    }

    public IEnumerator SendRequestToGemini(string promptText)
    {
        // Create JSON data
        string url = $"{apiEndpoint}?key={apiKey}";
        string jsonData = "{\"contents\": [{\"parts\": [{\"text\": \"{" + promptText + "}\"}]}]}";
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonData);

        // Create a UnityWebRequest with the JSON data
        using (UnityWebRequest www = new UnityWebRequest(url, "POST")) {
            www.uploadHandler = new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            
            yield return www.SendWebRequest();
            
            if (www.result != UnityWebRequest.Result.Success) {
                // Display an error message on the user interface.
                output.text = "Error: " + www.error;
                yield return "Error: " + www.error;
            } else {
                Response response = JsonUtility.FromJson<Response>(www.downloadHandler.text);
                if (response.candidates.Length > 0 && response.candidates[0].content.parts.Length > 0)
                {
                    string text = response.candidates[0].content.parts[0].text;
                    // Display Gemini's response on the user interface.
                    output.text = text;
                    yield return text;
                }
                else
                {
                    // Display an error message on the user interface.
                    output.text = "Error: Gemini API returned no results.";
                    yield return "No text found.";
                }
            }
        }
    }
}