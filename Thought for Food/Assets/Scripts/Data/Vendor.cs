using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Xml;
using System.Xml.Serialization;
using System.Xml.Linq;

// Whoever is selling the items we're buying.
public struct Vendor {
	private static string XML_TAG = "Vendor";

	// Unique identifier for an vendor.
	public uint id;

	// The user-readable name for this vendor. I.E. "FTB"
	public string name;

	public Vendor(uint _id, string _name) {
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
