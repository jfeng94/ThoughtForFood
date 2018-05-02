using UnityEngine;
using UnityEngine.UI;

public static class Util {
	

	public static float FloatOrDefault(string value, float defaultValue) {
		float convertedFloat;
		bool result = float.TryParse(value, out convertedFloat);

		return result ? convertedFloat : defaultValue;
	}

	// if possible to convert value to an integer, returns that integer
	// otherwise, returns zero
	public static float FloatOrZero(string value) {
		return FloatOrDefault(value, 0f);
	}

	// if possible to convert value to an integer, returns that integer
	// otherwise, returns zero
	public static float FloatOrOne(string value) {
		return FloatOrDefault(value, 1f);
	}

	public static string ToCurrency(this float cost) {
		return string.Format("{0:f2}", cost);
	}

	////////////////////////////////////////////////////////////////////////////////////////////////
	// UI
	////////////////////////////////////////////////////////////////////////////////////////////////
	// Since Unity forces onValueChanged to be called every time we access the value of the 
	// ui element, here's a set of value setters to circumvent the trigger.
	static Slider.SliderEvent emptySliderEvent = new Slider.SliderEvent();
	public static void QuietlySetValue(this Slider instance, float value)
	{
	    var originalEvent = instance.onValueChanged;
	    instance.onValueChanged = emptySliderEvent;
	    instance.value = value;
	    instance.onValueChanged = originalEvent;
	}
	
	static Toggle.ToggleEvent emptyToggleEvent = new Toggle.ToggleEvent();
	public static void QuietlySetValue(this Toggle instance, bool value)
	{
	    var originalEvent = instance.onValueChanged;
	    instance.onValueChanged = emptyToggleEvent;
	    instance.isOn = value;
	    instance.onValueChanged = originalEvent;
	}
	
	static InputField.OnChangeEvent emptyInputFieldEvent = new InputField.OnChangeEvent();
	public static void QuietlySetValue(this InputField instance, string value)
	{
	    var originalEvent = instance.onValueChanged;
	    instance.onValueChanged = emptyInputFieldEvent;
	    instance.text = value;
	    instance.onValueChanged = originalEvent;
	}

	// Text technically doesn't have an OnValueChanged, but for the sake of uniform code
	// structure, we provide an identical method for Text
	public static void QuietlySetValue(this Text instance, string value)
	{
	    instance.text = value;
	}
}