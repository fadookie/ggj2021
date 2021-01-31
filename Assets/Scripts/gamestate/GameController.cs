using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;
using com.eliotlash.core.service;

public class GameController : MonoBehaviour {
    public GameObject failPanel;
    public string NextScene;
    public bool IsWinning { get; set; }

    void Awake() {
		Services.instance.Set(this);
	}

    private void Start() {
		Services.instance.Get<MusicController>()
			.MusicEventStream
			.Where(evt => evt.type == MusicController.MusicEventType.End)
			.Subscribe(CheckWin)
			.AddTo(this);
    }

    public void OnShipDeathFinished() {
        failPanel.SetActive(true);
    }

    public void CheckWin(MusicController.MusicEvent _) {
	    if (IsWinning) SceneManager.LoadScene(NextScene, LoadSceneMode.Single);
	    // Otherwise assume the ship died and will call OnShipDeathFinished later
    }
}

