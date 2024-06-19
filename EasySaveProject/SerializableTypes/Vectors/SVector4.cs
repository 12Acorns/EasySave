using NEG.Plugins.EasySave.Data;
using Newtonsoft.Json;
using System;
using UnityEngine;

namespace NEG.Plugins.EasySave.SerializableTypes.Vector;

[Serializable]
public struct SVector4 : ISaveable
{
	[JsonConstructor]
	public SVector4(
		float _x,
		float _y,
		float _z,
		float _w)
	{
		X = _x;
		Y = _y;
		Z = _z;
		W = _w;
	}
	public SVector4(Quaternion _quaternion)
	{
		X = _quaternion.x;
		Y = _quaternion.y;
		Z = _quaternion.z;
		W = _quaternion.w;
	}
	public SVector4(Vector4 _vector)
	{
		X = _vector.x;
		Y = _vector.y;
		Z = _vector.z;
		W = _vector.w;
	}

	[JsonProperty]
	public float X { get; set; }
	[JsonProperty]
	public float Y { get; set; }
	[JsonProperty]
	public float Z { get; set; }
	[JsonProperty]
	public float W { get; set; }

	public Quaternion GetQuaternion()
	{
		return new Quaternion(X, Y, Z, W);
	}
	public Vector4 GetVector()
	{
		return new Vector4(X, Y, Z, W);
	}

	public static implicit operator SVector4(Quaternion _target)
	{
		return new SVector4(_target);
	}
	public static implicit operator SVector4(Vector4 _target)
	{
		return new SVector4(_target);
	}
	public static implicit operator SVector4(Vector3 _target)
	{
		return new SVector4(_target);
	}
	public static implicit operator SVector4(Vector2 _target)
	{
		return new SVector4(_target);
	}
}