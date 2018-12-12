using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyThisCard : MonoBehaviour {

	private GameManager gm;

	// Use this for initialization
	void Start () {
		gm = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	public void buycard()
	{
		purchase();
	}

	private void purchase()
	{
		if (gm.money >= 3)
		{
			gm.money = gm.money - 3;
			Destroy(gameObject);
			print("you bought this card");
		}

		else
		{
			print("you donthave enough money");
		}
	}
}
