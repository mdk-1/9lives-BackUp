using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBodyCollide : MonoBehaviour
{
    public GameObject player;
    public Collider2D headCollider;

    private SpriteRenderer playerSpriteRenderer;
    private Rigidbody2D playerRigidBody;
    private Color oColor;
    private bool playerInvuln;
    private Vector3 knockBack;

    public GameObject bloodSplat; //reference for particle effect

    private void Awake()
    {
        playerSpriteRenderer = player.GetComponent<SpriteRenderer>();
        playerRigidBody = player.GetComponent<Rigidbody2D>();
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (playerInvuln == false)
            {
                headCollider.enabled = false;
                if (GameManager.instance.health > 1)
                {
                    GameManager.instance.health -= 1;
                    Instantiate(bloodSplat, transform.position, Quaternion.identity);
                    TakeDamage();
                    knockBack = playerRigidBody.transform.position - this.transform.position;
                    playerRigidBody.AddForce(knockBack.normalized * 2500f);
                }
                else
                {
                    //might create IEnumerator here to trigger death outro before transition to game over
                    SceneManager.LoadScene(7);
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
        if (headCollider.enabled == false)
        {
            headCollider.enabled = true;
        }
    }
    public void TakeDamage()
    {
        // Tints the sprite red and fades back to the origin color after a delay of 1 second
        StartCoroutine(DamageEffectSequence(playerSpriteRenderer, Color.red, 1));
    }

    IEnumerator DamageEffectSequence(SpriteRenderer playerSpriteRenderer, Color dmgColor, float duration)
    {
        // save original sprite color
        Color oColor = playerSpriteRenderer.color;
        // tint the sprite with damage color
        playerSpriteRenderer.color = dmgColor;
        // lerp animation with given duration in seconds
        // toggle invuln so enemy can't be hit
        for (float t = 0; t < 1.0f; t += Time.deltaTime / duration)
        {
            playerSpriteRenderer.color = Color.Lerp(dmgColor, oColor, t);
            playerInvuln = true;

            yield return null;
        }

        // restore origin color
        //toggle invuln off
        playerSpriteRenderer.color = oColor;
        playerInvuln = false;
    }
}