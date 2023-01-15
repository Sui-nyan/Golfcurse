using UnityEngine;

public class Stats : MonoBehaviour
{
    private Healthbar healthbar;
    [SerializeField] private Vector3 healthbarOffset = new Vector3(0, 30, 0);
    public float Health => health;
    public float Attack => attack;
    [SerializeField] private float health;
    [SerializeField] private float attack;

    private void Start()
    {
        // If there's no healthbar
        if (!healthbar)
        {
            var prefab = Resources.Load<Healthbar>("EnemyHealthbar");
            var canvas = FindObjectOfType<Canvas>();
            healthbar = Instantiate(prefab, canvas.transform);
        }

        healthbar.SetMaxSliderValue(health);
    }

    private void Update()
    {
        //healthbar follows the game object
        if (healthbar && Camera.main)
        {
            healthbar.transform.position = Camera.main.WorldToScreenPoint(transform.position) + healthbarOffset;
        }
    }

    /// <summary>
    /// handles health and what happens when health drops below 0
    /// </summary>
    /// <param name="damage">health to be substracted</param>
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (healthbar) healthbar.SetHeath(health);

        if (health <= 0)
        {
            Destroy(gameObject);
            Destroy(healthbar.gameObject);
            FindObjectOfType<VisualEffectsManager>().OnDeath(gameObject);
            FindObjectOfType<SoundEffectManager>().onDeath(gameObject);
        }
    }
}