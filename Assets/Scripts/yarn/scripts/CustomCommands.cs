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

    public SpriteInfo[] sprites;
    public Image speaker1;
    public Image speaker2;

    public DialogueRunner runner;

    public TMP_Text name;

    public void Start(){

        runner.AddCommandHandler("setname",setName);
        runner.AddCommandHandler("speaker1",setSpeaker1);
        runner.AddCommandHandler("speaker2",setSpeaker2);

    }
    
    public void setSpeaker1(string[] parameters){
        setSpeakerImage(0,parameters[0]);
    }

    public void setSpeaker2(string[] parameters){
        setSpeakerImage(1,parameters[0]);
    }

    public void setName(string[] parameters){
        this.name.text = parameters[0].Equals("\\s") ? " ":parameters[0];
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


