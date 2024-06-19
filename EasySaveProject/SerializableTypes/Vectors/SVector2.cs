using NEG.Plugins.EasySave.Data;
using Newtonsoft.Json;
using System;
using UnityEngine;

namespace NEG.Plugins.EasySave.SerializableTypes.Vector;

[Serializable]
public readonly struct SVector2 : ISaveable
{
	[JsonConstructor]
	public SVector2(
		[JsonProperty("X")] float _x,
		[JsonProperty("Y")] float _y)
	{
		X = _x;
		Y = _y;
	}
	public SVector2(Vector2 _vector)
	{
		X = _vector.x;
		Y = _vector.y;
	}

	public readonly float X { get; }
	public readonly float Y { get; }

	public Vector2 GetVector()
	{
		return new Vector2(X, Y);
	}

	public static implicit operator SVector2(Vector2 _target)
	{
		return new SVector2(_target);
	}
}