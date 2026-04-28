using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;
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

        textMeshPro.SetText("player turn");

        //assign hud to characters
        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);
        yield return new WaitForSeconds(3f);

        //start the player's turn
        turnState = TurnState.PLAYERTURN;
        PlayerTurn();
        ShowButtons();

    }



    //wait for the player to choose an action
    void PlayerTurn()
    {
        textMeshPro.SetText("choose action");
        ShowButtons();
    }

    IEnumerator PlayerAttack()
    {

        //Damage enemy
        bool isDead = enemyUnit.TakeDamage(playerUnit.damage);
        enemyHUD.SetHP(enemyUnit.currentHP);
        textMeshPro.SetText(enemyUnit.unitName + " takes " + playerUnit.damage + " damage!");

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
            //enemy turn
            turnState = TurnState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
}
  
    IEnumerator EnemyTurn()
    {
        HideButtons();
        textMeshPro.SetText(enemyUnit.unitName + " attacks!");
        //enemy performs attack
        bool isDead = playerUnit.TakeDamage(enemyUnit.damage);
        playerHUD.SetHP(playerUnit.currentHP);

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

    //Performs attack when clicked
    public void OnAttackButton()
    {
        if(turnState != TurnState.PLAYERTURN)
            return;
        
        StartCoroutine(PlayerAttack());
    }

    //ends the battle
    public void EndBattle()
    {
        if (victoryState == VictoryState.WON)
        {
            textMeshPro.SetText("You won the battle!");

        }
        else if (victoryState == VictoryState.LOST)
        {
            textMeshPro.SetText("You lost the battle!");

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

    public void HideButtons()
    {
        // Hide the buttons
        foreach (Transform child in buttonContainer)
        {
            child.gameObject.SetActive(false);
        }
    }

}
