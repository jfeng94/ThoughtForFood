using UnityEngine;
using UnityEngine.UI;

public class DuoInputField : Field {
	[SerializeField] InputField field1;
	[SerializeField] InputField field2;

	public void SetField1(string s) {
		if (field1.text != s) {
			field1.QuietlySetValue(s);
		}
	}

	public void SetField2(string s) {
		if (field2.text != s) {
			field2.QuietlySetValue(s);
		}
	}

	public string GetField1() {
		return field1.text;
	}

	public string GetField2() {
		return field2.text;
	}
}
