using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public AudioClip[] bossPhaseSounds; // 0 = fase 1, 1 = fase 2, 2 = fase 3
    public AudioClip playerHitSound;
    public AudioClip playerShootSound;
    public AudioClip shieldBreakSound;
    public AudioClip shieldRegenSound;

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayBossPhaseSound(int phase)
    {
        audioSource.PlayOneShot(bossPhaseSounds[phase]);
    }

    public void PlayPlayerHit() => audioSource.PlayOneShot(playerHitSound);
    public void PlayPlayerShoot() => audioSource.PlayOneShot(playerShootSound);
    public void PlayShieldBreak() => audioSource.PlayOneShot(shieldBreakSound);
    public void PlayShieldRegen() => audioSource.PlayOneShot(shieldRegenSound);
}