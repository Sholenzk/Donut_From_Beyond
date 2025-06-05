using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class life : MonoBehaviour
{
    public int maxLife;
    public int currentLife;
    public float invulnebility;

    public Slider lifeSlider;
    // Start is called before the first frame update
    void Awake()
    {
        currentLife = maxLife;

        if (lifeSlider != null)
        {
            lifeSlider.maxValue = maxLife;
            lifeSlider.value = currentLife;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentLife == 0)
        {
            Destroy(this.gameObject);
            Die();
        }
        invulnebility += Time.deltaTime;

        if (lifeSlider != null)
        {
            lifeSlider.value = currentLife;
        }
    }

    public void DamagePlayer()
    {
        if (invulnebility >= 2)
        {
            currentLife --;
            invulnebility = 0;
        }
        
    }

    public void Die()
    {
        if (currentLife == 0)
        {
            SceneManager.LoadScene("Moriste");
        }
    }
    
    
    
}
