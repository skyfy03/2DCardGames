using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CardDisplay : MonoBehaviour
{

    //public Card card;

    public Image cardImageColor;
    public Image topCardSuit;
    public Image bottomCardSuit;

    public Text topCardNumeral;
    public Text bottomCardNumeral;

    public int cardInt;
    public int cardSuitInt;
    
    public string cardNumeral;
    public enumCardSuit cardSuit;
	public Color cardColor;

    public enum enumCardSuit
	{
		Club,
		Spade,
		Heart,
		Diamond
	}

	// Start is called before the first frame update
	void Start()
    {
		cardImageColor.color = cardColor;
		topCardSuit.color = cardImageColor.color;
		bottomCardSuit.color = cardImageColor.color;

		if (cardSuit == enumCardSuit.Diamond || cardSuit == enumCardSuit.Spade)
		{
			RectTransform tCS = topCardSuit.GetComponent<RectTransform>();
			tCS.rotation = new Quaternion(tCS.rotation.x, tCS.rotation.y, -.3f, tCS.rotation.w);
		}
		else
		{
			RectTransform tCS = topCardSuit.GetComponent<RectTransform>();
			tCS.rotation = new Quaternion(tCS.rotation.x, tCS.rotation.y, 0f, tCS.rotation.w);
		}

		topCardNumeral.text = cardNumeral;
		bottomCardNumeral.text = cardNumeral;
	}

	public void setCardDisplay()
	{
		if (cardInt != -1)
		{
            cardNumeral = getCardNumeral(cardInt);

			if (cardSuitInt != -1)
			{
				cardSuit = (enumCardSuit)cardSuitInt;
			}

			Color red = new Color(255, 0, 0);
			Color black = new Color(0, 0, 0);

			if (cardSuit == enumCardSuit.Diamond || cardSuit == enumCardSuit.Heart)
			{
				cardColor = red;
			}
			else
			{
				cardColor = black;
			}
        }
    }

	public string getCardNumeral(int cardNumber)
	{
		//Deck is double array. 0 == Ace
		int tempInt = cardNumber;
		string cardString = "";

		if (tempInt == 0)
		{
			cardString = "Ace";
			return cardString;
		}

		//Add one to make card numeral correct
		tempInt = tempInt + 1;

		if (tempInt == 11)
		{
			cardString = "Jack";
		}
		else if (tempInt == 12)
		{
			cardString = "Queen";

		}
		else if (tempInt == 13)
		{
			cardString = "King";
		}
		else
		{
			cardString = tempInt.ToString();
		}

		return cardString;
	}

}
