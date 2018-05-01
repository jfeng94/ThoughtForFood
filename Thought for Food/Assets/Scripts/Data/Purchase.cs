using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using System.Xml;
using System.Xml.Serialization;
using System.Xml.Linq;

// A relationship between a bought item, an buyer, and its price.
public class Purchase {
	public enum Type {
		None = 0,
		Groceries,
		Breakfast,
		Lunch,
		Dinner,
		Snacks,
		Luxury,
		Drinks,
	}

	private static string XML_TAG = "Purchase";

	public Item  item;
	public Buyer buyer;

	// How much the item basePrices before tax, tip
	public float basePrice;

	// How much tax this item is subject to.
	public float tax;

	// How much we're tipping.
	public float tip;

	// Price alterations go here.
	public List<AddOn> addOns;

	// Any other information that might be useful goes here.
	public List<string> notes;

	public Purchase() {
		if (addOns == null) {
			addOns = new List<AddOn>();
		}

		if (notes == null) {
			notes = new List<string>();
		}
	}

	public Purchase(Item _item, Buyer _buyer, float _basePrice, float _tax, float _tip,
	                List<AddOn> _addOns = null,
	                List<string> _notes = null) {

		item   = _item;
		buyer = _buyer;
		basePrice   = _basePrice;
		tax    = _tax;
		tip    = _tip;
		addOns = _addOns;
		notes  = _notes;

		if (addOns == null) {
			addOns = new List<AddOn>();
		}

		if (notes == null) {
			notes = new List<string>();
		}
	}

	public float GetTotal() {
		Debug.Log("GetTotal!!!! basePrice is " + basePrice);
		float subtotal = basePrice;

		for (int i = 0; i < addOns.Count; i++) {
			subtotal += addOns[i].price;
			Debug.Log("AddOn " + addOns[i].description + " -- $" + addOns[i].price);
		}

		Debug.Log("Subtotal is " + subtotal);
		Debug.Log("Total is " + (subtotal * (1 + tax + tip)));
		return subtotal * (1 + tax + tip);
	}

	public void AddOn(string reason, float price) {
		addOns.Add(new AddOn(reason, price));
	}

	public void AddNote(params string[] newNotes) {
		for (int i = 0; i < newNotes.Length; i++) {
			if ( ! notes.Contains(newNotes[i])) {
				notes.Add(newNotes[i]);
			}
			else Debug.LogError("AddNote -- note already in use!" + newNotes[i]);
		}
	}

	public string GetXMLTag() {
		return XML_TAG;
	}

	public XElement GetXML() {

		XElement xml = new XElement(XML_TAG,
		                               new XElement( "Item", item.id),
		                               new XElement("Buyer", buyer.id),
		                               new XElement( "Cost", basePrice),
		                               new XElement(  "Tax", tax),
		                               new XElement(  "Tip", tip)
		                           );

		for (int i = 0; i < addOns.Count; i++) {
			xml.Add(new XElement("AddOn",
			                        new XElement("Description", addOns[i].description),
			                        new XElement(      "Price", addOns[i].price)
			                    )
			       );
		}

		for (int i = 0; i < notes.Count; i++) {
			xml.Add(new XElement("Note", notes[i]));
		}

		return xml;
	}

}

