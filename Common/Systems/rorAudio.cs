using Terraria.Audio;
using Terraria.ModLoader;

namespace RealmOne.Common.Systems;

public class rorAudio : ModSystem
{
    public static readonly SoundStyle SFX_Scroll;

    public static readonly SoundStyle ElectricPulse;

    public static readonly SoundStyle SFX_BoomGun;

    public static readonly SoundStyle SFX_MetalSwing;

    public static readonly SoundStyle SFX_MetalClash;

    public static readonly SoundStyle SFX_ElectricDeath;

    public static readonly SoundStyle SFX_Porce;

    public static readonly SoundStyle SFX_Sonar;

    public static readonly SoundStyle SFX_Toast;

    public static readonly SoundStyle aughhhh;

    public static readonly SoundStyle Scroll;

    public static readonly SoundStyle SFX_Cigarette;

    public static readonly SoundStyle SFX_CrossbowShot;

    public static readonly SoundStyle SFX_CrossbowImpact;

    public static readonly SoundStyle SFX_CrossbowHit;

    public static readonly SoundStyle SFX_FlyingKnife;

    public static readonly SoundStyle SquirmoSummonSound;

    public static readonly SoundStyle blunderbussShot;

    public static readonly SoundStyle SFX_CrossbowLoad;

    public static readonly SoundStyle SFX_PumpShotgun;

    public static readonly SoundStyle OldGoldTink;

    public static readonly SoundStyle SFX_OldGoldBeam;

    public static readonly SoundStyle OldGoldChainSound;

    public static readonly SoundStyle SFX_Cord;

    public static readonly SoundStyle ModMenuClick;

    public static readonly SoundStyle SquirmoDiggingMiddle;

    public static readonly SoundStyle SquirmoMudBubbleBlow;

    public static readonly SoundStyle SquirmoMudBubblePop;

    public static readonly SoundStyle SFX_Cologne;

    public static readonly SoundStyle SFX_CologneClink;

    public static readonly SoundStyle SawbladeRev;

    public static readonly SoundStyle SFX_Shuriken;

    public static readonly SoundStyle TwinkleBell;

    public static readonly SoundStyle SFX_Acorn;

    public static readonly SoundStyle SFX_GrenadeThrow;

    public static readonly SoundStyle SFX_GrenadeRoll;

    public static readonly SoundStyle VINEBOOM;

    public static readonly SoundStyle MarbleTink;

    public static readonly SoundStyle SFX_PurpleShot;

    public static readonly SoundStyle SFX_TheDreamer;

    public static readonly SoundStyle LightbulbShine;

    public static readonly SoundStyle SFX_BottleSmash;

    public static readonly SoundStyle SFX_Harp;

    public static readonly SoundStyle PeaSilencerShot;

    public static readonly SoundStyle PeaSilencerReload;

    public static readonly SoundStyle MoraleShot;

    public static readonly SoundStyle Damn;

    public static readonly SoundStyle LeechHeartEat;

    public static readonly SoundStyle PulsaPickup;

    public static readonly SoundStyle BreatheFlame;

    public static readonly SoundStyle GemBladeHit;

    public static readonly SoundStyle GemBladeSwing;

    public static readonly SoundStyle GemBladeAltSwing;

    public static readonly SoundStyle TommyGunShot;

    public static readonly SoundStyle DrinkingFire;

    public static readonly SoundStyle Proton;

    public static readonly SoundStyle BrokenBarrel;

    public static readonly SoundStyle WheelgunSound;

    public static readonly SoundStyle BIJOU;

    public static readonly SoundStyle TheIdol;

    public static readonly SoundStyle MagpieCalling;

    public static readonly SoundStyle BulbShatter;

    public static readonly SoundStyle PorcelainDamaged;

    public static readonly SoundStyle GoreGun;

    public static readonly SoundStyle squeak;

    public static readonly SoundStyle Rat;


