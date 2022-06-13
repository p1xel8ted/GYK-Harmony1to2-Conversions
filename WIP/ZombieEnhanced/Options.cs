// Decompiled with JetBrains decompiler
// Type: ZombieEnhanced.Options
// Assembly: ZombieEnhanced, Version=2.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4022B546-E585-46CC-BE5E-ED1509FBE325
// Assembly location: E:\Downloads\IDM-Chrome\ZombieEnhanced-24-2-2-0-1612137970\ZombieEnhanced.dll

namespace ZombieEnhanced
{
  public class Options
  {
    public float Zombie_MovementSpeed { get; set; } = 2.55f;

    public float Zombie_BaseEfficiency { get; set; } = 16f;

    public float Zombie_ExtraEfficiency { get; set; }

    public float Zombie_MaxEfficiency { get; set; } = 100f;

    public bool Prayer_Reset_Enabled { get; set; } = true;

    public string Prayer_Key { get; set; } = "p";

    public bool Buffs_Activator_Enabled { get; set; } = true;

    public string Buffs_Activator_Key { get; set; } = "o";

    public string Buffs_Activator_Remover_Key { get; set; }

    public string Buffs_Activator_Selected { get; set; } = "buff_skull";

    public string Buffs_Activator_Title { get; set; } = "Difficult corpses";

    public bool Debug_Enabled { get; set; }

    public string Debug_Log_PropName { get; set; } = "Buffs";

    public string InGame_ReloadConfig_Key { get; set; } = "l";

    public bool InGame_ReloadConfig_Rerun { get; set; } = true;

    public string Item_Spawner_Id { get; set; } = "alchemy_2_d_green";

    public int Item_Spawner_Qty { get; set; } = 1;

    public string Item_Spawner_Key { get; set; } = "j";

    public string Item_Spawner_Package_Key { get; set; } = "b";

    public bool Item_Organs_Overhaul_Enabled { get; set; } = true;

    public int Item_Organs_Overhaul_StackCount { get; set; } = 10;

    public int Item_Organs_Additional_Value { get; set; } = 1;

    public bool Item_Embalm_Overhaul_Enabled { get; set; } = true;

    public int Item_Embalm_Additional_Value { get; set; } = 1;

    public bool Craft_ZombieFarm_ProduceWaste_Enabled { get; set; } = true;

    public int Craft_ZombieFarm_ProduceWaste_Min { get; set; }

    public int Craft_ZombieFarm_ProduceWaste_Max { get; set; } = 8;

    public bool Craft_ZombieFarm_SeedsNeed_Enabled { get; set; } = true;

    public int Craft_ZombieFarm_Garden_Needs_Value { get; set; } = 22;

    public int Craft_ZombieFarm_Vineyard_Needs_Value { get; set; } = 11;

    public float Craft_ZombieFarm_ProduceWaste_Chance { get; set; } = 0.7f;

    public bool Zombie_Dialogue_Enabled { get; set; } = true;

    public float Zombie_Dialogue_Chances { get; set; } = 25f;

    public float Zombie_Dialogue_WaitSec { get; set; } = 3f;

    public ZEVoiceID Zombie_Dialogue_Voice { get; set; } = ZEVoiceID.Gunter;

    public bool Tooltip_Enabled { get; set; } = true;

    public Mode Config_Preset { get; set; } = Mode.Balance;
  }
}
