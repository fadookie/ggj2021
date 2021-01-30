/*
 * @author Valentin Simonov / http://va.lent.in/
 */

using System;
using com.eliotlash.core.service;
using UnityEngine;

namespace TouchScript.Behaviors.Cursors.UI
{
    /// <summary>
    /// A helper class to turn on and off <see cref="CanvasRenderer"/> without causing allocations.
    /// </summary>
    [HelpURL("http://touchscript.github.io/docs/html/T_TouchScript_Behaviors_Cursors_UI_TextureSwitch.htm")]
    public class TextureSwitch : MonoBehaviour
    {

        private CanvasRenderer r;
        public GradientTexture greatCursor;
        public GradientTexture mediocreCursor;
        public GradientTexture failCursor;

        /// <summary>
        /// Shows this instance.
        /// </summary>
        public void Show() {
            var accuracy = Services.instance.Get<TempoManager>().getAccuracy();
            Debug.LogFormat($"TextureSwitch show, accuracy:{accuracy}");
            switch (accuracy) {
                case TempoManager.Accuracy.Great:
                    greatCursor.Apply();
                    break;
                case TempoManager.Accuracy.Mediocre:
                    mediocreCursor.Apply();
                    break;
                case TempoManager.Accuracy.Fail:
                    failCursor.Apply();
                    break;
                default:
                    throw new InvalidOperationException($"Unknown enum case: {Enum.GetName(typeof(TempoManager.Accuracy), accuracy)}");
            }
            r.SetAlpha(1);
        }

        /// <summary>
        /// Hides this instance.
        /// </summary>
        public void Hide()
        {
            r.SetAlpha(0);
        }

        private void Awake()
        {
            r = GetComponent<CanvasRenderer>();
        }

    }
}