using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;
    public TMP_Text HealtText;

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthText();
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        UpdateHealthText();

        if (currentHealth <= 0)
        {
            // Le joueur est mort, changer de scène
            SceneManager.LoadScene("EndScene");
        }
    }

    void UpdateHealthText()
    {
        HealtText.text = "Vie : " + currentHealth.ToString();
    }
}
