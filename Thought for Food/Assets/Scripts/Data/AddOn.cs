using UnityEngine;

public struct AddOn {
	public string description;
	public float price;

	public AddOn(string _description, float _price) {
		description = _description;
		price = _price;
	}
}