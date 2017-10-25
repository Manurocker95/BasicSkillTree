/*====================================================================*
 *                                                                    *
 *       Basic Skill Tree by Manuel Rodriguez Matesanz                *     
 *                       25/10/2017                                   *      
 *                                                                    *
 * ===================================================================*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.ComponentModel;
using SimpleJSON;

public class ST_AbilityTree : MonoBehaviour
{
    /// <summary>
    /// Usable abilities by the player
    /// </summary>
    private Dictionary<string, ST_Ability> m_myAbilities;
    /// <summary>
    /// Reference to the character that uses those abilities
    /// </summary>
    [SerializeField] private ST_Character m_character;

    private void Awake()
    {
        m_myAbilities = new Dictionary<string, ST_Ability>();
    }

    // Use this for initialization
    void Start ()
    {
       

        if (m_character == null)
            m_character = GetComponent<ST_Character>();
    }
	/// <summary>
    /// We read abilities from JSON and save them in our dictionary
    /// </summary>
    /// <param name="json_path"></param>
    public void PrepareAbilities(string json_path)
    {
        JSONNode info = JSON.Parse(Resources.Load(json_path).ToString());

        Debug.Log("Total: " + info["skilltree"].Count);

        for (int i = 0; i < info["skilltree"].Count; i++)
        {
            ST_Ability ability = new ST_Ability(info["skilltree"][i]);

            Debug.Log("Parsing: " + ability.ID);

            // We have that ability
            if (m_character.CurrentLevel >= ability.LevelToUnlock)
            {
                Debug.Log("Learned " + ability.ID + " cause it's level to unlock is " + ability.LevelToUnlock);

                m_myAbilities.Add(ability.ID, ability);

                //Each ability checks it's evolutions
                for (int j = 0; j < ability.evolutions.Count; j++)
                {
                    if (m_character.CurrentLevel >= ability.evolutions[j].LevelToUnlock)
                    {
                        m_myAbilities.Add(ability.evolutions[j].ID, ability);
                        Debug.Log("Learned " + ability.evolutions[j].ID);
                    }
                }
            }
        }

        Debug.Log("Total abilities learned at level "+m_character.CurrentLevel+": " + m_myAbilities.Count);
    }

    public void SetAbilitiesByPlayerLevel()
    {
        if (m_myAbilities == null)
            Debug.Log("There are no abilities");

        List<ST_Ability> abilities_to_learn = new List<ST_Ability>();

        foreach (string key in m_myAbilities.Keys)
        { 
            ST_Ability ab = m_myAbilities[key];
            Debug.Log("Checking evolutions from: " +ab.ID);

            //Each ability checks it's evolutions
            for (int j = 0; j < ab.evolutions.Count; j++)
            {
                if (m_character.CurrentLevel >= ab.evolutions[j].LevelToUnlock && !m_myAbilities.ContainsKey(ab.evolutions[j].ID))
                {
                    abilities_to_learn.Add(ab.evolutions[j]);
                    Debug.Log("Learned " + ab.evolutions[j].ID);
                }
                else
                {
                    Debug.Log("We can't learn " + ab.evolutions[j].ID);
                }
            }
        }

        for (int i = 0; i < abilities_to_learn.Count; i++)
        {
            m_myAbilities.Add(abilities_to_learn[i].ID, abilities_to_learn[i]);
        }

        Debug.Log("Total abilities learned at level " + m_character.CurrentLevel + ": " + m_myAbilities.Count);
    }

    public ST_Ability getAbilityByKey(string key)
    {
        return m_myAbilities[key];
    }

	// Update is called once per frame
	void Update ()
    {
		
	}
}
