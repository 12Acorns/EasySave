using NEG.Plugins.EasySave.Data;
using Newtonsoft.Json;
using System;
using UnityEngine;

namespace NEG.Plugins.EasySave.SerializableTypes.Vector;

[Serializable]
[JsonObject(MemberSerialization.Fields)]
public readonly struct SVector3 : ISaveable
{
	[JsonConstructor]
	public SVector3(
		[JsonProperty("X")] float _x,
		[JsonProperty("Y")] float _y,
		[JsonProperty("Z")] float _z)
	{
		X = _x;
		Y = _y;
		Z = _z;
	}
	public SVector3(Vector3 _vector)
	{
		X = _vector.x;
		Y = _vector.y;
		Z = _vector.z;
	}

	public readonly float X { get; }
	public readonly float Y { get; }
	public readonly float Z { get; }

	public Vector3 GetVector()
	{
		return new Vector3(X, Y, Z);
	}

	public static implicit operator SVector3(Vector3 _target)
	{
		return new SVector3(_target);
	}
	public static implicit operator SVector3(Vector2 _target)
	{
		return new SVector3(_target);
	}
}