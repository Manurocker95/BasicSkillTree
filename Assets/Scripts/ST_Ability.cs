/*====================================================================*
 *                                                                    *
 *       Basic Skill Tree by Manuel Rodriguez Matesanz                *     
 *                       25/10/2017                                   *      
 *                                                                    *
 * ===================================================================*/

using System;
using System.Collections.Generic;
using SimpleJSON;

/// <summary>
/// Skill Tree Ability
/// </summary>
[Serializable]
public class ST_Ability : UnityEngine.Object
{
    /// <summary>
    /// ID for usable list in ability tree. Can't be repeated
    /// </summary>
    public string ID;
    /// <summary>
    /// Description for the ability
    /// </summary>
    public string Description;
    /// <summary>
    /// Name of the ability
    /// </summary>
    public string Name;
    /// <summary>
    /// Target for the ability: OneEnemy, AllEnemies, OneAlly, AllAllies, EnemyAndChara, AllEnemiesAndAllAlly, EnemyAndAlly, Chara
    /// </summary>
    public string Target;
    /// <summary>
    /// Type: Physical, Special, Status
    /// </summary>
    public string Type;
    /// <summary>
    /// Damage that the ability does
    /// </summary>
    public int Damage;
    /// <summary>
    /// Restore points that the ability does
    /// </summary>
    public int Restore;
    /// <summary>
    /// Status modification
    /// </summary>
    public string Status_effect;
    /// <summary>
    /// Element: Fire, Ice, Water...
    /// </summary>
    public string Magic_Type;
    /// <summary>
    /// Level that the player need to have to be able to use this
    /// </summary>
    public int LevelToUnlock;
    /// <summary>
    /// List of evolutions
    /// </summary>
    public List<ST_Ability> evolutions;

    public ST_Ability(JSONNode data)
    {
        ID = data["ID"];
        Description = data["Description"];
        Name = data["Name"];
        Target = data["Target"];
        Type = data["Type"];
        Damage = data["Damage"].AsInt;
        Restore = data["Restore"].AsInt;
        Status_effect = data["Status_effect"];
        Magic_Type = data["Magic"];
        LevelToUnlock = data["LevelToUnlock"].AsInt;

        evolutions = new List<ST_Ability>();

        JSONArray abilities_evol = data["Evolutions"].AsArray;

        if (abilities_evol != null)
        {
            for (int i = 0; i < abilities_evol.Count; i++)
            {
                evolutions.Add(new ST_Ability(abilities_evol[i]));
            }
        }
    }
}
