using UnityEngine;

public class CharacterData : MonoBehaviour
{
    public int Attraction = 0;



    [SerializeField] Sprite[] CharacterSprite;
    Sprite newSprite;

    public void Update() //checks attraction points and changes sprite based on them
    {

        if (Attraction < 3 && Attraction >= 0)
        {

            newSprite = CharacterSprite[0];

            gameObject.GetComponent<SpriteRenderer>().sprite = newSprite;
        }
        else if (Attraction == 3)
        {

            newSprite = CharacterSprite[1];

            gameObject.GetComponent<SpriteRenderer>().sprite = newSprite;
        }
        else if (Attraction ==4)
        {

            newSprite = CharacterSprite[2];

            gameObject.GetComponent<SpriteRenderer>().sprite = newSprite;
        }
        else if (Attraction == 5)
        {

            newSprite = CharacterSprite[3];

            gameObject.GetComponent<SpriteRenderer>().sprite = newSprite;
        }
        else if (Attraction == 6 || Attraction == 7)
        {

            newSprite = CharacterSprite[4];

            gameObject.GetComponent<SpriteRenderer>().sprite = newSprite;
        }
        else if (Attraction == 8 || Attraction == 9)
        {

            newSprite = CharacterSprite[5];

            gameObject.GetComponent<SpriteRenderer>().sprite = newSprite;
        }
        else if (Attraction == 10)
        {

            newSprite = CharacterSprite[6];

            gameObject.GetComponent<SpriteRenderer>().sprite = newSprite;
        }
    }
}
 