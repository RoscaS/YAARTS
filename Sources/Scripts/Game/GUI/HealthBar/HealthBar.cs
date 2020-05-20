using UnityEngine;
using UnityEngine.UI;
using Utils;

public class HealthBar : MonoBehaviour
{

    public Gradient Gradient;
    public int Resolution = 5;

    private Slider slider;
    private Image fill;

    private bool visible;
    public bool Visible {
        get => visible;
        set {
            visible = value;
            gameObject.SetActive(visible);
        }
    }

    public void InitHealth(float health) {
        slider = GetComponent<Slider>();
        fill = transform.Find("Fill").GetComponent<Image>();
        slider.maxValue = health;
        SetValue(health);
        Visible = false;

    }

    public void SetValue(float health) {
        slider.value = health;
        fill.color = Gradient.Evaluate(slider.normalizedValue);
        Visible = true;
    }

    public void FillUpDelay(float time) {
        time *= Resolution;
        var frac = slider.maxValue / time;

        FunctionInterval.Create(() => {
            SetValue(slider.value + frac);
        }, 1f / Resolution, (int) time);


    }
}
