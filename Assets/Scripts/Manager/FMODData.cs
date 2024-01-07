using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMODData
{
    public EventInstance music;
    public EventInstance baguetteDeath, muffinDeath, pretzelDeath, sandwichDeath;
    public EventInstance baguetteDodge, muffinDodge, pretzelDodge, sandwichDodge;
    public EventInstance baguetteHeal, muffinHeal, pretzelHeal, sandwichHeal;
    public EventInstance baguetteMainAttack, muffinMainAttack, pretzelMainAttack, sandwichMainAttack;
    public EventInstance baguetteHeavyAttack, muffinHeavyAttack, pretzelHeavyAttack, sandwichHeavyAttack;
    public EventInstance baguetteSkillOne, muffinSkillOne, pretzelSkillOne, sandwichSkillOne;
    public EventInstance baguetteSkillTwo, muffinSkillTwo, pretzelSkillTwo, sandwichSkillTwo;

    public EventInstance swanBlock, swanHealing, swanHurt, swanMainHeavy, swanMainPeckAttack, swanSkillOne, swanSkillTwo, swanUseItem;

    public void InitFMODData()
    {
        music = RuntimeManager.CreateInstance("event:/Music/Snack Attack! Music Layout");

        baguetteDeath = RuntimeManager.CreateInstance("event:/SFX/Bread/Baguette/Baguette Death");
        muffinDeath = RuntimeManager.CreateInstance("event:/SFX/Bread/Muffin/Muffin Death");
        pretzelDeath = RuntimeManager.CreateInstance("event:/SFX/Bread/Pretzel/Pretzel Death");
        sandwichDeath = RuntimeManager.CreateInstance("event:/SFX/Bread/Sandwich/Sandwich Death");

        baguetteDodge = RuntimeManager.CreateInstance("event:/SFX/Bread/Baguette/Baguette Dodge");
        muffinDodge = RuntimeManager.CreateInstance("event:/SFX/Bread/Muffin/Muffin Dodge");
        pretzelDodge = RuntimeManager.CreateInstance("event:/SFX/Bread/Pretzel/Pretzel Dodge");
        sandwichDodge = RuntimeManager.CreateInstance("event:/SFX/Bread/Sandwich/Sandwich Dodge");

        baguetteHeal = RuntimeManager.CreateInstance("event:/SFX/Bread/Baguette/Baguette Heal");
        muffinHeal = RuntimeManager.CreateInstance("event:/SFX/Bread/Muffin/Muffin Heal");
        pretzelHeal = RuntimeManager.CreateInstance("event:/SFX/Bread/Pretzel/Pretzel Heal");
        sandwichHeal = RuntimeManager.CreateInstance("event:/SFX/Bread/Sandwich/Sandwich Heal");

        baguetteMainAttack = RuntimeManager.CreateInstance("event:/SFX/Bread/Baguette/Baguette Main");
        muffinMainAttack = RuntimeManager.CreateInstance("event:/SFX/Bread/Muffin/Muffin Main");
        pretzelMainAttack = RuntimeManager.CreateInstance("event:/SFX/Bread/Pretzel/Pretzel Main");
        sandwichMainAttack = RuntimeManager.CreateInstance("event:/SFX/Bread/Sandwich/Sandwich Main");

        baguetteHeavyAttack = RuntimeManager.CreateInstance("event:/SFX/Bread/Baguette/Baguette Heavy");
        muffinHeavyAttack = RuntimeManager.CreateInstance("event:/SFX/Bread/Muffin/Muffin Heavy");
        pretzelHeavyAttack = RuntimeManager.CreateInstance("event:/SFX/Bread/Pretzel/Pretzel Heavy");
        sandwichHeavyAttack = RuntimeManager.CreateInstance("event:/SFX/Bread/Sandwich/Sandwich Heavy");

        baguetteSkillOne = RuntimeManager.CreateInstance("event:/SFX/Bread/Baguette/Baguette Skill 1");
        muffinSkillOne = RuntimeManager.CreateInstance("event:/SFX/Bread/Muffin/Muffin Skill 1");
        pretzelSkillOne = RuntimeManager.CreateInstance("event:/SFX/Bread/Pretzel/Pretzel Skill 1");
        sandwichSkillOne = RuntimeManager.CreateInstance("event:/SFX/Bread/Sandwich/Sandwich Skill 1");

        baguetteSkillTwo = RuntimeManager.CreateInstance("event:/SFX/Bread/Baguette/Baguette Skill 2");
        muffinSkillTwo = RuntimeManager.CreateInstance("event:/SFX/Bread/Muffin/Muffin Skill 2");
        pretzelSkillTwo = RuntimeManager.CreateInstance("event:/SFX/Bread/Pretzel/Pretzel Skill 2");
        sandwichSkillTwo = RuntimeManager.CreateInstance("event:/SFX/Bread/Sandwich/Sandwich Skill 2");

        swanBlock = RuntimeManager.CreateInstance("event:/SFX/Swan/Swan Block");
        swanHealing = RuntimeManager.CreateInstance("event:/SFX/Swan/Swan Healing");
        swanHurt = RuntimeManager.CreateInstance("event:/SFX/Swan/Swan Hurt");
        swanMainHeavy = RuntimeManager.CreateInstance("event:/SFX/Swan/Swan Main Heavy");
        swanMainPeckAttack = RuntimeManager.CreateInstance("event:/SFX/Swan/Swan Main Peck Attack");
        swanSkillOne = RuntimeManager.CreateInstance("event:/SFX/Swan/Swan Skill 1");
        swanSkillTwo = RuntimeManager.CreateInstance("event:/SFX/Swan/Swan Skill 2");
        swanUseItem = RuntimeManager.CreateInstance("event:/SFX/Swan/Use Item");
    }

}
