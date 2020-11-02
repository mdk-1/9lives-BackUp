using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int totalHearts;
    public Image[] hearts;
    public Sprite heartFull;
    public Sprite heartEmpty;

    private void Update()
    {
        for (int i = 0; i < hearts.Length; i++)
            {

                if (GameManager.instance.health > totalHearts)
                {
                    GameManager.instance.health = totalHearts;
                }

                if (i < GameManager.instance.health)
                {
                    hearts[i].sprite = heartFull;
                }
                else
                {
                    hearts[i].sprite = heartEmpty;
                }

                if (i < totalHearts)
                {
                    hearts[i].enabled = true;
                }
                else
                {
                    hearts[i].enabled = false;
                }
            }
     }
        
}
