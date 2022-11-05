using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    public Slider slider;
    public Color low;
    public Color high;
    public Vector3 offset; // used because not all animals have the same height
    // Start is called before the first frame update
    public void setHealth(float health, float maxHealth)
    { // need to set at start of animal controller and whenever an animal takes damage
        if (health <= 0)
        {
            Destroy(this.gameObject.GetComponentInParent<HealthBarController>().gameObject); // removes the health bar after no more health
        }
        slider.gameObject.SetActive(health < maxHealth);
        slider.value = health;
        slider.maxValue = maxHealth;

        slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(low, high, slider.normalizedValue);
    }
    // Update is called once per frame
    void Update()
    {
        slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offset);
    }
}
