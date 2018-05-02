using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PurchaseDisplay : Display {
	[SerializeField] private InputField buyerName; 
	[SerializeField] private InputField itemName; 

	[SerializeField] private GameObject footer; 

	[SerializeField] private Text totalPrice; 

	[SerializeField] private Dropdown purchaseType; 

	[SerializeField] private Toggle taxable; 


	[SerializeField] private LabeledInputField basePriceField;
	[SerializeField] private DuoInputField     prototypeAddOnRegion;
	[SerializeField] private NoteField         prototypeNoteRegion;

	[SerializeField] private RectTransform details;

	private List<DuoInputField> addOns = new List<DuoInputField>();
	private List<NoteField>  notes  = new List<NoteField>();

	private Purchase purchase = new Purchase();

	private bool expanded = false;


	void Start() {
		prototypeAddOnRegion.gameObject.SetActive(false);
		prototypeNoteRegion.gameObject.SetActive(false);

		Item   nngt = new Item(1, "Nom Nom Green Tea");
		Buyer  jry  = new Buyer(5, "Jerry");
		Purchase p1 = new Purchase(nngt, jry, 3.95f, 0.095f, 0.0f);
		p1.AddOn("Boba", 0.5f);
		p1.AddOn("Pudding", 0.5f);
		p1.AddNote("Quarter Sugar", "No ice");

		purchase = p1;

		expanded = true;

		Redraw();
	}


	public void SetPurchase(Purchase _purchase) {
		purchase = _purchase;

		Redraw();
	}

	public void Expand(bool expand) {
		expanded = expand;

		Redraw();
	}

	public override void OnEditEnded() {
		UpdatePurchaseData();
		Redraw();
	}

	public override void OnInputChanged() {
		RefreshBasePrice();
	}

	////////////////////////////////////////////////////////////////////////////////////////////////
	// PRIVATE IMPLEMENTATION
	////////////////////////////////////////////////////////////////////////////////////////////////
	private void RefreshBasePrice() {
		float newPrice = Util.FloatOrZero(basePriceField.GetField());
		for (int i = 0; i < addOns.Count; i++) {
			newPrice += Util.FloatOrZero(addOns[i].GetField2());
		}

		if (taxable.isOn) {
			newPrice += purchase.tax * newPrice;
		}


		float currentPrice = Util.FloatOrZero(totalPrice.text);
		if (currentPrice != newPrice) {
			Debug.Log("Refreshing base price... " + newPrice);
			totalPrice.QuietlySetValue(string.Format("{0:f2}", newPrice));
		}

	}

	private void Redraw() {
		if (buyerName.text != purchase.buyer.name) {
			buyerName.QuietlySetValue(purchase.buyer.name);
		}

		if (itemName.text != purchase.item.name) {
			itemName.QuietlySetValue(purchase.item.name);
		}

		float currentPrice = Util.FloatOrZero(totalPrice.text);
		float newPrice = purchase.GetTotal();
		if (currentPrice != newPrice) {
			totalPrice.QuietlySetValue(string.Format("{0:f2}", newPrice));
		}

		taxable.QuietlySetValue(purchase.tax != 0f);

		float verticalPos   = 0f;
		float regionHeight  = -40f;
		float footerHeight = -140f;

		if (expanded == false) {

			basePriceField.gameObject.SetActive(false);

			footer.SetActive(false);

			for (int i = 0; i < addOns.Count; i++) {
				addOns[i].gameObject.SetActive(false);
			}

			for (int i = 0; i < notes.Count; i++) {
				notes[i].gameObject.SetActive(false);
			}
		}
		else {
			basePriceField.gameObject.SetActive(true);
			basePriceField.SetField(purchase.basePrice.ToCurrency());

			verticalPos += regionHeight;

			int idx = 0;
			for ( ; idx < purchase.addOns.Count; idx++) {
				while (addOns.Count <= idx) {
					GameObject go = Object.Instantiate(prototypeAddOnRegion.gameObject, prototypeAddOnRegion.transform.parent);

					DuoInputField region = go.GetComponent<DuoInputField>();
					if (region != null) {
						addOns.Add(region);
					}
					else {
						Debug.LogError("Could not instantate a new add on region?");
						break;
					}
				}

				addOns[idx].gameObject.SetActive(true);
				addOns[idx].SetField1(purchase.addOns[idx].description);
				addOns[idx].SetField2(purchase.addOns[idx].price.ToCurrency());

				RectTransform rectTransform = addOns[idx].GetComponent<RectTransform>();
				if (rectTransform != null) {
					Vector2 pos = rectTransform.anchoredPosition;
					pos.y = verticalPos;
					rectTransform.anchoredPosition = pos;
					verticalPos += regionHeight;
				}

			}

			for (; idx < addOns.Count; idx++) {
				addOns[idx].gameObject.SetActive(false);
			}

			idx = 0;
			for ( ; idx < purchase.notes.Count; idx++) {
				while (notes.Count <= idx) {
					GameObject go = Object.Instantiate(prototypeNoteRegion.gameObject, prototypeNoteRegion.transform.parent);

					NoteField region = go.GetComponent<NoteField>();
					if (region != null) {
						notes.Add(region);
					}
					else {
						Debug.LogError("Could not instantate a new note region?");
						break;
					}
				}

				notes[idx].gameObject.SetActive(true);
				notes[idx].SetNote(purchase.notes[idx]);

				RectTransform rectTransform = notes[idx].GetComponent<RectTransform>();
				if (rectTransform != null) {
					Vector2 pos = rectTransform.anchoredPosition;
					pos.y = verticalPos;
					rectTransform.anchoredPosition = pos;
					verticalPos += regionHeight;
				}

			}

			for (; idx < notes.Count; idx++) {
				notes[idx].gameObject.SetActive(false);
			}

			footer.SetActive(true);
			verticalPos += footerHeight;
		}

		Vector2 sd = details.sizeDelta;
		sd.y = Mathf.Abs(verticalPos);
		details.sizeDelta = sd; 
	}

	private void UpdatePurchaseData() {
		// TODO -- Search for new item/buyer by look up.
		purchase.buyer.name = buyerName.text;
		purchase.item.name  = itemName.text;

		if (taxable.isOn) {
			purchase.tax = 0.095f;
		}
		else {
			purchase.tax = 0f;
		}

		purchase.basePrice = Util.FloatOrZero(basePriceField.GetField());

		for (int i = 0; i < addOns.Count; i++) {
			while (i >= purchase.addOns.Count) {
				purchase.addOns.Add(new AddOn());
			}

			purchase.addOns[i] = new AddOn(addOns[i].GetField1(), Util.FloatOrZero(addOns[i].GetField2()));
		}

		for (int i = 0; i < notes.Count; i++) {
			while (i >= purchase.addOns.Count) {
				purchase.notes.Add("");
			}

			purchase.notes[i] = notes[i].GetNote();
		}
	}
}