using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDropHandler
{

	public bool isDraggleAfterDrop = false;
	public bool addCardTopStack = false;

	public bool checkCardValidDrop = true;
	public bool kingCorner = false;

	void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
	{
		//Debug.Log("OnPointerEnter");

		if (eventData.pointerDrag == null)
		{
			return;
		}

		Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
		if (d != null)
		{
			d.parentCardPlaceHolder = this.transform;
		}
	}

	void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
	{
		//Debug.Log("OnPointerExit");

		if (eventData.pointerDrag == null)
		{
			return;
		}

		Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
		if (d != null && d.parentCardPlaceHolder == this.transform)
		{
			d.parentCardPlaceHolder = this.transform;
		}
	}

	void IDropHandler.OnDrop(PointerEventData eventData)
	{
		//Debug.Log(eventData.pointerDrag.name + " was dropped On " + gameObject.name);

		Draggable d = eventData.pointerDrag.GetComponent<Draggable>();

		bool dropValid = false;

		int targetChildCount = gameObject.transform.childCount;
		CardDisplay droppedCardDisplay = d.GetComponent<CardDisplay>();
		if (targetChildCount > 1)
		{
			for (int i = 1; i < gameObject.transform.childCount; i++)
			{
				Transform droppedTargetTransform = gameObject.transform;
				if (i == 1)
				{
					Transform targetTransform = droppedTargetTransform.GetChild(i);
					CardDisplay targetCardDisplay = targetTransform.GetComponent<CardDisplay>();
					if (targetCardDisplay != null && droppedCardDisplay != null)
					{
						//Debug.Log("Card Target Numeral: " + targetCardDisplay.topCardNumeral.text + " Card Target Suit: " + targetCardDisplay.cardSuitInt +
						//	"Card Dropped Numeral: " + droppedCardDisplay.topCardNumeral.text + " Card Dropped Suit: " + droppedCardDisplay.cardSuitInt);

						//Check card drop validation
						if (cardDropValid(targetCardDisplay, droppedCardDisplay) || checkCardValidDrop == false)
						{
							//Debug.Log("Card Drop IS valid!!!!!!!!!!!!!");
							dropValid = true;
						}
						else
						{
							//Debug.Log("Card Drop is not valid");
						}
					}
				}
			}
		}
		else
		{
			if (kingCorner)
			{
				if (kingCornerValid(droppedCardDisplay))
				{
					dropValid = true;
				}
			}
			else
			{
				dropValid = true;
			}
		}

		if (d != null && dropValid)
		{
			d.returnParent = this.transform;
		}
	}

	private bool cardDropValid(CardDisplay targetCard, CardDisplay droppedCard)
	{
		bool cardValid = false;

		//Debug.Log("target card int: " + targetCard.cardInt + " target card suit: " + targetCard.cardSuitInt);
		//Debug.Log("dropped card int: " + droppedCard.cardInt + " dropped card suit: " + droppedCard.cardSuitInt);

		if (targetCard.cardInt == droppedCard.cardInt + 1)
		{
			if (targetCard.cardSuitInt > 1)
			{
				//Target Card is Red
				if (droppedCard.cardSuitInt <= 1)
				{
					cardValid = true;
				}
			}
			else
			{
				//Target Card is Black
				if (droppedCard.cardSuitInt >= 2)
				{
					cardValid = true;
				}
			}
		}

		return cardValid;
	}

	private bool kingCornerValid(CardDisplay droppedCard)
	{
		bool isCardDroppedKing = false;

		if (droppedCard.cardInt == 12)
		{
			isCardDroppedKing = true;
		}

		return isCardDroppedKing;
	}

}
