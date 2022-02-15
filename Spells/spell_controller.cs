using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spell_controller : MonoBehaviour
{
    public Transform firePoint;
    public spell curSpell;
    public spell[] spellList;
    List<spell> activeSpells = new List<spell>();
    private int spellIndex = 0;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1")){
            activeSpells.Add(Cast());
        }
        if(Input.GetKey("q")){
            spellIndex = (spellIndex + 1) % spellList.Length;
            curSpell = spellList[spellIndex];
        }
        checkDead();
    }
    spell Cast(){
        spell projectile = Instantiate(curSpell, firePoint.position, firePoint.rotation) as spell;
        projectile.transform.parent = this.transform;
        return projectile;
    }
    void checkDead(){
        for(int i = 0; i < activeSpells.Count; i++){
            spell spell = activeSpells[i];
            if (spell.dead){
                DestroyImmediate(spell.gameObject);
                activeSpells.Remove(spell);
            }
        }
    }
}
