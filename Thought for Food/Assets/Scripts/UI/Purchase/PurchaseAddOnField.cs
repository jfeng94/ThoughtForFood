using UnityEngine;
using UnityEngine.UI;

public class PurchaseAddOnField : Field {
	[SerializeField] InputField description;
	[SerializeField] InputField price;

	public void SetDescription(string s) {
		if (description.text != s) {
			description.QuietlySetValue(s);
		}
	}

	public void SetPrice(float cost) {
		string s = string.Format("{0:f2}", cost);
		if (price.text != s) {
			price.QuietlySetValue(s);
		}
	}

	public string GetDescription() {
		return description.text;
	}

	public float GetPrice() {
		return Util.FloatOrZero(price.text);
	}
}
