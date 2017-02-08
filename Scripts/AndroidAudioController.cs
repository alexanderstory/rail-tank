using UnityEngine;
using System.Collections;

public class AndroidAudioController : MonoBehaviour {


    public static AndroidAudioController current;
    private int gunSound;
    private int railSound;
    private int enemyExplosion;
    private int playerExplosion;

	// Use this for initialization

    void Start () {
        current = this;

        AndroidNativeAudio.makePool(15);
        railSound = AndroidNativeAudio.load("AntiMaterialRifle_1p_02.wav");
        gunSound = AndroidNativeAudio.load("AutoGun_1p_02.wav");
        enemyExplosion = AndroidNativeAudio.load("explosion_enemy.wav");
        playerExplosion = AndroidNativeAudio.load("explosion_player.wav");
    }
	
	// Update is called once per frame
    public void RailSound()
    {
        AndroidNativeAudio.play(railSound);
    }
    public void GunSound()
    {
        AndroidNativeAudio.play(gunSound);
    }
    public void EnemyExplosion()
    {
        AndroidNativeAudio.play(enemyExplosion);
    }
    public void PlayerExplosion()
    {
        AndroidNativeAudio.play(playerExplosion);
    }
}
