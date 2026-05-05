using System.Collections;
using TMPro;
using Unity.Hierarchy;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum TurnState { PLAYERTURN, ENEMYTURN }//checks if it is the players turn or the enemies turn
public enum VictoryState { WON, LOST}//checks if the player won, lost, or drew the battle
public enum StatusState { ACTIVE, INACTIVE }//checks if the status is active or inactive

public class BattleSystem : MonoBehaviour
{
    public static BattleSystem Instance4 { get; private set; }

    public GameObject playerPrefab; // spawns the player prefab in the battle scene
    public GameObject enemyPrefab; // spawns the enemy prefab in the battle scene
    public TextMeshProUGUI textMeshPro;

    public Transform playerBattleStation; //where the player prefab will be spawned in the battle scene
    public Transform enemyBattleStation; //where the enemy prefab will be spawned in the battle scene
    public Transform buttonContainer; //where the buttons will be spawned in the battle scene
    public GameObject AttackbuttonPrefab; // prefab for the buttons to be spawned in the battle scene
    public GameObject AbilityButtonPrefab; // prefab for the buttons to be spawned in the battle scene
    public Transform abilityCuttonContainer;//where ability buttons go

    Unit playerUnit;// reference to the Unit script attached to the player prefab
    Unit enemyUnit;// reference to the Unit script attached to the enemy prefab

    public BattleHudd playerHUD;// reference to the BattleHudd script to update the UI elements for the player
    public BattleHudd enemyHUD;// reference to the BattleHudd script to update the UI elements for the enemy

    public TurnState turnState; // lets us check if it is the player's turn or the enemy's turn
    public StatusState statusState; // lets us check if the status is active or inactive
    public VictoryState victoryState; // lets us check if the player won, lost, or drew the battle

    // Start is called before the first frame update
    public void Start()
    {
        StartCoroutine(Battle());
    }

    //start the battle
    public IEnumerator Battle()
    {
        //spawn character prefabs
        GameObject playerGo = Instantiate(playerPrefab, playerBattleStation);
        playerUnit = playerGo.GetComponent<Unit>();
        GameObject enemyGo = Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemyGo.GetComponent<Unit>();
        //assign hud to characters
        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);
        yield return new WaitForSeconds(1f);

