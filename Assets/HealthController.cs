using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Include the UI namespace

public class HealthController : MonoBehaviour
{
    [SerializeField]
    private float maxHealth;
    private float currentHealth;

    [SerializeField]
    private float respawnTime;

    [SerializeField]
    private GameObject healthPanel;

    [SerializeField]
    private Text healthText;

    [SerializeField]
    private RectTransform healthBar;
    private float healthBarWidth; // Use this variable name consistently
    private MeshRenderer meshRenderer;
    private bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        healthBarWidth = healthBar.sizeDelta.x; // Use the correct variable name
        meshRenderer = GetComponent<MeshRenderer>();
        ResetHealth();
        UpdateHealthUI();
    }

    public void ApplyDamage(float damage)
    {
        if (isDead) return;

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            isDead = true;
            meshRenderer.enabled = false;
            healthPanel.SetActive(false);
            StartCoroutine(RespawnAfterTime());
        }

        UpdateHealthUI();
    }

    IEnumerator RespawnAfterTime()
    {
        yield return new WaitForSeconds(respawnTime);
        ResetHealth();
    }

    private void ResetHealth()
    {
        isDead = false;
        currentHealth = maxHealth;
        meshRenderer.enabled = true;
        healthPanel.SetActive(true);
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        float percentOutOf = (currentHealth / maxHealth) * 100;
        float newWidth = (percentOutOf / 100f) * healthBarWidth; // Use the same variable here

        healthBar.sizeDelta = new Vector2(newWidth, healthBar.sizeDelta.y);
        healthText.text = currentHealth + "/" + maxHealth;
    }
}
