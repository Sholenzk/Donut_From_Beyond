using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class life : MonoBehaviour
{
    public int maxLife;
    public int currentLife;
    public float invulnebility;
    
     // Start is called before the first frame update
    void Awake()
    {
        currentLife = maxLife;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentLife == 0)
        {
            Destroy(this.gameObject);
        }
        invulnebility += Time.deltaTime;
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
