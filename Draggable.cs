using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

	public Transform ParentReturn = null;
	public Transform placeholderparent = null;

	public enum Slot {HAND, PLAYFIELD};
	public Slot typeOfItem = Slot.PLAYFIELD;

	GameObject placeholder = null;

	public void OnBeginDrag(PointerEventData eventData)
	{
		Debug.Log("On Begin Drag");

		placeholder = new GameObject();
		placeholder.transform.SetParent(this.transform.parent);
		LayoutElement le = placeholder.AddComponent<LayoutElement>();
		le.preferredWidth = this.GetComponent<LayoutElement>().preferredWidth;
		le.preferredHeight = this.GetComponent<LayoutElement>().preferredHeight;
		le.flexibleWidth = 0;
		le.flexibleHeight = 0;

		placeholder.transform.SetSiblingIndex(this.transform.GetSiblingIndex());

		ParentReturn = this.transform.parent;
		placeholderparent = ParentReturn;
		this.transform.SetParent(this.transform.parent.parent);

		GetComponent<CanvasGroup>().blocksRaycasts = false;
	}

	public void OnDrag(PointerEventData eventData)
	{
		//Debug.Log("On Drag");

		this.transform.position = eventData.position;

		if (placeholder.transform.parent != placeholderparent)
		{
			placeholder.transform.SetParent(placeholderparent);
		}

		int newSiblingIndex = placeholderparent.childCount;

		for (int i = 0; i < placeholderparent.childCount; i++)
		{
			if (this.transform.position.x < placeholderparent.GetChild(i).position.x)
			{
				newSiblingIndex = i;
				if (placeholder.transform.GetSiblingIndex() < newSiblingIndex)
				{
					newSiblingIndex--;
				}
				break;
			}
		}

		placeholder.transform.SetSiblingIndex(newSiblingIndex);

	}

	public void OnEndDrag(PointerEventData eventData)
	{
		Debug.Log("On end Drag");
		this.transform.SetParent(ParentReturn);
		this.transform.SetSiblingIndex(placeholder.transform.GetSiblingIndex());
		GetComponent<CanvasGroup>().blocksRaycasts = true;

		Destroy(placeholder);
	}
}
