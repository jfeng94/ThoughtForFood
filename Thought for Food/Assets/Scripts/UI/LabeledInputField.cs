using UnityEngine;
using UnityEngine.UI;

public class LabeledInputField : Field {
	[SerializeField] InputField field;

	public void SetField(string s) {
		if (field.text != s) {
			field.QuietlySetValue(s);
		}
	}

	public string GetField() {
		return field.text;
	}
}
