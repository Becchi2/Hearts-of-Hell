using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;
public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }


public class BattleSystem : MonoBehaviour
{
    public static BattleSystem Instance4 { get; private set; }

    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public TextMeshProUGUI textMeshPro;

    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    Unit playerUnit;
    Unit enemyUnit;

    public BattleHudd playerHUD;
    public BattleHudd enemyHUD;

    public BattleState state;

    private void Awake()
    {

        //ensures theres only one instance of dialogue manager at a time
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
        GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
        playerUnit = playerGO.GetComponent<Unit>();

        GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemyGO.GetComponent<Unit>();

        Debug.Log("kuchisake onna gets aggressive");
        textMeshPro.SetText("kuchisake onna gets aggressive");

        playerHUD.SetHUD(playerUnit);
        //enemyHUD.SetHUD(enemyUnit);
        
        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;


    }

    IEnumerator PlayerAttack()
    {
        state = BattleState.ENEMYTURN;

        bool isDead = enemyUnit.TakeDamage(playerUnit.damage);

        enemyHUD.SetHP(enemyUnit.currentHP);
        Debug.Log("kuchisake onna takes " + playerUnit.damage + " damage!");
        textMeshPro.SetText("kuchisake onna takes " + playerUnit.damage + " damage!");

        yield return new WaitForSeconds(2f);

        if(isDead)
        {
            state = BattleState.WON;
            EndBattle();
            SceneManager.LoadScene("Office SlitMouth 4");//Loads the enidng scene if the player wins the battle
        }
        else
        {
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator EnemyTurn()
    {
        Debug.Log("kuchisake onna attacks!");
        textMeshPro.SetText("kuchisake onna attacks!");

        yield return new WaitForSeconds(1f);

        bool isDead = playerUnit.TakeDamage(enemyUnit.damage);

        playerHUD.SetHP(playerUnit.currentHP);

        yield return new WaitForSeconds(2f);

        if(isDead)
        {
            state = BattleState.LOST;
            EndBattle();
            SceneManager.LoadScene("Office SlitMouth 4");//Loads the enidng scene if the player wins the battle
        }
        else
        {
            state = BattleState.PLAYERTURN;
        }
    }

    IEnumerator PlayerAttackTwo()
    {
        const int mpCost = 30;
        playerHUD.SetMP(playerUnit.currentMP);
        if (playerUnit.currentMP < mpCost)
        {
            Debug.Log("Not enough MP!");
            textMeshPro.SetText("Not enough MP!");
            yield break;
        }

        state = BattleState.ENEMYTURN;

        playerUnit.UseMP(mpCost);

        bool isDead = enemyUnit.TakeMagicDamage(playerUnit.magicDamage);

        enemyHUD.SetHP(enemyUnit.currentHP);

        Debug.Log("kuchisake onna takes " + playerUnit.magicDamage + " magic damage!");
        textMeshPro.SetText("kuchisake onna takes " + playerUnit.magicDamage + " magic damage!");

        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            state = BattleState.WON;
            EndBattle();
            SceneManager.LoadScene("Office SlitMouth 4");//Loads the enidng scene if the player wins the battle
        }
        else
        {
            StartCoroutine(EnemyTurn());
        }
    }
    void EndBattle()
        {
        if(state == BattleState.WON)
        {
            Debug.Log("You won the battle!");
            textMeshPro.SetText("You won the battle!");
        }
        else if(state == BattleState.LOST)
        {
            Debug.Log("You were defeated.");
            textMeshPro.SetText("You were defeated.");
        }
    }
    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerAttack());
    }
    public void OnAbilityButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerAttackTwo());
    }
}
