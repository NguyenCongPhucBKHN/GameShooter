using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class AudioManager: MonoBehaviour
{
    [SerializeField] private UnityEngine.AudioSource m_Music;
    [SerializeField] private UnityEngine.AudioSource m_SFX;
    [SerializeField] private UnityEngine.AudioClip m_HomeMusicClip;
    [SerializeField] private UnityEngine.AudioClip m_BattleMusicClip;
    [SerializeField] private UnityEngine.AudioClip m_LazerSFXClip;
    [SerializeField] private UnityEngine.AudioClip m_PlasmaSFXClip;
    [SerializeField] private UnityEngine.AudioClip m_HitSFXClip;
    [SerializeField] private UnityEngine.AudioClip m_ExplosionSFXClip;
 
    public void PlayHomeMusic()
    {
        if(m_Music.clip==m_HomeMusicClip)
            return;
        m_Music.loop= true;
        m_Music.clip= m_HomeMusicClip;
        m_Music.Play();
    }

    public void PlayBattleMusic()
    {   
        if(m_Music.clip==m_BattleMusicClip)
            return;
        m_SFX.loop= true;
        m_SFX.clip= m_BattleMusicClip;
        m_SFX.Play();
    }
    
    public void PlayLazerSFX()
    {
        m_SFX.PlayOneShot(m_LazerSFXClip);
    }

    public void PlayPlasmaSFX()
    {
        m_SFX.PlayOneShot( m_PlasmaSFXClip);
    }

    public void PlayHitSFX()
    {
        m_SFX.PlayOneShot(m_HitSFXClip);
    }

    public void PlayExplosionSFX()
    {
        m_SFX.PlayOneShot(m_ExplosionSFXClip);
    }
    
    
    
}
