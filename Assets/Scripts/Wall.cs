using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {
	public Sprite dmgSprite;
	public AudioClip chopSound1;
	public AudioClip chopSound2;
	public int hp = 4;
	private SpriteRenderer SpriteRenderer;

	// Use this for initialization
	void Awake () {
		 SpriteRenderer = GetComponent<SpriteRenderer>();

	}

	public void DamageWall(int loss){
		SpriteRenderer.sprite = dmgSprite;
		hp = hp - loss;
		SoundManager.instance.RandomizeSfc(chopSound1,chopSound2);
		if(hp < 0){
			gameObject.SetActive(false);
		}
	}

}
