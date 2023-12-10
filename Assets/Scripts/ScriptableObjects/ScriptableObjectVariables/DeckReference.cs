using System;
using UnityEngine;

[Serializable]
public class DeckReference
{
	public bool UseConstant = true;
	public int[,] ConstantDeck;
	public DeckVariable DeckVariable;

	public int[,] Value
	{
		get
		{
			return UseConstant ? ConstantDeck:
								 DeckVariable.Deck;
		}
	}
}
