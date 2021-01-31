using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



namespace Yarn.Unity{

public class CustomCommands : MonoBehaviour
{



    [System.Serializable]
    public struct SpriteInfo
    {
        public string name;
        public Sprite sprite;
    }

    // sprites used for speaker commands
    public SpriteInfo[] sprites;
    
    public Image speaker1;
    public Image speaker2;

   // public string[] dates;

    public DialogueRunner runner;

    // text object used for speaker name
    public TMP_Text textName;

    // text object used for the date
    public TMP_Text textDate;

    // text object used for the location
    public TMP_Text textLocation;

    public Image fadeOutPanel;



    // speaker 1 is dreamer
    // speaker 2 is captain
    public float speakerFadeTime = .5f;
    private float speakerOneElapsed = 100f;
    private bool speakerOneFadeIn = true; // if false then fades 
    private float speakerOneInitialAlpha = 0;
    private float speakerTwoElapsed = 100f;
    private bool speakerTwoFadeIn = true; // if false then fades out
    private float speakerTwoInitialAlpha = 0;


    //TODO : NAME STUFF BETTER lol
    public float fadeToBlackTime = 1f;
    private float fadeToBlackElapsed = 1000f;
    private bool fadeSceneIn = false;

    public string[] speakers = new string[2];

    public void Start(){

        //defaults
        speaker1.sprite = sprites[0].sprite;
        speaker2.sprite = sprites[1].sprite;

        runner.AddCommandHandler("start_scene",startCurrentScene);  // fade scene in from black
        runner.AddCommandHandler("end_scene",endCurrentScene);      //fade scene to black
        runner.AddCommandHandler("assign",assignSpeaker);           // assign a name to a speaker role ex: <<assign speaker1 Dreamer>>
        runner.AddCommandHandler("fadein",fadeIn);                  //fadein speaker - will enable if not enabled
        runner.AddCommandHandler("fadeout",fadeOut);                //fade out speaker - will disable atfter fadeout complete
        runner.AddCommandHandler("disable", disableSpeaker);        //disbale speaker
        runner.AddCommandHandler("enable", enableSpeaker);          //enable speaker
        runner.AddCommandHandler("load_scene",setScene);            // change the scene
        runner.AddCommandHandler("setdate",setDate);
        runner.AddCommandHandler("setlocation",setLocation);
        runner.AddCommandHandler("setname",setName);
        runner.AddCommandHandler("speaker1",setSpeaker1);
        runner.AddCommandHandler("speaker2",setSpeaker2);

    }

    public void startCurrentScene(string[] parameters,System.Action onComplete){
        fadeToBlackElapsed = 0;
        fadeSceneIn = true;
        fadeOutPanel.color = new Color(fadeOutPanel.color.r,fadeOutPanel.color.g,fadeOutPanel.color.b, 1);
        StartCoroutine(fadeScene(onComplete));
    }

    public void endCurrentScene(string[] parameters,System.Action onComplete){
          fadeToBlackElapsed = 0;
          fadeSceneIn = false;
          fadeOutPanel.color = new Color(fadeOutPanel.color.r,fadeOutPanel.color.g,fadeOutPanel.color.b, 1);
          StartCoroutine(fadeScene(onComplete));
    }

    private IEnumerator fadeScene(System.Action onComplete){
        yield return new WaitForSeconds(fadeToBlackTime);
        onComplete();
    }

    public void assignSpeaker(string[] parameters){
        string speaker = parameters[0];
        string assignment = parameters[1];
        speakers[speaker.Equals("speaker1") ? 0:1] = assignment;
    }


    public void Update(){
        speakerOneElapsed+=Time.deltaTime;
        speakerTwoElapsed+=Time.deltaTime;
        fadeToBlackElapsed+=Time.deltaTime;

        if(fadeToBlackElapsed <= fadeToBlackTime){
            float t = fadeToBlackElapsed / fadeToBlackTime;
            if(fadeSceneIn){
                fadeOutPanel.color = new Color(fadeOutPanel.color.r,fadeOutPanel.color.g,fadeOutPanel.color.b, speakerOneInitialAlpha * (1 - t)+0*t);
            }else{
                fadeOutPanel.color = new Color(fadeOutPanel.color.r,fadeOutPanel.color.g,fadeOutPanel.color.b, speakerOneInitialAlpha * (1 - t)+1*t);

            }
        }

        if(speakerOneElapsed <= speakerFadeTime){
            // speaker 1 fading
            float t = speakerOneElapsed / speakerFadeTime;
            if(speakerOneFadeIn){
                speaker1.gameObject.SetActive(true);
                speaker1.color = new Color(speaker1.color.r,speaker1.color.g,speaker1.color.b, speakerOneInitialAlpha * (1 - t)+1*t);
            }else{
                speaker1.color = new Color(speaker1.color.r,speaker1.color.g,speaker1.color.b, speakerOneInitialAlpha * (1 - t)+0*t);
            }
        }else if(!speakerOneFadeIn){
            speaker1.gameObject.SetActive(false);
        }

        if(speakerTwoElapsed <= speakerFadeTime){
             // speaker 2 fading
            float t = speakerTwoElapsed / speakerFadeTime;
            if(speakerTwoFadeIn){
                speaker2.gameObject.SetActive(true);
                speaker2.color = new Color(speaker2.color.r,speaker2.color.g,speaker2.color.b, speakerOneInitialAlpha * (1 - t)+1*t);
            }else{
                speaker2.color = new Color(speaker2.color.r,speaker2.color.g,speaker2.color.b, speakerOneInitialAlpha * (1 - t)+0*t);
            }
        }else if(!speakerTwoFadeIn){
            speaker2.gameObject.SetActive(false);
        }
    }

