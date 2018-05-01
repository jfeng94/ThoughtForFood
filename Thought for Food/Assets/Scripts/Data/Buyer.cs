using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Xml;
using System.Xml.Serialization;
using System.Xml.Linq;

// A buyer. Someone participating in a shopping trip.
public struct Buyer {
	private static string XML_TAG = "Buyer";

	// Unique identifier for an entity.
	public uint id;

	// List of entities that are considered part of this entity.
	public List<Buyer> subEntities;

	// The user-readable name for this entity. I.E. "Jerry"
	public string name;

	public Buyer(uint _id, string _name, List<Buyer> _subEntities = null) {
		id = _id;
		name = _name;
		subEntities = _subEntities;
	}

	public string GetXMLTag() {
		return XML_TAG;
	}

	public XElement GetXML() {
		return new XElement(XML_TAG, id);
	}
}
