using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainRandomSprite : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] spritesToApply;
    [SerializeField] private int[] randomRotationValue;

    private void Start()
    {
        RandomizeSpriteOnStart();
    }

    private void RandomizeSpriteOnStart()
    {
        int rand = Random.Range(0, spritesToApply.Length - 1);
        spriteRenderer.sprite = spritesToApply[rand];
        rand = Random.Range(0, randomRotationValue.Length - 1);
        spriteRenderer.gameObject.transform.eulerAngles = new Vector3(spriteRenderer.gameObject.transform.eulerAngles.x, spriteRenderer.gameObject.transform.eulerAngles.y, randomRotationValue[rand]);
    }
}