    public void enableSpeaker(string[] parameters){
        if(parameters[0].Equals("speaker1")){
            speaker1.gameObject.SetActive(true);
            speaker1.color = new Color(speaker1.color.r,speaker1.color.g,speaker1.color.b, 1);
        }else{
            speaker2.gameObject.SetActive(true);
            speaker2.color = new Color(speaker2.color.r,speaker2.color.g,speaker2.color.b, 1);
        }
    }

    public void disableSpeaker(string[] parameters){
        if(parameters[0].Equals("speaker1")){
            speaker1.gameObject.SetActive(false);
            speaker1.color = new Color(speaker1.color.r,speaker1.color.g,speaker1.color.b, 0);
        }else{
            speaker2.gameObject.SetActive(false);
            speaker2.color = new Color(speaker2.color.r,speaker2.color.g,speaker2.color.b, 0);
        }
    }

    public void fadeIn(string[] parameters){
        if(parameters[0].Equals("speaker1")){
            speakerOneFadeIn = true;
            speakerOneElapsed = 0;
            speakerOneInitialAlpha = 0;
            speaker1.color = new Color(speaker1.color.r,speaker1.color.g,speaker1.color.b, 0);
        }else{
            speakerTwoFadeIn = true;
            speakerTwoElapsed = 0;
            speakerTwoInitialAlpha = 0;
            speaker2.color = new Color(speaker2.color.r,speaker2.color.g,speaker2.color.b, 0);
        }
    }

    public void fadeOut(string[] parameters){
        if(parameters[0].Equals("speaker1")){
            speakerOneFadeIn = false;
            speakerOneElapsed = 0;
            speakerOneInitialAlpha = speaker1.color.a;
        }else{
            speakerTwoFadeIn = false;
            speakerTwoElapsed = 0;
            speakerTwoInitialAlpha = speaker2.color.a;
        }
    }

    /// focus the current speaker
    public void focusSpeaker(string speaker){
        float s1Alpha = speaker.Equals(speakers[0]) ? 1 : .5f;
        float s2Alpha = speaker.Equals(speakers[1]) ? 1 : .5f;
        speaker1.color = new Color(speaker1.color.r,speaker1.color.g,speaker1.color.b,s1Alpha);
        speaker2.color = new Color(speaker2.color.r,speaker2.color.g,speaker2.color.b,s2Alpha);
    }


    public void setScene(string[] parameters){
        UnityEngine.SceneManagement.SceneManager.LoadScene(parameters[0]);
    }
    
    public void setSpeaker1(string[] parameters){
        setSpeakerImage(0,parameters[0]);
    }

    public void setSpeaker2(string[] parameters){
        setSpeakerImage(1,parameters[0]);
    }

    public void setName(string[] parameters){
        this.textName.text = parameters[0].Equals("\\s") ? " ":parameters[0];
    }

    public void setLocation(string[] parameters){
        this.textLocation.text = parameters[0].Substring(0,1).ToUpper() + parameters[0].Substring(1);
        this.textLocation.text = this.textLocation.text.Replace('_',' ');
    }

    public void setDate(string[] parameters){
        
        string date = parameters[0].Replace('_',' ');
        textDate.text = date;

        // Debug.Log("number is " + num);
        // int index = -1; 
        // if(!int.TryParse(num,out index)){
        //     index = 0; // unknown
        // }
        // if(index >= 0 && index < dates.Length)
        //     textDate.text = dates[index];
        // else
        //     textDate.text = "unknown";
    }

    public void setSpeakerImage(int speaker,string spriteName){
         Sprite s = null;
        foreach(var info in sprites) {
            if (info.name == spriteName) {
                s = info.sprite;
                break;
            }
        }
        
        if(speaker1!= null && speaker == 0){
            if(s == null){
                speaker1.gameObject.SetActive(false);
            }else{
                speaker1.gameObject.SetActive(true);
                speaker1.sprite = s;
            }
        }else if(speaker2!=null){
            if(s == null){
                speaker2.gameObject.SetActive(false);
            }else{
                speaker2.gameObject.SetActive(true);
                speaker2.sprite = s;
            }
        }
    }
    
}
}


