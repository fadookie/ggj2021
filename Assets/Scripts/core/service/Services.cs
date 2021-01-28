/// <summary>
/// Wallet model.
/// Copyright (c) 2014 Eliot Lash
/// </summary>
using System;
using System.Collections.Generic;

namespace com.eliotlash.core.service {
	public class Services {
		//Statics
		private static Services _instance;

		//Instance
		private Dictionary<Type, object> services = new Dictionary<Type, object>();

		public Services() 
		{
			if (_instance != null)
			{
				UnityEngine.Debug.LogError("Cannot have two instances of singleton.");
				return;
			}
			
			_instance = this;
		}
		
		public static Services instance {
			get {
				if (_instance == null) {
					new Services();
				}
				
				return _instance;
			}
		}

		public void Set<T>(T service) where T : class {
			services.Add(typeof(T), service);
		}

		public T Get<T>() where T : class {
			T ret = null;
			try {
			 ret = services[typeof(T)] as T;
			} catch (KeyNotFoundException) {
			}
			return ret;
		}
	}
}
