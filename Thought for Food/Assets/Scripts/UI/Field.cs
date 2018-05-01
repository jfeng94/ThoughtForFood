using UnityEngine;
using UnityEngine.UI;

public class Field : MonoBehaviour {
	[SerializeField] private Display display; 

	// Set up event handlers
	void OnEnable() {
		foreach (Transform child in transform) {
			{
				var uiElement = child.gameObject.GetComponent<Dropdown>();
				if (uiElement != null) {
					uiElement.onValueChanged.AddListener(OnInputChanged);
				}
			}
			{
				var uiElement = child.gameObject.GetComponent<InputField>();
				if (uiElement != null) {
					uiElement.onValueChanged.AddListener(OnInputChanged);
					uiElement.onEndEdit.AddListener(OnEditEnded);
				}
			}
			{
				var uiElement = child.gameObject.GetComponent<Slider>();
				if (uiElement != null) {
					uiElement.onValueChanged.AddListener(OnInputChanged);
				}
			}
			{
				var uiElement = child.gameObject.GetComponent<Toggle>();
				if (uiElement != null) {
					uiElement.onValueChanged.AddListener(OnInputChanged);
				}
			}
		}
	}

	void OnDisable() {
		foreach (Transform child in transform) {
			{
				var uiElement = child.gameObject.GetComponent<Dropdown>();
				if (uiElement != null) {
					uiElement.onValueChanged.RemoveListener(OnInputChanged);
				}
			}
			{
				var uiElement = child.gameObject.GetComponent<InputField>();
				if (uiElement != null) {
					uiElement.onValueChanged.RemoveListener(OnInputChanged);
					uiElement.onEndEdit.RemoveListener(OnEditEnded);
				}
			}
			{
				var uiElement = child.gameObject.GetComponent<Slider>();
				if (uiElement != null) {
					uiElement.onValueChanged.RemoveListener(OnInputChanged);
				}
			}
			{
				var uiElement = child.gameObject.GetComponent<Toggle>();
				if (uiElement != null) {
					uiElement.onValueChanged.RemoveListener(OnInputChanged);
				}
			}
		}

	}

	public void OnEditEnded<T>(T o) {
		display.OnEditEnded();
	}

	public void OnInputChanged<T>(T o) {
		display.OnInputChanged();
	}
}