using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Xml;
using System.Xml.Serialization;
using System.Xml.Linq;

// An object that was bought.
public struct Item {
	private static string XML_TAG = "Item";

	public uint id;
	public string name;


	public Item(uint _id, string _name) {
		id = _id;
		name = _name;
	}

	public string GetXMLTag() {
		return XML_TAG;
	}

	public XElement GetXML() {
		return new XElement(XML_TAG, id);
	}
}

