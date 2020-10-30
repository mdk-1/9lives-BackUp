using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossHeadCollide : MonoBehaviour
{
    public BossManager bossManager;

    private SpriteRenderer aiSpriteRenderer;
    private Color oColor;
    private bool aiInvuln;

    public GameObject bloodSplat; //reference for particle effect

    private void Awake()
    {
        aiSpriteRenderer = bossManager.GetComponent<SpriteRenderer>();
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (aiInvuln == false)
            {
                if (bossManager.aiHealth == 2)
                {
                    bossManager.aiHealth -= 1;
                    Instantiate(bloodSplat, transform.position, Quaternion.identity);
                    TakeDamage();
                    bossManager.OnAiHit();
                }
                else if (bossManager.aiHealth == 1)
                {
                    bossManager.OnAiDeath();
                }
            }
            else
            {
                return;
            }
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
    }
}
