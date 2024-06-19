using NEG.Plugins.EasySave.Data;
using Newtonsoft.Json;
using System;
using UnityEngine;

namespace NEG.Plugins.EasySave.SerializableTypes.Colour;

[Serializable]
public struct SColour : ISaveable
{
	[JsonConstructor]
	public SColour(
		float _r,
		float _g,
		float _b,
		float _a)
	{
		R = _r;
		G = _g;
		B = _b;
		A = _a;
	}
	public SColour(Color _colour)
	{
		R = _colour.r;
		G = _colour.g;
		B = _colour.b;
		A = _colour.a;
	}

	/// <summary>
	/// Red channel
	/// </summary>
	public float R { get; set; }
	/// <summary>
	/// Green channel
	/// </summary>
	public float G { get; set; }
	/// <summary>
	/// Blue channel
	/// </summary>
	public float B { get; set; }
	/// <summary>
	/// Alpha Channel
	/// </summary>
	public float A { get; set; }

	public Color GetColour()
	{
		return new Color(R, G, B, A);
	}

	public static implicit operator SColour(Color _colour)
	{
		return new SColour(_colour);
	}
}
