/// <summary>
/// Player model.
/// Copyright (c) 2014 Eliot Lash
/// </summary>
using System;
using UnityEngine;
using com.eliotlash.core.service;

namespace com.eliotlash.core.model
{
	public class PlayerModel : IPersistsLocally
	{
		public PlayerModel()
		{
		}

		public void reset()
		{
		}

		#region IPersistsLocally
		public bool save(bool flush)
		{
			if(flush) {
				PlayerPrefs.Save();
			}
			return false;
		}

		public bool load()
		{
			bool success = true;
			return success;
		}
		#endregion
	}
}

