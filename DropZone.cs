using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler {

	private GameManager gm;
	private int cost = 1;

	private void Start()
	{
		gm = GameObject.Find("GameManager").GetComponent<GameManager>();//look in the game for a game object with the name GameManager
	}

	public Draggable.Slot typeOfItem = Draggable.Slot.PLAYFIELD;
	public void OnPointerEnter(PointerEventData eventData)
	{
		Debug.Log("in");
		if (eventData.pointerDrag == null)
		{
			return;
		}
		Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
		if (d != null)
		{
			d.placeholderparent = this.transform;
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		Debug.Log("out");
		if (eventData.pointerDrag == null)
		{
			return;
		}
		Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
		if (d != null && d.placeholderparent == this.transform)
		{
			d.placeholderparent = d.ParentReturn;
		}
	}

	public void OnDrop(PointerEventData eventData)
	{
		Debug.Log(eventData.pointerDrag.name + "was dropped onto " + gameObject.name);

		Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
		if (d != null)
		{
			if (typeOfItem == d.typeOfItem)
			{
				d.ParentReturn = this.transform;
			}
			gm.money += cost;
		}
	}
}
