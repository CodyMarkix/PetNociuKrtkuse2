using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSprite : MonoBehaviour {
    public Sprite[] krtkusSprites;
    private System.Random rng = new System.Random();

    void OnEnable() {
        StartCoroutine(ChangeSprite());
    }

    IEnumerator ChangeSprite() {
        while (true) {
            int chance = rng.Next(0, 10);
            if (chance > 2) { StartCoroutine(ActualChangeSprite()); }
            yield return new WaitForSeconds(1.1f);
        }
    }

    IEnumerator ActualChangeSprite() {
        int newSpriteIndex1 = rng.Next(1, 3);
        int newSpriteIndex2 = rng.Next(1, 3);

        transform.gameObject.GetComponent<Image>().sprite = krtkusSprites[newSpriteIndex1];
        yield return new WaitForSeconds(.1f);
        transform.gameObject.GetComponent<Image>().sprite = krtkusSprites[0];
    }
}
