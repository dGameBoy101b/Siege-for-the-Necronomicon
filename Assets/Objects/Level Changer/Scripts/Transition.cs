using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour
{
    public GameObject WAVE_MANAGER;
    public ParticleSystem TRAPPED_ANIMATION;
    public float ANIMATION_LENGTH;
    public float START_TIME;

    public LevelTimer LEVEL_TIMER;

    public int TOD;

    public Transform TELEPORT_TARGET;
    public ParticleSystem START_TELEPORT_ANIMATION;
    public ParticleSystem END_TELEPORT_ANIMATION;
    public GameObject PLAYER;

    public AudioSource[] TRAP_SPAWN_SOUNDS;
    public AudioSource[] NEUTRAL_THEME;
    public AudioSource[] COMBAT_THEME;

    // Start is called before the first frame update
    void Start()
    {
        EnviroSkyMgr.instance.SetTimeOfDay(TOD);
        WAVE_MANAGER.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            LEVEL_TIMER.timerIsRunning = true;
            stopNeutralTheme();
            playCombatTheme();
            EnviroSkyMgr.instance.ChangeWeather(6);
            TRAPPED_ANIMATION.Play();
            playSpawnSounds();
            Invoke("StopTrapAnimation", ANIMATION_LENGTH);
            Invoke("StartAttacks", START_TIME);
        }
    }
    public void levelCompleted()
    {
        stopCombatTheme();
        playNeutralTheme();
        Invoke("playTeleportationAnimation", 7);
    }

    private void playTeleportationAnimation()
    {
        START_TELEPORT_ANIMATION.Play();
        END_TELEPORT_ANIMATION.Play();
        Invoke("stopTeleportationAnimation", 3);
        Invoke("teleportPlayer", 3);
    }

    private void stopTeleportationAnimation()
    {
        START_TELEPORT_ANIMATION.Stop();
        END_TELEPORT_ANIMATION.Stop();
    }

    private void teleportPlayer()
    {
        PLAYER.transform.position = TELEPORT_TARGET.transform.position;
    }

    public void StartAttacks()
    {
        WAVE_MANAGER.SetActive(true);
    }

    public void StopTrapAnimation()
    {
        TRAPPED_ANIMATION.Stop();
    }
    protected void playSpawnSounds()
    {
        foreach (AudioSource aud in this.TRAP_SPAWN_SOUNDS)
        {
            aud.Play();
        }
    }
    protected void playNeutralTheme()
    {
        foreach (AudioSource aud in this.NEUTRAL_THEME)
        {
            aud.Play();
        }
    }

    protected void stopNeutralTheme()
    {
        foreach (AudioSource aud in this.NEUTRAL_THEME)
        {
            aud.Stop();
        }
    }

    protected void playCombatTheme()
    {
        foreach (AudioSource aud in this.COMBAT_THEME)
        {
            aud.Play();
        }
    }

    protected void stopCombatTheme()
    {
        foreach (AudioSource aud in this.COMBAT_THEME)
        {
            aud.Stop();
        }
    }
}
