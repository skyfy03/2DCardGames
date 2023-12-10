using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{

	public DeckReference deck;

	public CardDisplay pullCardFromDeck(CardDisplay newCard)
	{
		newCard.cardInt = -1;
		newCard.cardSuitInt = -1;
		newCard = pullCard(newCard);
		return newCard;
	}

	private CardDisplay pullCard(CardDisplay newCard)
	{
		newCard = getRandomCard(newCard);
		if (newCard.cardInt == -1)
		{
			//Deck is empty
			return newCard;
		}
		else
		{
			insertCardInDeck(newCard);
			return newCard;
		}
	}

	private CardDisplay getRandomCard(CardDisplay newCard)
	{
		int cardInt = -1;
		int cardSuitInt = -1;

		cardInt = getRandomCardInt();
		if (cardInt == -1)
		{
			//No more cards exist;
			newCard.cardInt = -1;
			return newCard;
		}
		cardSuitInt = getRandomCardSuit(cardInt);

		newCard.cardInt = cardInt;
		newCard.cardSuitInt = cardSuitInt;

		return newCard;
	}

	private int getRandomCardInt()
	{
		int tempCardInt = -1;

		List<int> useableNumeralList = new List<int>();

		for (int i = 0; i < deck.DeckVariable.Deck.GetLength(0); i++)
		{
			for (int x = 0; x < deck.DeckVariable.Deck.GetLength(1); x++)
			{
				int useableCardDeck = deck.DeckVariable.Deck[i, x];
				if (useableCardDeck == 0)
				{
					useableNumeralList.Add(i);
				}
			}
		}

		//Debug.Log("Amount of Cards Left Before Pulling: " + useableNumeralList.Count);

		//Get Random Card Numeral From List
		if (useableNumeralList.Count > 1)
		{
			int r = Random.Range(1, useableNumeralList.Count);
			tempCardInt = useableNumeralList[r];
		}
		else if (useableNumeralList.Count == 1)
		{
			int tempC = useableNumeralList[useableNumeralList.Count - 1];
			tempCardInt = tempC;
		}
		else
		{
			//No more Cards to pull
			tempCardInt = -1;
			return tempCardInt;
		}

		return tempCardInt;
	}

	private int getRandomCardSuit(int cardNumber)
	{
		int tempCardSuit = -1;

		List<int> useableSuitList = new List<int>();
		for (int y = 0; y < deck.DeckVariable.Deck.GetLength(0); y++)
		{
			if (y == cardNumber)
			{
				for (int s = 0; s < deck.DeckVariable.Deck.GetLength(1); s++)
				{
					int useableCardDeck = deck.DeckVariable.Deck[y, s];
					if (useableCardDeck == 0)
					{
						useableSuitList.Add(s);
					}
				}
			}
		}

		//Delete later
		//Card newCard = ScriptableObject.CreateInstance<Card>();
		//Debug.Log("Actual Card Int " + cardNumber);
		//Debug.Log("Card Pulled " + newCard.getCardNumeral(cardNumber) + ". Amount of Suits left before pulling " + useableSuitList.Count);

		//string debugString = "";
		//for (int deB = 0; deB < deck.DeckVariable.Deck.GetLength(0); deB++)
		//{
		//	if (deB == cardNumber)
		//	{
		//		for (int s = 0; s < deck.DeckVariable.Deck.GetLength(1); s++)
		//		{
		//			int useableCardDeck = deck.DeckVariable.Deck[deB, s];
		//			if (useableCardDeck == 0)
		//			{
		//				debugString += " " + s + " ";
		//			}
		//		}
		//	}
		//}

		//Debug.Log("Useable Suit Ints: " + debugString);

		//Get Random Card Suit From List
		if (useableSuitList.Count > 1)
		{
			int suitR = Random.Range(1, useableSuitList.Count);
			tempCardSuit = useableSuitList[suitR];
		}
		else if (useableSuitList.Count == 1)
		{
			int tempS = useableSuitList[useableSuitList.Count - 1];
			tempCardSuit = tempS;
		}
		else
		{
			//This should never get reached.
			//Debug.Log("Deck Suit Pulled When No Card Numeral Available! This should NEVER HAPPEN");
		}

		return tempCardSuit;
	}

	private void insertCardInDeck(CardDisplay newCard)
	{
		deck.DeckVariable.Deck[newCard.cardInt, newCard.cardSuitInt] = 1;
	}

}
