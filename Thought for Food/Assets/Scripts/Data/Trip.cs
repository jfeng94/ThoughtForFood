using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Linq;


// A shopping trip.
public class Trip {
	private uint     id;
	private DateTime date;
	private Vendor   vendor;
	private List<Purchase> purchases = new List<Purchase>();

	public void SetID(uint _id) {
		id = _id;
	}

	public void SetDate(int year, int month, int day) {
		date = new DateTime(year, month, day);
	}

	public void SetVendor(Vendor _vendor) {
		vendor = _vendor;
	}

	public void AddPurchases(params Purchase[] newPurchases) {
		for (int i = 0; i < newPurchases.Length; i++) {
			if ( ! purchases.Contains(newPurchases[i])) {
				purchases.Add(newPurchases[i]);
			}
			else Debug.LogError("Purchase already in trip? " + newPurchases[i]);
		}
	}

	public void Write() {
		XDocument doc = new XDocument();

		XElement root = new XElement("Trip");

		root.Add(
		    new XElement("ID", id),
		    new XElement("Date", new XElement( "Year", date.Year),
		    		             new XElement("Month", date.Month),
		    		             new XElement(  "Day", date.Day)
		                ),
		    new XElement("Vendor", vendor.id)
		);

		for (int i = 0; i < purchases.Count; i++) {
			root.Add(purchases[i].GetXML());
		}

		doc.Add(root);

		string declaration = @"<?xml version='1.0' encoding='utf-8' standalone='yes'?>" + "\n\n";

		Debug.Log(declaration + doc.ToString());
	}

}


/*
<?xml version='1.0' encoding='utf-8' standalone='yes'?>

<Trip>
  <ID>0</ID>

  <Date>
    <Year>2018</Year>
    <Month>4</Month>
    <Day>20</Day>
  </Date>
  <Vendor>69</Vendor>

  <Purchase>
    <Item>1</Item>
    <Entity>5</Entity>
    <Cost>3.95</Cost>
    <Tax>0.095</Tax>
    <Tip>0</Tip>
    <AddOn>
      <Reason>Boba</Reason>
      <Price>0.5</Price>
    </AddOn>
    <Note>Quarter Sugar</Note>
    <Note>No ice</Note>
  </Purchase>

  <Purchase>
    <Item>4</Item>
    <Entity>5</Entity>
    <Cost>3.95</Cost>
    <Tax>0.095</Tax>
    <Tip>0</Tip>
    <AddOn>
      <Reason>Boba</Reason>
      <Price>0.5</Price>
    </AddOn>
    <Note>No ice</Note>
  </Purchase>

  <Purchase>
    <Item>1</Item>
    <Entity>12</Entity>
    <Cost>3.95</Cost>
    <Tax>0.095</Tax>
    <Tip>0</Tip>
    <Note>Half Sugar</Note>
    <Note>No ice</Note>
  </Purchase>
  
  <Purchase>
    <Item>7</Item>
    <Entity>17</Entity>
    <Cost>3.95</Cost>
    <Tax>0.095</Tax>
    <Tip>0</Tip>
    <AddOn>
      <Reason>Lychee Jelly</Reason>
      <Price>0.5</Price>
    </AddOn>
    <Note>Half Sugar</Note>
    <Note>No ice</Note>
  </Purchase>
</Trip>

*/
