/// <summary>
/// Interface for objects which persist on the local device.
/// Note that storage MAY only provide room for a single object, so multiple instances will overwrite each other!
/// In most cases (Player data, etc.) this is fine.
/// Copyright (c) 2014 Eliot Lash
/// </summary>

using System;
namespace com.eliotlash.core.model
{
	public interface IPersistsLocally
	{
		bool save(bool flush = true);
		bool load();
	}
}

