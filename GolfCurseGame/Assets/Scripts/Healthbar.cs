using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();
    }

    public void SetMaxSliderValue(float value)
    {
        slider.maxValue = value;
        slider.value = value;
    }
    public void SetHeath(float health)
    {
        slider.value = health;
    }
}
