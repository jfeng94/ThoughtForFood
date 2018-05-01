using UnityEngine;
using UnityEngine.UI;

public class BasePriceField : Field {
	[SerializeField] InputField price;

	public void SetBasePrice(float cost) {
		string s = string.Format("{0:f2}", cost);
		if (price.text != s) {
			price.QuietlySetValue(s);
		}
	}

	public float GetBasePrice() {
		return Util.FloatOrZero(price.text);
	}
}
