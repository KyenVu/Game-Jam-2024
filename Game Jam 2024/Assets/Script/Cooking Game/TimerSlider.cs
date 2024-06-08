using UnityEngine;
using UnityEngine.UI;

public class TimerSlider : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void SetMaxTime(float time)
    {
        slider.maxValue = time;
        slider.value = time;
        fill.color = gradient.Evaluate(1f);
    }

    public void SetTime(float time)
    {
        slider.value = time;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
