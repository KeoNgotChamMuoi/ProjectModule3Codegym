using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private HealthSystem healthSystem;
    [SerializeField] private Scrollbar healthScrollbar; // Kéo Scrollbar vào đây

    private void OnEnable()
    {
        // Đăng ký sự kiện khi máu thay đổi
        if (healthSystem != null)
        {
            healthSystem.OnHealthChanged.AddListener(UpdateHealthBar);
        }
    }
    private void OnDisable()
    {
        // Hủy đăng ký sự kiện khi không cần thiết
        if (healthSystem != null)
        {
            healthSystem.OnHealthChanged.RemoveListener(UpdateHealthBar);
        }
    }

    private void UpdateHealthBar(float healthPerhealthPercent)
    {
        if (healthScrollbar != null)
        {
            healthScrollbar.size = healthPerhealthPercent; // Cập nhật thanh máu
        }
    }

}
