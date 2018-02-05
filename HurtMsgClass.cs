using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fougerite;
using Fougerite.Events;
using UnityEngine;
using System.IO;

namespace HurtMsg
{
    public class HurtMsgClass : Fougerite.Module
    {
        public override string Name { get { return "HurtMsg"; } }
        public override string Author { get { return "Salva/Juli"; } }
        public override string Description { get { return "HurtMsg"; } }
        public override Version Version { get { return new Version("1.0"); } }

        public string red = "[color #B40404]";
        public string blue = "[color #81F7F3]";
        public string green = "[color #82FA58]";
        public string yellow = "[color #F4FA58]";
        public string orange = "[color #FF8000]";
        public string pink = "[color #FA58F4]";
        public string white = "[color #FFFFFF]";

        public IniParser Settings;
        public static List<string> weapons = new List<string>();

        public override void Initialize()
        {
            Hooks.OnServerInit += OnServerInit;
            Hooks.OnCommand += OnCommand;
            Hooks.OnEntityHurt += OnEntityHurt;
        }
        public override void DeInitialize()
        {
            Hooks.OnServerInit -= OnServerInit;
            Hooks.OnCommand -= OnCommand;
            Hooks.OnEntityHurt -= OnEntityHurt;
        }
        public void OnServerInit()
        {
            ReloadConfig();
        }
        private void ReloadConfig()
        {
            weapons.Clear();
            if (!File.Exists(Path.Combine(ModuleFolder, "Settings.ini")))
            {
                File.Create(Path.Combine(ModuleFolder, "Settings.ini")).Dispose();
                Settings = new IniParser(Path.Combine(ModuleFolder, "Settings.ini"));

                Settings.AddSetting("WeaponsCanShowDamage", "Rock", "true");
                Settings.AddSetting("WeaponsCanShowDamage", "Stone Hatchet", "true");
                Settings.AddSetting("WeaponsCanShowDamage", "Hatchet", "true");
                Settings.AddSetting("WeaponsCanShowDamage", "Pick Axe", "true");
                Settings.AddSetting("WeaponsCanShowDamage", "Hunting Bow", "true");
                Settings.AddSetting("WeaponsCanShowDamage", "Uber Hunting Bow", "true");
                Settings.AddSetting("WeaponsCanShowDamage", "Revolver", "true");
                Settings.AddSetting("WeaponsCanShowDamage", "HandCannon", "false");
                Settings.AddSetting("WeaponsCanShowDamage", "Pipe Shotgun", "false");
                Settings.AddSetting("WeaponsCanShowDamage", "9mm Pistol", "true");
                Settings.AddSetting("WeaponsCanShowDamage", "P250", "true");
                Settings.AddSetting("WeaponsCanShowDamage", "MP5A4", "false");
                Settings.AddSetting("WeaponsCanShowDamage", "Shotgun", "false");
                Settings.AddSetting("WeaponsCanShowDamage", "M4", "false");
                Settings.AddSetting("WeaponsCanShowDamage", "Bolt Action Rifle", "true");
                Settings.AddSetting("WeaponsCanShowDamage", "F1 Grenade", "false");
                Settings.AddSetting("WeaponsCanShowDamage", "Explosive Charge", "true");
                Logger.Log(Name + " Plugin: New Settings File Created!");
                Settings.Save();
                ReloadConfig();
            }
            else
            {
                Settings = new IniParser(Path.Combine(ModuleFolder, "Settings.ini"));
                if (Settings.ContainsSetting("WeaponsCanShowDamage", "Rock") &&
                    Settings.ContainsSetting("WeaponsCanShowDamage", "Stone Hatchet") &&
                    Settings.ContainsSetting("WeaponsCanShowDamage", "Hatchet") &&
                    Settings.ContainsSetting("WeaponsCanShowDamage", "Pick Axe") &&
                    Settings.ContainsSetting("WeaponsCanShowDamage", "Hunting Bow") &&
                    Settings.ContainsSetting("WeaponsCanShowDamage", "Uber Hunting Bow") &&
                    Settings.ContainsSetting("WeaponsCanShowDamage", "Revolver") &&
                    Settings.ContainsSetting("WeaponsCanShowDamage", "HandCannon") &&
                    Settings.ContainsSetting("WeaponsCanShowDamage", "Pipe Shotgun") &&
                    Settings.ContainsSetting("WeaponsCanShowDamage", "9mm Pistol") &&
                    Settings.ContainsSetting("WeaponsCanShowDamage", "P250") &&
                    Settings.ContainsSetting("WeaponsCanShowDamage", "MP5A4") &&
                    Settings.ContainsSetting("WeaponsCanShowDamage", "Shotgun") &&
                    Settings.ContainsSetting("WeaponsCanShowDamage", "M4") &&
                    Settings.ContainsSetting("WeaponsCanShowDamage", "Bolt Action Rifle") &&
                    Settings.ContainsSetting("WeaponsCanShowDamage", "F1 Grenade") &&
                    Settings.ContainsSetting("WeaponsCanShowDamage", "Explosive Charge"))
                {
                    try
                    {
                        if (Settings.GetBoolSetting("WeaponsCanShowDamage", "Rock")) { weapons.Add("Rock"); }
                        if (Settings.GetBoolSetting("WeaponsCanShowDamage", "Stone Hatchet")) { weapons.Add("Stone Hatchet"); }
                        if (Settings.GetBoolSetting("WeaponsCanShowDamage", "Hatchet")) { weapons.Add("Hatchet"); }
                        if (Settings.GetBoolSetting("WeaponsCanShowDamage", "Pick Axe")) { weapons.Add("Pick Axe"); }
                        if (Settings.GetBoolSetting("WeaponsCanShowDamage", "Hunting Bow")) { weapons.Add("Hunting Bow"); }
                        if (Settings.GetBoolSetting("WeaponsCanShowDamage", "Uber Hunting Bow")) { weapons.Add("Uber Hunting Bow"); }
                        if (Settings.GetBoolSetting("WeaponsCanShowDamage", "Revolver")) { weapons.Add("Revolver"); }
                        if (Settings.GetBoolSetting("WeaponsCanShowDamage", "HandCannon")) { weapons.Add("HandCannon"); }
                        if (Settings.GetBoolSetting("WeaponsCanShowDamage", "Pipe Shotgun")) { weapons.Add("Pipe Shotgun"); }
                        if (Settings.GetBoolSetting("WeaponsCanShowDamage", "9mm Pistol")) { weapons.Add(""); }
                        if (Settings.GetBoolSetting("WeaponsCanShowDamage", "P250")) { weapons.Add("P250"); }
                        if (Settings.GetBoolSetting("WeaponsCanShowDamage", "MP5A4")) { weapons.Add("MP5A4"); }
                        if (Settings.GetBoolSetting("WeaponsCanShowDamage", "Shotgun")) { weapons.Add("Shotgun"); }
                        if (Settings.GetBoolSetting("WeaponsCanShowDamage", "M4")) { weapons.Add("M4"); }
                        if (Settings.GetBoolSetting("WeaponsCanShowDamage", "Bolt Action Rifle")) { weapons.Add("Bolt Action Rifle"); }
                        if (Settings.GetBoolSetting("WeaponsCanShowDamage", "F1 Grenade")) { weapons.Add("F1 Grenade"); }
                        if (Settings.GetBoolSetting("WeaponsCanShowDamage", "Explosive Charge")) { weapons.Add("Explosive Charge"); }
                        Logger.Log(Name + " Plugin: Settings File Loaded!");
                    }
                    catch (Exception ex)
                    {
                        Logger.LogError(Name + " Plugin: Detected a problem in the configuration");
                        Logger.Log("ERROR -->" + ex.Message);
                        File.Delete(Path.Combine(ModuleFolder, "Settings.ini"));
                        Logger.LogError(Name + " Plugin: Deleted the old configuration file");
                        ReloadConfig();
                    }
                }
                else
                {
                    Logger.LogError(Name + " Plugin: Detected a problem in the configuration (lost key)");
                    File.Delete(Path.Combine(ModuleFolder, "Settings.ini"));
                    Logger.LogError(Name + " Plugin: Deleted the old configuration file");
                    ReloadConfig();
                }
                return;
            }
        }
        public void OnCommand(Fougerite.Player player, string cmd, string[] args)
        {
            if (!player.Admin) { return; }
            if (cmd == "hurtmsg")
            {
                if (args.Length == 0)
                {
                    player.MessageFrom(Name, "/hurtmsg " + green + " List of commands");
                    player.MessageFrom(Name, "/hurtmsg reload " + green + " Reload and apply the Settings");
                }
                else
                {
                    if (args[0] == "reload")
                    {
                        ReloadConfig();
                        player.MessageFrom(Name, "Settings has been Reloaded :)");
                    }
                }
            }
        }
        public void OnEntityHurt(HurtEvent he)
        {
            if (weapons.Contains(he.WeaponName))
            {
                Fougerite.Player pl = (Fougerite.Player)he.Attacker;
                int h = Convert.ToInt32(he.Entity.Health);
                int d = Convert.ToInt32(he.DamageAmount);
                pl.InventoryNotice(he.Entity.Name + " " + (h - d));
            }
        }
    }
}
