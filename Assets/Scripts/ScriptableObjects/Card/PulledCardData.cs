//using UnityEngine;

//public class PulledCardData : MonoBehaviour
//{
//	public IntReference CardNumeralInt;
//	public IntReference CardSuitInt;

//	public CardDisplay cardPrefab;

//	public void pullCard()
//	{

//		if (CardNumeralInt.Variable.Value == -1)
//		{
//			Debug.Log("Deck is empty");
//		}
//		else
//		{
//			Card newCard = ScriptableObject.CreateInstance<Card>();
//			newCard.CreateInstance(CardNumeralInt.Variable.Value, CardSuitInt.Variable.Value);
//			cardPrefab.card = newCard;
//			cardPrefab = Instantiate(cardPrefab, this.transform);
//		}

//	}

//}
