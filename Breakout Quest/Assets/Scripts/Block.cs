using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    // Configuration parameters
    [SerializeField] Sprite[] hitSprites;
    [SerializeField] int maxHits;
    [SerializeField] GameObject blockDestroyVFX;
    [SerializeField] AudioClip breakSound;
    enum BlockType { REGULAR, ENHANCING };
    [SerializeField] GameObject powerup;


    [SerializeField] int timesHit;  // TODO deserialize this
    [SerializeField] BlockType m_blockType;

    Level level;

    // Start is called before the first frame update
    void Start()
    {
        level = FindObjectOfType<Level>();
        CountBreakableBlocks();
        maxHits = hitSprites.Length + 1;
    }

    void OnCollisionEnter2D()
    {
        if (gameObject.CompareTag("Breakable"))
        {
            HandleHit();
        }
    }

    private void CountBreakableBlocks()
    {
        if (gameObject.CompareTag("Breakable"))
        {
            level.CountBlock();
        }
    }

    private void HandleHit()
    {
        timesHit++;
        if (timesHit >= maxHits)
        {
            PlayDestroySFX();
            PlayDeathEffect();
            DestroyBlock();
        }
        else
        {
            ShowNextHitSprite();
        }
    }

    private void PlayDestroySFX()
    {
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);
        FindObjectOfType<Score>().AddToScore();
    }

    private void PlayDeathEffect()
    {
        GameObject effect = Instantiate(blockDestroyVFX, transform.position,
            Quaternion.identity);
        Destroy(effect, 1f);
    }

    private void DestroyBlock()
    {
        Destroy(gameObject);
        level.BlockDestroyed();
        if (m_blockType == BlockType.ENHANCING)
        {
            Instantiate(powerup, transform.position, Quaternion.identity);
        }
    }

    private void ShowNextHitSprite()
    {
        int index = timesHit - 1;
        if (hitSprites[index] != null)
        {
            GetComponent<SpriteRenderer>().sprite = hitSprites[index];
        }
        else
        {
            Debug.LogError("Block sprite " + gameObject.name +
                " is missing from array.");
        }
    }
}
