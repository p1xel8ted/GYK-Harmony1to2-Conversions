// Decompiled with JetBrains decompiler
// Type: ZombiePlus.Options
// Assembly: ZombiePlus, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BD1FC9C3-F067-4EFB-8476-BCA6458A2629
// Assembly location: E:\Downloads\IDM-Chrome\ZombiePlus-35-0-0-1-1636472842_2\ZombiePlus\ZombiePlus.dll

namespace ZombiePlus
{
  public class Options
  {
    public Options()
    {
      Zombie_MovementSpeed = 2.55f;
      Zombie_BaseEfficiency = 16f;
      Zombie_MaxEfficiency = 100f;
      Craft_ZombieFarm_ProduceWaste_Enabled = true;
      Craft_ZombieFarm_ProduceWaste_Max = 8;
      Craft_ZombieFarm_SeedsNeed_Enabled = true;
      Craft_ZombieFarm_Garden_Needs_Value = 22;
      Craft_ZombieFarm_Vineyard_Needs_Value = 11;
      Craft_ZombieFarm_ProduceWaste_Chance = 0.7f;
      InGame_ReloadConfig_Key = "l";
      InGame_ReloadConfig_Rerun = true;
    }

    public float Zombie_MovementSpeed { get; set; } = 2.55f;

    public float Zombie_BaseEfficiency { get; set; } = 16f;

    public float Zombie_ExtraEfficiency { get; set; }

    public float Zombie_MaxEfficiency { get; set; } = 100f;

    public bool Craft_ZombieFarm_ProduceWaste_Enabled { get; set; } = true;

    public int Craft_ZombieFarm_ProduceWaste_Min { get; set; }

    public int Craft_ZombieFarm_ProduceWaste_Max { get; set; } = 8;

    public bool Craft_ZombieFarm_SeedsNeed_Enabled { get; set; } = true;

    public int Craft_ZombieFarm_Garden_Needs_Value { get; set; } = 22;

    public int Craft_ZombieFarm_Vineyard_Needs_Value { get; set; } = 11;

    public float Craft_ZombieFarm_ProduceWaste_Chance { get; set; } = 0.7f;

    public string InGame_ReloadConfig_Key { get; set; } = "l";

    public bool InGame_ReloadConfig_Rerun { get; set; } = true;
  }
}
