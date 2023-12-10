using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Draggable : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{

	float offSetX, offSetY;

	public Transform returnParent = null;
	public Transform parentCardPlaceHolder = null;

	GameObject cardPlaceHolder = null;

	public bool isCardDraggable = true;

	void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
	{
		if (isCardDraggable)
		{
			cardPlaceHolder = new GameObject("Card Place Holder");
			cardPlaceHolder.transform.SetParent(this.transform.parent);
			LayoutElement le = cardPlaceHolder.AddComponent<LayoutElement>();
			le.preferredWidth = this.GetComponent<LayoutElement>().preferredWidth;
			le.preferredHeight = this.GetComponent<LayoutElement>().preferredHeight;
			le.flexibleWidth = 0;
			le.flexibleHeight = 0;

			cardPlaceHolder.transform.SetSiblingIndex(this.transform.GetSiblingIndex());

			offSetX = this.transform.position.x - eventData.position.x;
			offSetY = this.transform.position.y - eventData.position.y;

			returnParent = this.transform.parent;
			parentCardPlaceHolder = returnParent;
			this.transform.SetParent(this.transform.parent.parent);

			GetComponent<CanvasGroup>().blocksRaycasts = false;
		}
	}

	void IDragHandler.OnDrag(PointerEventData eventData)
	{
		if (isCardDraggable)
		{
			Vector2 mouseCurrentPostion = eventData.position;

			mouseCurrentPostion.x = mouseCurrentPostion.x + offSetX;
			mouseCurrentPostion.y = mouseCurrentPostion.y + offSetY;

			this.transform.position = mouseCurrentPostion;

			if (cardPlaceHolder.transform.parent != parentCardPlaceHolder)
			{
				cardPlaceHolder.transform.SetParent(parentCardPlaceHolder);
			}

			bool addCardTopStack = false;
			if (parentCardPlaceHolder != null)
			{
				DropZone dropZone = parentCardPlaceHolder.GetComponent<DropZone>();
				addCardTopStack = dropZone.addCardTopStack;
			}

			if (addCardTopStack)
			{
				cardPlaceHolder.transform.SetSiblingIndex(0);
			}
			else
			{
				int newSiblingIndex = parentCardPlaceHolder.childCount;

				for (int i = 0; i < parentCardPlaceHolder.childCount; i++)
				{
					if (this.transform.position.x < parentCardPlaceHolder.GetChild(i).position.x)
					{

						newSiblingIndex = i;

						if (cardPlaceHolder.transform.GetSiblingIndex() < newSiblingIndex)
						{
							newSiblingIndex--;
						}

						break;
					}
				}

				cardPlaceHolder.transform.SetSiblingIndex(newSiblingIndex);

			}
		}
	}


	void IEndDragHandler.OnEndDrag(PointerEventData eventData)
	{

		if (isCardDraggable)
		{
			offSetX = 0f;
			offSetY = 0f;

			this.transform.SetParent(returnParent);
			this.transform.SetSiblingIndex(cardPlaceHolder.transform.GetSiblingIndex());
			GetComponent<CanvasGroup>().blocksRaycasts = true;

			DropZone dropZone = returnParent.GetComponent<DropZone>();
			this.isCardDraggable = dropZone.isDraggleAfterDrop;
			//this.addCardTopStack = false;

			Destroy(cardPlaceHolder);
		}
	}

}
