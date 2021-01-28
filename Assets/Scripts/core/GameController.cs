#define USE_CONSOLE
using UnityEngine;
using com.eliotlash.core.model;
using com.eliotlash.core.service;

namespace com.eliotlash.core {
	public class GameController : MonoBehaviour {
		/*
		private PlayerModel playerModel = null;

#if USE_CONSOLE
		public const bool consoleEnabled = true;
#else
		public const bool consoleEnabled = false;
#endif

		//Happens before any Start() is called, use for early init
		void Awake() {
			Services.instance.Set<GameController>(this);

			//Init model
#if USE_CONSOLE
			Services.instance.Set<dev.console.ConsoleController>(new dev.console.ConsoleController());
#endif
			playerModel = new PlayerModel();
			Services.instance.Set<PlayerModel>(playerModel);
		}

		// Use this for initialization
		void Start () {
			playerModel.load();
		}
		
		// Update is called once per frame
//		void Update () {
//		}
*/
	}
}
