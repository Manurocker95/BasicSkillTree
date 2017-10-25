/*====================================================================*
 *                                                                    *
 *       Basic Skill Tree by Manuel Rodriguez Matesanz                *     
 *                       25/10/2017                                   *      
 *                                                                    *
 * ===================================================================*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Skill Tree Character
/// </summary>
public class ST_Character : MonoBehaviour
{
    /// <summary>
    /// Current level of the character
    /// </summary>
    [SerializeField] private int m_currentLevel = 1;
    /// <summary>
    /// Our ability tree. Serialized for debug.
    /// </summary>
    [SerializeField] private ST_AbilityTree m_AbilityTree;
    /// <summary>
    /// Json name. Placed in Resources/path
    /// </summary>
    [SerializeField] private string AbilityJSON = "Data/abilities";
    /// <summary>
    /// Property for accessing current level
    /// </summary>
    public int CurrentLevel { get { return m_currentLevel; } set { m_currentLevel = value; } }

	// Use this for initialization
	void Start ()
    {
	    if (m_AbilityTree == null)
        {
            m_AbilityTree = GetComponent<ST_AbilityTree>();
        }

        m_AbilityTree.PrepareAbilities(AbilityJSON);
	}
	
    void LevelUp()
    {
        m_currentLevel++;
        Debug.Log("Level up to level " + m_currentLevel + "!");
        m_AbilityTree.SetAbilitiesByPlayerLevel();
    }

	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            LevelUp();
        }
	}
}
