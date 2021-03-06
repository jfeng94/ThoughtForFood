using UnityEngine;
using UnityEngine.UI;

public class NoteField : Field {
	[SerializeField] InputField note;

	public void SetNote(string s) {
		if (note.text != s) {
			note.QuietlySetValue(s);
		}
	}

	public string GetNote() {
		return note.text;
	}
}
