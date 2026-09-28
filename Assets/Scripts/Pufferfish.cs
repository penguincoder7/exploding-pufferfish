using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

// Attach this to every pufferfish prefab (player and enemies).
// Requires a Collider2D (set as Trigger or normal, doesn't matter which,
// since we use OverlapCircleAll instead of collision events).
public class Pufferfish : MonoBehaviour
{
    [SerializeField] float explosionRadius = 2f;       // how far the shockwave reaches
    [SerializeField] float visualExpandTime = 0.15f;   // how long the shockwave visual takes to grow

    [SerializeField] LayerMask pufferfishLayer;        // set this to a "Pufferfish" layer in the Inspector
    [SerializeField] GameObject explosionVisualPrefab; // optional: a circle/particle prefab to show the blast
    [SerializeField] GameManager gm;

    private bool hasExploded = false;
    [SerializeField] ParticleSystem deathParticles;

    void Update()
    {
        // Only the player should call this — enemies get triggered externally.
        if (CompareTag("Player") && Keyboard.current.spaceKey.wasPressedThisFrame && !GetComponent<PlayerController>().IsDead())
        {
            Explode();
        }
    }

    // Public so other pufferfish (or the player) can trigger this one.
    public void Explode()
    {
        if (hasExploded) return; // prevent double-triggering in a chain
        hasExploded = true;
        deathParticles.Emit(15);

        // Optional: spawn a visual shockwave effect
        if (explosionVisualPrefab != null)
        {
            gm.CreateExplosion(explosionVisualPrefab, transform, explosionRadius, visualExpandTime);
        }

        // Find all pufferfish within blast radius
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRadius, pufferfishLayer);

        foreach (Collider2D hit in hits)
        {
            if (hit.gameObject == this.gameObject) continue; // don't re-trigger self

            Pufferfish other = hit.GetComponent<Pufferfish>();
            if (other != null && !other.hasExploded)
            {
                // Delay so the chain visibly ripples outward instead of everything vanishing instantly
                other.StartCoroutine(other.DelayedExplode(gm.getChainDelay()));
            }
        }

        // Handle death (player or enemy)
        Die();
    }

    private IEnumerator DelayedExplode(float delay)
    {
        yield return new WaitForSeconds(delay);
        Explode();
    }

    private void Die()
    {
        // Swap this out for animation/particles/sound as time allows.
        // For jam speed: just destroy the object after a tiny delay so the
        // explosion visual has a frame to appear first.
        Destroy(gameObject, 0.15f);

        Transform spriteObj = transform.Find("Sprite");
        spriteObj.GetComponent<SpriteRenderer>().enabled = false;
        if (CompareTag("Player"))
        {
            transform.GetComponent<PlayerController>().MarkDead();
            gm.OnPlayerDied(false);
        } else
        {
            gm.OnEnemyDied();
        }
    }
}