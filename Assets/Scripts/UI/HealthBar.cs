using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HealthBar : MyMonoBehaviour
    {
        private Slider _healthSlider;

        protected override void OnEnable()
        {
            LoadSlider();
        }

        protected virtual void LoadSlider()
        {
            if (_healthSlider == null)
                _healthSlider = GetComponent<Slider>();
        }

        public void SetMaxHealthValue(int maxHealth)
        {
            LoadSlider();
            _healthSlider.maxValue = maxHealth;
            _healthSlider.value = maxHealth;
        }

        public void SetHealthBarValue(int health)
        {
            _healthSlider.value = health;
        }
    }
}