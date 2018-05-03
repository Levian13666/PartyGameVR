using System;
using UnityEngine;

[Serializable]
public class BombType {

	[HideInInspector] public string name = "Bomb";
	public int MinimumBombTime;
	public int MaximumBombTime;

}
