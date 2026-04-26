using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;
public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST } //the different states of the battle system


public class BattleSystem : MonoBehaviour
{
    public static BattleSystem Instance4 { get; private set; }

    public GameObject playerPrefab; // spawns the player prefab in the battle scene
    public GameObject enemyPrefab; // spawns the enemy prefab in the battle scene
    public TextMeshProUGUI textMeshPro;

    public Transform playerBattleStation; //where the player prefab will be spawned in the battle scene
    public Transform enemyBattleStation; //where the enemy prefab will be spawned in the battle scene

    Unit playerUnit;// reference to the Unit script attached to the player prefab
    Unit enemyUnit;// reference to the Unit script attached to the enemy prefab

    public BattleHudd playerHUD;// reference to the BattleHudd script to update the UI elements for the player
    public BattleHudd enemyHUD;// reference to the BattleHudd script to update the UI elements for the enemy

    public BattleState state; // lets us change the state of the battle system to determine what happens next in the battle

    private void Awake()
    {
        if (Instance4 == null)
        {
            Instance4 = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        //Instantiate player and enemy units
        //Set up the battle scene
        //Initialize UI elements    
        state = BattleState.PLAYERTURN;
        GameObject playerGO = Instantiate(playerPrefab, playerBattleStation); // places the player prefab in the battle scene at the playerBattleStation position
        playerUnit = playerGO.GetComponent<Unit>(); // gets the Unit component from the player prefab and assigns it to the playerUnit variable

        GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation); // places the enemy prefab in the battle scene at the enemyBattleStation position
        enemyUnit = enemyGO.GetComponent<Unit>(); // gets the Unit component from the enemy prefab and assigns it to the enemyUnit variable


        textMeshPro.SetText("kuchisake onna gets aggressive");

        playerHUD.SetHUD(playerUnit);// sets up the player HUD with the player unit's information
        enemyHUD.SetHUD(enemyUnit);// sets up the enemy HUD with the enemy unit's information

        yield return new WaitForSeconds(2f);
        PlayerTurn();

    }

    void PlayerTurn()
    {
        ShowButtons();
        state = BattleState.PLAYERTURN;// sets the state to the player's turn
        textMeshPro.SetText("Choose an action");
        
    }

    IEnumerator PlayerAttack()
    {
        //Damage the enemy
        bool isDead = enemyUnit.TakeDamage(playerUnit.damage);//emeny takes damage from the player and checks if the enemy is dead
        enemyHUD.SetHP(enemyUnit.currentHP); //updates the enemy HUD with the enemy unit's current HP

        yield return new WaitForSeconds(2f);

        //check if the enemy is dead
        if (isDead)
        {
            state = BattleState.WON; //player wins the battle
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;//player 's turn is over, now it's the enemy's turn
            StartCoroutine(EnemyTurn());
        }
    }

    void EndBattle()//ends the battle and displays the result
    {
        if (state == BattleState.WON)
        {
            textMeshPro.SetText("You won the battle!");
            // Load the next scene after a delay
        }
        else if (state == BattleState.LOST)
        {
            textMeshPro.SetText("You were defeated.");
            // Load the game over scene after a delay
        }
    }

    IEnumerator EnemyTurn()
    {
        HideButtons();
        textMeshPro.SetText(enemyUnit.unitName + " attacks");

        yield return new WaitForSeconds(1f);

        bool isDead = playerUnit.TakeDamage(enemyUnit.damage); //player takes damage from the enemy and checks if the player is dead

        playerHUD.SetHP(playerUnit.currentHP);//updates the player HUD with the player unit's current HP

        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            state = BattleState.LOST; //player loses the battle
            EndBattle();
        }
        else
        {
            state = BattleState.PLAYERTURN; //enemy's turn is over, now it's the player's turn
            PlayerTurn();
        }


        }

    public void onAttackButton() // performs the player attack when the attack button is pressed
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }
        else
        {
            StartCoroutine(PlayerAttack());
        }
    }

    public void ShowButtons()
    {
        // Show the buttons
        GameObject attackButton = GameObject.Find("AttackButton");
        GameObject abilityButton = GameObject.Find("AbilityButton");
        if (attackButton != null)
            attackButton.SetActive(true);
        if (abilityButton != null)
            abilityButton.SetActive(true);
    }

    public void HideButtons()
    {
        // Hide the buttons
        GameObject attackButton = GameObject.Find("AttackButton");
        GameObject abilityButton = GameObject.Find("AbilityButton");
        if (attackButton != null)
            attackButton.SetActive(false);
        if (abilityButton != null)
            abilityButton.SetActive(false);
    }
}
