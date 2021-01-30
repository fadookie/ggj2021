using com.eliotlash.core.service;
using TouchScript;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private void OnEnable()
    {
        if (TouchManager.Instance != null)
        {
            TouchManager.Instance.PointersPressed += pointersPressedHandler;
        }
    }

    private void OnDisable()
    {
        if (TouchManager.Instance != null)
        {
            TouchManager.Instance.PointersPressed -= pointersPressedHandler;
        }
    }
    private void pointersPressedHandler(object sender, PointerEventArgs e)
    {
        foreach (var pointer in e.Pointers) {
            Services.instance.Get<NavPointManager>().AddNavPoint(pointer.Position);
            var accuracy = Services.instance.Get<TempoManager>().getAccuracy();
            if (accuracy == TempoManager.Accuracy.Great) {
                Services.instance.Get<PlayerController>().Heal();
            }
        }
    }
}
