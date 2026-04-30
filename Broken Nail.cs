using BrokenNail;
using GlobalEnums;
using MenuChanger;
using Modding;
using Steamworks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using UnityEngine;
using UObject = UnityEngine.Object;
using ItemChanger.Internal;
//using Broken_Nail.GlobalSettingsClass;

//namespace Broken_Nail
//{
public class Broken_Nail : Mod, ILocalSettings<GlobalSettingsClass>
{
    public static GlobalSettingsClass saveSettings { get; set; } = new GlobalSettingsClass();
    public void OnLoadLocal(GlobalSettingsClass s) => saveSettings = s;
    public GlobalSettingsClass OnSaveLocal() => saveSettings;

    internal static Broken_Nail Instance;

    public static SpriteManager SpriteManager { get; private set; }

    public override string GetVersion() => "1.0.0.0";

    public static Assembly Assembly { get; } = typeof(Broken_Nail).Assembly;

//internal bool BrokenNailMode = false;

//public override List<ValueTuple<string, string>> GetPreloadNames()
//{
//    return new List<ValueTuple<string, string>>
//    {
//        new ValueTuple<string, string>("White_Palace_18", "White Palace Fly")
//    };
//}

    public Broken_Nail() : base("Broken Nail")
    {
        Instance = this;
    }

    public override void Initialize(Dictionary<string, Dictionary<string, GameObject>> preloadedObjects)
    {
        Log("Initializing");

        Instance = this;

        ModeMenu.AddMode(new BrokenNailMenuConstructor());

        SpriteManager = new(Assembly, "Broken_Nail.Resources.");

        On.HeroController.Attack += ForceNailDamage;

        Log("Initialized");
    }

    private void ForceNailDamage(On.HeroController.orig_Attack orig, HeroController self, AttackDirection attackDir)
    {
        if (saveSettings.BrokenNailMode)
        {
            int num = 1;
            if (PlayerData.instance.nailDamage != 1)
            {
                PlayerData.instance.nailDamage = num;
                PlayMakerFSM.BroadcastEvent("UPDATE NAIL DAMAGE");
                Instance.Log("Forced nail damage to 1");
            }
        }

        orig(self, attackDir);
    }
}