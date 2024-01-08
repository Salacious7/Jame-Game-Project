using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; set; }
    public FMODData FMODData { get; set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        FMODData = new FMODData();
        FMODData.InitFMODData();
    }

    public void OnMusicStop()
    {
        FMODData.music.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void OnPlayCombatMusic()
    {
        RuntimeManager.StudioSystem.setParameterByName("Game State", 2f);
        FMODData.music.start();
        FMODData.music.release();
    }

    public void OnPlayTitleMusic()
    {
        RuntimeManager.StudioSystem.setParameterByName("Game State", 3f);
        FMODData.music.start();
        FMODData.music.release();
    }

    #region Swan SFX
    public void OnPlaySwanBlock()
    {
        FMODData.swanBlock.start();
    }

    public void OnPlaySwanHealing()
    {
        FMODData.swanHealing.start();
    }

    public void OnPlaySwanHurt()
    {
        FMODData.swanHurt.start();
    }

    public void OnPlaySwanMainHeavy()
    {
        FMODData.swanMainHeavy.start();
    }

    public void OnPlaySwanMainPeckAttack()
    {
        FMODData.swanMainPeckAttack.start();
    }

    public void OnPlaySwanSkillOne()
    {
        FMODData.swanSkillOne.start();
    }

    public void OnPlaySwanSkillTwo()
    {
        FMODData.swanSkillTwo.start();
    }

    public void OnPlaySwanUseItem()
    {
        FMODData.swanUseItem.start();
    }
    #endregion

    #region Bread SFX
    public void OnPlayBaguetteDeath()
    {
        FMODData.baguetteDeath.start();
    }

    public void OnPlayBaguetteDodge()
    {
        FMODData.baguetteDodge.start();
    }

    public void OnPlayBaguetteHeal()
    {
        FMODData.baguetteHeal.start();
    }

    public void OnPlayBaguetteMainAttack()
    {
        FMODData.baguetteMainAttack.start();
    }

    public void OnPlayBaguetteHeavyAttack()
    {
        FMODData.baguetteHeavyAttack.start();
    }

    public void OnPlayBaguetteSkillOne()
    {
        FMODData.baguetteSkillOne.start();
    }

    public void OnPlayBaguetteSkillTwo()
    {
        FMODData.baguetteSkillTwo.start();
    }

    public void OnPlayMuffinDeath()
    {
        FMODData.muffinDeath.start();
    }

    public void OnPlayMuffinDodge()
    {
        FMODData.muffinDodge.start();
    }

    public void OnPlayMuffinHeal()
    {
        FMODData.muffinHeal.start();
    }

    public void OnPlayMuffinMainAttack()
    {
        FMODData.muffinMainAttack.start();
    }

    public void OnPlayMuffinHeavyAttack()
    {
        FMODData.muffinHeavyAttack.start();
    }

    public void OnPlayMuffinSkillOne()
    {
        FMODData.muffinSkillOne.start();
    }

    public void OnPlayMuffinSkillTwo()
    {
        FMODData.muffinSkillTwo.start();
    }

    public void OnPlayPretzelDeath()
    {
        FMODData.pretzelDeath.start();
    }

    public void OnPlayPretzelDodge()
    {
        FMODData.pretzelDodge.start();
    }

    public void OnPlayPretzelHeal()
    {
        FMODData.pretzelHeal.start();
    }

    public void OnPlayPretzelMainAttack()
    {
        FMODData.pretzelMainAttack.start();
    }

    public void OnPlayPretzelHeavyAttack()
    {
        FMODData.pretzelHeavyAttack.start();
    }

    public void OnPlayPretzelSkillOne()
    {
        FMODData.pretzelSkillOne.start();
    }

    public void OnPlayPretzelSkillTwo()
    {
        FMODData.pretzelSkillTwo.start();
    }

    public void OnPlaySandwichDeath()
    {
        FMODData.sandwichDeath.start();
    }

    public void OnPlaySandwichDodge()
    {
        FMODData.sandwichDodge.start();
    }

    public void OnPlaySandwichHeal()
    {
        FMODData.sandwichHeal.start();
    }

    public void OnPlaySandwichMainAttack()
    {
        FMODData.sandwichMainAttack.start();
    }

    public void OnPlaySandwichHeavyAttack()
    {
        FMODData.sandwichHeavyAttack.start();
    }

    public void OnPlaySandwichSkillOne()
    {
        FMODData.sandwichSkillOne.start();
    }

    public void OnPlaySandwichSkillTwo()
    {
        FMODData.sandwichSkillTwo.start();
    }
    #endregion

    #region Cutscene SFX
    public void OnPlayPanelOne()
    {
        RuntimeManager.StudioSystem.setParameterByName("Game State", 4f);
    }

    public void OnPlayPanelTwo()
    {
        RuntimeManager.StudioSystem.setParameterByName("Game State", 5f);
    }

    public void OnPlayPanelThree()
    {
        RuntimeManager.StudioSystem.setParameterByName("Game State", 6f);
    }

    public void OnPlayPanelFour()
    {
        RuntimeManager.StudioSystem.setParameterByName("Game State", 7f);
    }

    public void OnPlayPanelFive()
    {
        RuntimeManager.StudioSystem.setParameterByName("Game State", 8f);
    }

    public void OnPlayPanelSix()
    {
        RuntimeManager.StudioSystem.setParameterByName("Game State", 9f);
    }

    public void OnPlayPanelEnd()
    {
        RuntimeManager.StudioSystem.setParameterByName("Game State", 10f);
    }
    #endregion
}
