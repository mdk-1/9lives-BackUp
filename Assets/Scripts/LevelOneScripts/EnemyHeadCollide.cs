using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyHeadCollide : MonoBehaviour
{
    public EnemyManager enemyManager;
    public Collider2D bodyCollider;

    private SpriteRenderer aiSpriteRenderer;
    private Color oColor;
    private bool aiInvuln;

    public GameObject bloodSplat; //reference for particle effect
    public AudioSource catSound;

    private void Awake()
    {
        aiSpriteRenderer = enemyManager.GetComponent<SpriteRenderer>();
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (aiInvuln == false)
            {
                bodyCollider.enabled = false;

                if (enemyManager.aiHealth == 2)
                {
                    enemyManager.aiHealth -= 1;
                    Instantiate(bloodSplat, transform.position, Quaternion.identity);
                    catSound.Play();
                    TakeDamage();
                    enemyManager.OnAiHit();
                }
                else if (enemyManager.aiHealth == 1)
                {
                    enemyManager.OnAiDeath();
                }
            }
            else
            {
                return;
            }
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (bodyCollider.enabled == false)
        {
            bodyCollider.enabled = true;
        }        
    }
    public void TakeDamage()
    {
        // Tints the sprite red and fades back to the origin color after a delay of 1 second
        StartCoroutine(DamageEffectSequence(aiSpriteRenderer, Color.red, 1));
    }

    IEnumerator DamageEffectSequence(SpriteRenderer aiSpriteRenderer, Color dmgColor, float duration)
    {
        // save original sprite color
        Color oColor = aiSpriteRenderer.color;
        // tint the sprite with damage color
        aiSpriteRenderer.color = dmgColor;
        // lerp animation with given duration in seconds
        // toggle invuln so enemy can't be hit
        for (float t = 0; t < 1.0f; t += Time.deltaTime / duration)
        {
            aiSpriteRenderer.color = Color.Lerp(dmgColor, oColor, t);
            aiInvuln = true;

            yield return null;
        }

        // restore origin color
        //toggle invuln off
        aiSpriteRenderer.color = oColor;
        aiInvuln = false;
        bodyCollider.enabled = bodyCollider.enabled;
    }
}
