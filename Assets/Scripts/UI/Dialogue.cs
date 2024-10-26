using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Samples.SpatialKeyboard;

public class Dialogue : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI dialogueText;  // The text object that will output the dialogue
    [SerializeField, Range(0.0f, 0.75f)]
    private float timePerCharacter = 0.05f;  // The delay in time between each character
    [SerializeField]
    private UnityAndGeminiV2 geminiModelV2;  // Ensures connection to a Gemini model
    
    private const string InitialPrompt = "Please briefly describe what your app does?\nRedove is an app that utilizes XR technology to immerse long-term or isolated patients into a world that provides a sense of freedom or familiarity. XR technology includes virtual, augmented, and mixed reality. Isolated and long-term patients often lack social interaction, and this can mentally hurt them. It has an avatar customization feature, an AI-powered counseling tool, and two simulation worlds. 3D avatar customization includes a variety of options to account for all users. The option categories include hair style, outfit color, hair color, and skin color. The AI-powered counseling tool uses Gemini, a large language model developed by Google, to generate responses to what the user speaks into the microphone (speech-to-text) and reads them out loud (text-to-speech). Aside from the main menu/office, there are two worlds: the natural world, which includes mountains, a life-sized chess board, parks, forests, and buildings; and the game complex, which includes a bowling alley, a skating rink, seating, and basketball and tennis courts. The user will move around and interact with the various objects in the scene. As such interactions can not be found in a hospital setting, patients will be entertained, socially engaged, and curious as they explore new worlds. This fulfills our goals of meeting medical and mental health needs by facilitating communication, enabling patients to share their feelings, and fostering a better sense of self.\n\nWhat inspired you to create this app?\nOur journey to create Redove began with the formulation of abstract app ideas that were broadly based to align with content related to improving social-emotional health issues. This process helped us gain clarity on our vision. To refine our concepts, we reached out to Dr. David Eddie, a researcher at the Recovery Research Institute at Massachusetts General Hospital, an affiliate of Harvard Medical School. Dr. Eddie’s expertise in the patient experience of recovery made him an invaluable resource. During our meeting, we explored the current applications of extended reality (XR) technology in medicine, learning that while several XR tools exist—such as InnerWorld, which aids emotional recovery—very few are designed specifically for in-hospital use. Dr. Eddie advised us with different ways we could proceed, but through our research, one demographic stuck out to us- isolated or prolonged-visit patients, who had little, if any, social interaction over the period of their hospital stay. Because of this, these individuals endured significant emotional and psychological challenges. Their plight resonated with us and triggered us to develop a solution that brings freedom once again, just as a dove is released. This led to the creation of our app, Redove.\n\nWhat technical difficulties did you face programming your app?\nAfter the world was modeled and uploaded into Unity for testing, an unbearable amount of lag hindered us from proceeding. Around two weeks had passed with no solution, but we finally determined the cause: the world model had too many polygons, and this, combined with detailed texturing, overloaded the Meta Quest 3’s processing power. To fix this, the parts of the model with the most polygons were simplified. That, however, was not the only issue when importing the model. Unity-Blender scale changes occur when working in VR, and importing the model led to large and hard-to-work-with scales. Additionally, the model’s textures did not convert correctly, as Blender renders common artifacts or discrepancies, while Unity does not. The root cause of the former issue was the file format; instead of using the typical .FBX file, as is done in normal Unity projects, we had to use .blend files (Blender files) directly. This would also automatically create prefabs, or reusable objects, that enabled us to modify only the prefab, which automatically updated all instances/usages of that prefab. The latter issue was more difficult to fix as it involved two different softwares with two different methods of shading and texturing. Because of this, we ended up having to redo a lot of the shaders and textures in Unity.\n\nWhat improvements would you make if you were to create a 2.0 version of your app?\nIn the future, Redove will introduce multi-user functionality that enables the family and friends of isolated patients to interact with them with headsets as they are outside isolation rooms. This crucial feature will assist in combating feelings brought by isolation by giving a platform for real-time connections. This allows loved ones to provide emotional support and companionship. In addition to this, the graphics of the virtual environment will see significant improvements to offer more immersive experiences that account for more possible patient interests. The goal would be to create virtual settings that provide either the same comfort and familiarity as home or the same freedom as nature. This helps to create a sense of presence despite physical distance. As with all virtual experiences, accessibility is a primary goal, and one definite area of improvement is how the app is presented to immobile or bedridden patients. The app heavily emphasizes features for when the patient is standing up, but not as much for when they are sitting down. Finally, improvements to the AI feature could be created with tighter machine learning integration. For example, if the AI could remember the conversation farther back, it would be able to give better and more holistic responses, which can personalize these experiences further.\n\nGiven this information, please answer the following prompt with the knowledge that you are the Redove A.I., and your job will be to assist people with using Redove and providing great comfort to them when needed.\n\n";

    // Start is called before the first frame update
    void Start()
    {
        dialogueText.text = string.Empty;
    }
    
    /// <summary>
    /// Initiates typewriter dialogue
    /// </summary>
    /// <param name="text">The text to apply the typewriter effect.</param>
    public void StartDialogue(string text)
    {
        StartCoroutine(Typewriter(text));
    }

    /// <summary>
    /// Initiates typewriter dialogue based on a VR keyboard object.
    /// </summary>
    /// <param name="keyboard">The VR keyboard object used to extract text.</param>
    public void StartDialogueFromKeyboard(XRKeyboardDisplay keyboard)
    {
        string prompt = InitialPrompt + keyboard.keyboard.text;
        dialogueText.text = "Loading response from Gemini API...";
        geminiModelV2.GetGeminiResponse(prompt);
    }
    
    /// <summary>
    /// The coroutine responsible for initiating typewriter dialogue
    /// </summary>
    /// <param name="text">The typewriter text</param>
    /// <returns>Returns `null` to signify the end of the typewriter sequence</returns>
    IEnumerator Typewriter(string text)
    {
        dialogueText.text = string.Empty;
        foreach (char character in text)
        {
            dialogueText.text += character;
            yield return new WaitForSeconds(timePerCharacter);
        }
        yield return null;
    }
}




