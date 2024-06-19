using NEG.Plugins.EasySave.Data;
using Newtonsoft.Json;
using System;
using UnityEngine;

namespace NEG.Plugins.EasySave.SerializableTypes.Colour;

[Serializable]
public readonly struct SColour : ISaveable
{
	[JsonConstructor]
	public SColour(
		[JsonProperty("R")] float _r,
		[JsonProperty("G")] float _g,
		[JsonProperty("B")] float _b,
		[JsonProperty("A")] float _a)
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
	public readonly float R { get; }
	/// <summary>
	/// Green channel
	/// </summary>
	public readonly float G { get; }
	/// <summary>
	/// Blue channel
	/// </summary>
	public readonly float B { get; }
	/// <summary>
	/// Alpha Channel
	/// </summary>
	public readonly float A { get; }

	public Color GetColour()
	{
		return new Color(R, G, B, A);
	}

	public static implicit operator SColour(Color _colour)
	{
		return new SColour(_colour);
	}
}