        //start the player's turn
        turnState = TurnState.PLAYERTURN;
        PlayerTurn();
        ShowButtons();

    }

        IEnumerator PlayerTalk()
    {
        return null;
    }

    //wait for the player to choose an action
    void PlayerTurn()
    {
        textMeshPro.SetText("choose action");
        ShowButtons();
        

    }

    IEnumerator PlayerAttack()
    {

        //reflects the attack back to the player if the enemy has reflect active
        if (enemyUnit.reflect > 0)
        {
            enemyUnit.reflect -= 1;
            textMeshPro.SetText(enemyUnit.unitName + " reflects the attack!");
            yield return new WaitForSeconds(1f);
            playerUnit.currentHP -= playerUnit.damage;
            playerHUD.SetHP(playerUnit.currentHP);

            textMeshPro.SetText(playerUnit.unitName + " takes damage from reflection!");
            playerHUD.GetComponent<Animator>().Play("Player bars attacked"); //plays attack animation on the player hud
            playerPrefab.GetComponent<Animator>().Play("Player attacked"); //plays attack animation on the player prefab
            playerPrefab.GetComponent<AudioSource>().PlayOneShot(playerPrefab.GetComponent<AudioSource>().clip);
            yield return new WaitForSeconds(1f);
        }
        else if (playerUnit.attackBuffTurns > 0)
        {  
            enemyUnit.currentHP -= playerUnit.damage;
            enemyHUD.SetHP(enemyUnit.currentHP);
            textMeshPro.SetText(playerUnit.unitName + " attacks with a buff!");
            enemyHUD.GetComponent<Animator>().Play("Enemy Hud attacked");//plays attack animation on the enemy hud
            enemyPrefab.GetComponent<Animator>().Play("enemy attacked");//plays attack animation on the enemy prefab
            enemyPrefab.GetComponent<AudioSource>().PlayOneShot(enemyPrefab.GetComponent<AudioSource>().clip);//plays sound when attacking
            playerUnit.attackBuffTurns -= 1;

            if (playerUnit.attackBuffTurns <= 0)
            {
                playerUnit.damage -= playerUnit.attackBuffValue;
                playerUnit.attackBuff = 0;
                textMeshPro.SetText(playerUnit.unitName + "'s attack buff wears off!");
            }
            yield return new WaitForSeconds(1f);
        }
        else //damages the enemy normally if there is no reflection or attack buff
        {
            enemyUnit.currentHP -= playerUnit.damage;
            enemyHUD.SetHP(enemyUnit.currentHP);
            textMeshPro.SetText(playerUnit.unitName + " attacks!");
            enemyHUD.GetComponent<Animator>().Play("Enemy Hud attacked");//plays attack animation on the enemy hud
            enemyPrefab.GetComponent<Animator>().Play("enemy attacked");//plays attack animation on the enemy prefab
            enemyPrefab.GetComponent<AudioSource>().PlayOneShot(enemyPrefab.GetComponent<AudioSource>().clip);//plays sound when attacking
            yield return new WaitForSeconds(1f);
        }
        bool isDead = enemyUnit.currentHP <= 0;

        yield return new WaitForSeconds(1f);

        //check if enemy is dead
        if (isDead)
        {
            //end battle
            victoryState = VictoryState.WON;
            EndBattle();
        }
        else
        {
            //apply bleeding damage if player is bleeding
            if (playerUnit.bleeding > 0)
            {
                playerHUD.GetComponent<Animator>().Play("Player bars attacked"); //plays attack animation on the player hud
                playerPrefab.GetComponent<Animator>().Play("Player attacked"); //plays attack animation on the player prefab
                playerUnit.currentHP -= 5;
                playerHUD.SetHP(playerUnit.currentHP);
                textMeshPro.SetText(playerUnit.unitName + " takes damage from bleeding!");
                playerPrefab.GetComponent<AudioSource>().PlayOneShot(playerPrefab.GetComponent<AudioSource>().clip);//plays sound when taking damage
            }
            playerUnit.bleeding -= 1;
            if (playerUnit.bleeding < 0)
            {
                playerUnit.bleeding = 0;
            }
             yield return new WaitForSeconds(1f);
            //enemy turn
            turnState = TurnState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
}
  
    public enum EnemyAttacks
    {
        Stab,
        Slash,
        Reflect,
    }

    IEnumerator EnemyTurn()
    {
        HideButtons();
        textMeshPro.SetText(enemyUnit.unitName + " attacks!");
        //enemy performs attack
        EnemyAttacks attack = (EnemyAttacks)Random.Range(0, System.Enum.GetValues(typeof(EnemyAttacks)).Length);
        //enemy stabs player and causes bleeding
        if (attack == EnemyAttacks.Stab)
        {
            textMeshPro.SetText(enemyUnit.unitName + " uses Stab!");
            yield return new WaitForSeconds(1f);
            enemyPrefab.GetComponent<Animator>().Play("enemy attack");//plays attack animation on the enemy prefab
            playerHUD.GetComponent<Animator>().Play("Player bars attacked"); //plays attack animation on the player hud
            playerPrefab.GetComponent<Animator>().Play("Player attacked"); //plays attack animation on the player prefab
            playerPrefab.GetComponent<AudioSource>().PlayOneShot(playerPrefab.GetComponent<AudioSource>().clip);//plays sound when attacking

            playerUnit.bleeding += 3;
            if (playerUnit.bleeding > 3)
            {               
                playerUnit.bleeding = 3; 
            }
                textMeshPro.SetText(playerUnit.unitName + " is bleeding now bleeding");
        
        }
        //enemy slashes player and causes damage
        else if (attack == EnemyAttacks.Slash)
        {
            if (playerUnit.defenseDuration > 0)
            {
                playerUnit.defenseDuration -= 1;
                textMeshPro.SetText(playerUnit.unitName + " blocks the attack!");
                playerPrefab.GetComponent<Animator>().Play("Player Blocked");
                playerPrefab.GetComponent<AudioSource>().PlayOneShot(playerPrefab.GetComponent<AudioSource>().clip);
                yield return new WaitForSeconds(1f);
            }
            else
            {
                playerUnit.currentHP -= enemyUnit.damage;
                playerHUD.SetHP(playerUnit.currentHP);
                enemyPrefab.GetComponent<Animator>().Play("enemy attack");
                textMeshPro.SetText(enemyUnit.unitName + " uses Slash!");
                enemyPrefab.GetComponent<Animator>().Play("enemy attack");//plays attack animation on the enemy prefab
                playerHUD.GetComponent<Animator>().Play("Player bars attacked"); //plays attack animation on the player hud
                playerPrefab.GetComponent<AudioSource>().PlayOneShot(playerPrefab.GetComponent<AudioSource>().clip);//plays sound when attacking
                yield return new WaitForSeconds(1f);
            }
        }
        //enemy reflects the next attack
        else if (attack == EnemyAttacks.Reflect)
        {
            enemyUnit.reflect += 1;
            if (enemyUnit.reflect > 1)
            {
                enemyUnit.reflect = 1;
            }
            textMeshPro.SetText(enemyUnit.unitName + " uses Reflect!");
            enemyPrefab.GetComponent<Animator>().Play("enemy reflect");
            playerPrefab.GetComponent<AudioSource>().PlayOneShot(playerPrefab.GetComponent<AudioSource>().clip);//plays sound when attacking
            yield return new WaitForSeconds(1f);
        }

        bool isDead = playerUnit.currentHP <= 0;

        yield return new WaitForSeconds(1f);
        if (isDead)
        {
            //if player is alive then subtract health else set state to won
            victoryState = VictoryState.LOST;
            EndBattle();
        }
        else
        {
            //player turn
            turnState = TurnState.PLAYERTURN;
            PlayerTurn();
        }


    }

    IEnumerator PlayerAbility()
    {
        if (enemyUnit.reflect > 0)
        {
            enemyUnit.reflect -= 1;
            textMeshPro.SetText(enemyUnit.unitName + " reflects the attack!");
            yield return new WaitForSeconds(1f);
            playerUnit.currentHP -= playerUnit.magicDamage;
            playerHUD.SetHP(playerUnit.currentHP);
            playerUnit.currentMP -= playerUnit.mpCost;
            playerHUD.SetMP(playerUnit.currentMP);
            playerHUD.GetComponent<Animator>().Play("Player bars attacked"); //plays attack animation on the player hud
            playerPrefab.GetComponent<Animator>().Play("Player attacked"); //plays attack animation on the player prefab
            textMeshPro.SetText(playerUnit.unitName + " takes damage from reflection!");
            playerPrefab.GetComponent<AudioSource>().PlayOneShot(playerPrefab.GetComponent<AudioSource>().clip);
            yield return new WaitForSeconds(1f);
        }
        else
        {
            enemyUnit.currentHP -= playerUnit.magicDamage;
            enemyHUD.SetHP(enemyUnit.currentHP);
            playerUnit.currentMP -= playerUnit.mpCost;
            playerHUD.SetMP(playerUnit.currentMP);
            enemyHUD.GetComponent<Animator>().Play("Enemy Hud attacked");//plays attack animation on the enemy hud
            enemyPrefab.GetComponent<Animator>().Play("enemy attacked");//plays attack animation on the enemy prefab
            textMeshPro.SetText(playerUnit.unitName + " uses a magical ability!");
            enemyPrefab.GetComponent<AudioSource>().PlayOneShot(enemyPrefab.GetComponent<AudioSource>().clip);
            yield return new WaitForSeconds(1f);
        }
        bool isDead = enemyUnit.currentHP <= 0;

        yield return new WaitForSeconds(1f);

        //check if enemy is dead
        if (isDead)
        {
            //end battle
            victoryState = VictoryState.WON;
            EndBattle();
        }
        else
        {
            //apply bleeding damage if player is bleeding
            if (playerUnit.bleeding > 0)
            {
                playerUnit.currentHP -= 5;
                playerHUD.SetHP(playerUnit.currentHP);
                playerHUD.GetComponent<Animator>().Play("Player bars attacked"); //plays attack animation on the player hud
                playerPrefab.GetComponent<Animator>().Play("Player attacked"); //plays attack animation on the player prefab
                textMeshPro.SetText(playerUnit.unitName + " takes damage from bleeding!");
                playerPrefab.GetComponent<AudioSource>().PlayOneShot(playerPrefab.GetComponent<AudioSource>().clip);
            }
            playerUnit.bleeding -= 1;
            if (playerUnit.bleeding < 0)
            {
                playerUnit.bleeding = 0;
            }
            yield return new WaitForSeconds(1f);
            //enemy turn
            turnState = TurnState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }

    }

    IEnumerator PlayerBuff()
    {
        if (playerUnit.currentMP < playerUnit.mpCost)
        {
            textMeshPro.SetText("Not enough MP!");
            yield break;
        }

        playerUnit.currentMP -= playerUnit.buffMPCost;
        playerHUD.SetMP(playerUnit.currentMP);

        playerUnit.attackBuff += playerUnit.attackBuffValue;
        playerUnit.attackBuffTurns = 2;
        playerUnit.damage += playerUnit.attackBuffValue;

        textMeshPro.SetText(playerUnit.unitName + " uses a Power Up!");
        yield return new WaitForSeconds(1f);

        turnState = TurnState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    IEnumerator PlayerDefend()
    {
        if (playerUnit.currentMP < playerUnit.mpCost)
        {
            textMeshPro.SetText("Not enough MP!");
            yield break;
        }
        playerUnit.currentMP -= playerUnit.mpCost;
        playerHUD.SetMP(playerUnit.currentMP);

        playerUnit.defenseDuration += 1;

        if (playerUnit.defenseDuration > 1)
        {
            playerUnit.defenseDuration = 1;
        }

        textMeshPro.SetText(playerUnit.unitName + " prepares to defend!");
        yield return new WaitForSeconds(1f);
        turnState = TurnState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    //Performs attack when clicked
    public void OnAttackButton()
    {
        if(turnState != TurnState.PLAYERTURN)
            return;
        StartCoroutine(PlayerAttack());
    }

    public void OnMagicButton()
    {
        if(turnState != TurnState.PLAYERTURN)
            return;

        if(playerUnit.currentMP < playerUnit.mpCost)
        {
            textMeshPro.SetText("Not enough MP!");
            return;
        } 
        StartCoroutine(PlayerAbility());
    }

    public void OnBuffButton()
    {
        if(turnState != TurnState.PLAYERTURN)
            return;

        if(playerUnit.currentMP < playerUnit.buffMPCost)
        {
            textMeshPro.SetText("Not enough MP!");
            return;
        } 

        StartCoroutine(PlayerBuff());
    }

    public void OnDefendButton()
    {
        if(turnState != TurnState.PLAYERTURN)
            return;
        if(playerUnit.currentMP < playerUnit.mpCost)
        {
            textMeshPro.SetText("Not enough MP!");
            return;
        } 
        StartCoroutine(PlayerDefend());
    }
    public void OnTalkButton()
    {         if(turnState != TurnState.PLAYERTURN)
            return;
        
        StartCoroutine(PlayerTalk());
    }

    //ends the battle
    public void EndBattle()
    {
        if (victoryState == VictoryState.WON)
        {
            textMeshPro.SetText("You won the battle!");
            SceneManager.LoadScene(9);

        }
        else if (victoryState == VictoryState.LOST)
        {
            textMeshPro.SetText("You lost the battle!");
            SceneManager.LoadScene(1);

        }
    }


    public void ShowButtons()
    {
        // Show the buttons
        foreach (Transform child in buttonContainer)
        {
            child.gameObject.SetActive(true);
        }
    }
    public void ShowAbilityButtons()
    {
        abilityCuttonContainer.gameObject.SetActive(true);
    }

    public void HideAbilityButtons()
    {
        abilityCuttonContainer.gameObject.SetActive(false);
    }

    public void HideButtons()
    {
    
        // Hide the buttons
        foreach (Transform child in buttonContainer)
        {
            child.gameObject.SetActive(false);
        }
    }

}
