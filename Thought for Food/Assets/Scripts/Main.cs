using UnityEngine;

public class Main : MonoBehaviour {
	void Update() {
		if (Input.GetKeyDown(KeyCode.Space)) {
			TestTripWrite();
		}
	}

	private void TestTripWrite() {
		float taxRate = 0.095f;

		Item   nngt = new Item(1, "Nom Nom Green Tea");
		Buyer  jry  = new Buyer(5, "Jerry");
		Purchase p1 = new Purchase(nngt, jry, 3.95f, taxRate, 0.0f);
		p1.AddOn("Boba", 0.5f);
		p1.AddNote("Quarter Sugar", "No ice");

		Item   rmt  = new Item(4, "Rose Milk Tea");
		Buyer  grc  = new Buyer(5, "Grace");
		Purchase p2 = new Purchase(rmt, grc, 3.95f, taxRate, 0.0f);
		p2.AddOn("Boba", 0.5f);
		p2.AddNote("No ice");

		Buyer tony  = new Buyer(12, "Tony");
		Purchase p3 = new Purchase(nngt, tony, 3.95f, taxRate, 0.0f);
		p3.AddNote("Half Sugar", "No ice");

		Item   gagt = new Item(7, "Green Apple Green Tea");
		Buyer  aris = new Buyer(17, "Aris");
		Purchase p4 = new Purchase(gagt, aris, 3.95f, taxRate, 0.0f);
		p4.AddOn("Lychee Jelly", 0.5f);
		p4.AddOn("Boba", 0.5f);
		p4.AddNote("Half Sugar", "No ice");


		Item   chkn = new Item(2, "Popcorn Chicken");
		Purchase p5 = new Purchase(chkn, jry, 4.95f, taxRate, 0.0f);
		p5.AddNote("Mild");


		Trip trip = new Trip();
		trip.SetID(0);
		trip.SetDate(2018, 4, 20);
		trip.SetVendor(new Vendor(69, "FTB"));
		trip.AddPurchases(p1, p2, p3, p4, p5);

		trip.Write();
	}
}