    static rorAudio()
    {
        SFX_MetalSwing = new SoundStyle("RealmOne/Assets/Soundss/SFX_MetalSwing", (SoundType)0);
        SFX_Scroll = new SoundStyle("RealmOne/Assets/Soundss/SFX_Scroll", (SoundType)0);
        ElectricPulse = new SoundStyle("RealmOne/Assets/Soundss/ElectricPulse", (SoundType)0);
        SFX_BoomGun = new SoundStyle("RealmOne/Assets/Soundss/SFX_BoomGun", (SoundType)0);
        SFX_MetalClash = new SoundStyle("RealmOne/Assets/Soundss/SFX_MetalClash", (SoundType)0);
        SFX_ElectricDeath = new SoundStyle("RealmOne/Assets/Soundss/SFX_ElectricDeath", (SoundType)0);
        SFX_Porce = new SoundStyle("RealmOne/Assets/Soundss/SFX_Porce", (SoundType)0);
        SFX_Sonar = new SoundStyle("RealmOne/Assets/Soundss/SFX_Sonar", (SoundType)0);
        SFX_Toast = new SoundStyle("RealmOne/Assets/Soundss/SFX_Toast", (SoundType)0);
        aughhhh = new SoundStyle("RealmOne/Assets/Soundss/aughhhh", (SoundType)0);
        SFX_Cigarette = new SoundStyle("RealmOne/Assets/Soundss/SFX_Cigarette", (SoundType)0);
        SFX_CrossbowShot = new SoundStyle("RealmOne/Assets/Soundss/SFX_CrossbowShot", (SoundType)0);
        SFX_CrossbowImpact = new SoundStyle("RealmOne/Assets/Soundss/SFX_CrossbowImpact", (SoundType)0);
        SFX_CrossbowHit = new SoundStyle("RealmOne/Assets/Soundss/SFX_CrossbowHit", (SoundType)0);
        SFX_FlyingKnife = new SoundStyle("RealmOne/Assets/Soundss/SFX_FlyingKnife", (SoundType)0);
        SquirmoSummonSound = new SoundStyle("RealmOne/Assets/Soundss/SquirmoSummonSound", (SoundType)0);
        blunderbussShot = new SoundStyle("RealmOne/Assets/Soundss/blunderbussShot", (SoundType)0);
        SFX_CrossbowLoad = new SoundStyle("RealmOne/Assets/Soundss/SFX_CrossbowLoad", (SoundType)0);
        SFX_PumpShotgun = new SoundStyle("RealmOne/Assets/Soundss/SFX_PumpShotgun", (SoundType)0);
        OldGoldTink = new SoundStyle("RealmOne/Assets/Soundss/OldGoldTink", (SoundType)0);
        SFX_OldGoldBeam = new SoundStyle("RealmOne/Assets/Soundss/SFX_OldGoldBeam", (SoundType)0);
        OldGoldChainSound = new SoundStyle("RealmOne/Assets/Soundss/OldGoldChainSound", (SoundType)0);
        SFX_Cord = new SoundStyle("RealmOne/Assets/Soundss/SFX_Cord", (SoundType)0);
        ModMenuClick = new SoundStyle("RealmOne/Assets/Soundss/ModMenuClick", (SoundType)0);
        SquirmoDiggingMiddle = new SoundStyle("RealmOne/Assets/Soundss/SquirmoDiggingMiddle", (SoundType)0);
        SquirmoMudBubbleBlow = new SoundStyle("RealmOne/Assets/Soundss/SquirmoMudBubbleBlow", (SoundType)0);
        SquirmoMudBubblePop = new SoundStyle("RealmOne/Assets/Soundss/SquirmoMudBubblePop", (SoundType)0);
        SFX_Cologne = new SoundStyle("RealmOne/Assets/Soundss/SFX_Cologne", (SoundType)0);
        SFX_CologneClink = new SoundStyle("RealmOne/Assets/Soundss/SFX_CologneClink", (SoundType)0);
        SawbladeRev = new SoundStyle("RealmOne/Assets/Soundss/SawbladeRev", (SoundType)0);
        SFX_Shuriken = new SoundStyle("RealmOne/Assets/Soundss/SFX_Shuriken", (SoundType)0);
        TwinkleBell = new SoundStyle("RealmOne/Assets/Soundss/TwinkleBell", (SoundType)0);
        SFX_Acorn = new SoundStyle("RealmOne/Assets/Soundss/SFX_Acorn", (SoundType)0);
        SFX_GrenadeThrow = new SoundStyle("RealmOne/Assets/Soundss/SFX_GrenadeThrow", (SoundType)0);
        SFX_GrenadeRoll = new SoundStyle("RealmOne/Assets/Soundss/SFX_GrenadeRoll", (SoundType)0);
        VINEBOOM = new SoundStyle("RealmOne/Assets/Soundss/VINEBOOM", (SoundType)0);
        MarbleTink = new SoundStyle("RealmOne/Assets/Soundss/MarbleTink", (SoundType)0);
        SFX_PurpleShot = new SoundStyle("RealmOne/Assets/Soundss/SFX_PurpleShot", (SoundType)0);
        SFX_TheDreamer = new SoundStyle("RealmOne/Assets/Soundss/SFX_TheDreamer", (SoundType)0);
        LightbulbShine = new SoundStyle("RealmOne/Assets/Soundss/LightbulbShine", (SoundType)0);
        SFX_Harp = new SoundStyle("RealmOne/Assets/Soundss/SFX_Harp", (SoundType)0);
        SFX_BottleSmash = new SoundStyle("RealmOne/Assets/Soundss/SFX_BottleSmash", (SoundType)0);
        PeaSilencerShot = new SoundStyle("RealmOne/Assets/Soundss/PeaSilencerShot", (SoundType)0);
        PeaSilencerReload = new SoundStyle("RealmOne/Assets/Soundss/PeaSilencerReload", (SoundType)0);
        MoraleShot = new SoundStyle("RealmOne/Assets/Soundss/MoraleShot", (SoundType)0);
        Damn = new SoundStyle("RealmOne/Assets/Soundss/Damn", (SoundType)0);
        LeechHeartEat = new SoundStyle("RealmOne/Assets/Soundss/LeechHeartEat", (SoundType)0);
        PulsaPickup = new SoundStyle("RealmOne/Assets/Soundss/PulsaPickup", (SoundType)0);
        BreatheFlame = new SoundStyle("RealmOne/Assets/Soundss/BreatheFlame", (SoundType)0);
        GemBladeHit = new SoundStyle("RealmOne/Assets/Soundss/GemBladeHit", (SoundType)0);
        GemBladeSwing = new SoundStyle("RealmOne/Assets/Soundss/GemBladeSwing", (SoundType)0);
        GemBladeAltSwing = new SoundStyle("RealmOne/Assets/Soundss/GemBladeAltSwing", (SoundType)0);
        TommyGunShot = new SoundStyle("RealmOne/Assets/Soundss/TommyGunShot", (SoundType)0);
        DrinkingFire = new SoundStyle("RealmOne/Assets/Soundss/DrinkingFire", (SoundType)0);
        Proton = new SoundStyle("RealmOne/Assets/Soundss/Proton", (SoundType)0);
        BrokenBarrel = new SoundStyle("RealmOne/Assets/Soundss/BrokenBarrel", (SoundType)0);
        WheelgunSound = new SoundStyle("RealmOne/Assets/Soundss/WheelgunSound", (SoundType)0);
        BIJOU = new SoundStyle("RealmOne/Assets/Soundss/BIJOU", (SoundType)0);
        TheIdol = new SoundStyle("RealmOne/Assets/Soundss/TheIdol", (SoundType)0);
        MagpieCalling = new SoundStyle("RealmOne/Assets/Soundss/MagpieCalling", (SoundType)0);
        BulbShatter = new SoundStyle("RealmOne/Assets/Soundss/BulbShatter", (SoundType)0);
        PorcelainDamaged = new SoundStyle("RealmOne/Assets/Soundss/porcelainbreak", (SoundType)0);
        GoreGun = new SoundStyle("RealmOne/Assets/Soundss/GoreGun", (SoundType)0);
        squeak = new SoundStyle("RealmOne/Assets/Soundss/squeak", (SoundType)0);
        Rat = new SoundStyle("RealmOne/Assets/Soundss/Rat", (SoundType)0);

    }
}