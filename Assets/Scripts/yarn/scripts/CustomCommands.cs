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

    public string[] dates;

    public DialogueRunner runner;

    // text object used for speaker name
    public TMP_Text textName;

    // text object used for the date
    public TMP_Text textDate;

    // text object used for the location
    public TMP_Text textLocation;

    

    public void Start(){

        runner.AddCommandHandler("setdate",setDate);
        runner.AddCommandHandler("setlocation",setLocation);
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
        this.textName.text = parameters[0].Equals("\\s") ? " ":parameters[0];
    }

    public void setLocation(string[] parameters){
        this.textLocation.text = parameters[0].Substring(0,1).ToUpper() + parameters[0].Substring(1);
    }

    public void setDate(string[] parameters){
        
        string num = parameters[0].Substring(4);
        Debug.Log("number is " + num);
        int index = -1; 
        if(!int.TryParse(num,out index)){
            index = 0; // unknown
        }
        if(index >= 0 && index < dates.Length)
            textDate.text = dates[index];
        else
            textDate.text = "unknown";
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


