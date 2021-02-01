using System;
using com.eliotlash.core.service;
using UniRx;
using UnityEngine;

namespace music
{
    public class DialogueSfxProxy : MonoBehaviour
    {
        private SfxPlayer sfxPlayer;
        private readonly Subject<Unit> dialogueLineUpdate = new Subject<Unit>();

        private void Start() {
            sfxPlayer = Services.instance.Get<SfxPlayer>();
            dialogueLineUpdate
                .ThrottleFirst(new TimeSpan(TimeSpan.TicksPerSecond / 15))
                .Subscribe(_ => sfxPlayer.PlaySound(SfxPlayer.Sound.DialogueLineUpdate))
                .AddTo(this);
        }
        

        public void OnDialogueLineUpdate() {
            dialogueLineUpdate.OnNext(Unit.Default);
        }
    
        public void OnDialogueAdvance() {
            sfxPlayer.PlaySound(SfxPlayer.Sound.DialogueAdvance);
       }
    }
}