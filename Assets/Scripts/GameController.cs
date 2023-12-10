using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    [SerializeField] private Transform higherCardPlacement = null;
    [SerializeField] private Transform leftCardPlacement = null;
    [SerializeField] private Transform rightCardPlacement = null;
    [SerializeField] private Transform lowerCardPlacement = null;
    [SerializeField] private Transform hand = null;
    [SerializeField] private Transform enemyHand = null;

    [SerializeField] private CardDisplay cardDisplay = null;

    [SerializeField] private Deck deck = null;

    [SerializeField] private IntReference intRefCardNumeral = null;
    [SerializeField] private IntReference intRefCardSuit = null;

    [SerializeField] private Button deckButton = null;
    [SerializeField] private Button endTurn = null;

    private bool isPlayerTurn = true;
    private bool drawCardInteractable = true;

    private void drawCard(Deck deck, Transform transform, CardDisplay newCard)
	{
        newCard = deck.pullCardFromDeck(cardDisplay);

        if (newCard.cardInt != -1)
        {
            newCard.setCardDisplay();
            newCard = Instantiate(newCard, transform);

            intRefCardNumeral.Variable.Value = newCard.cardInt;
            intRefCardSuit.Variable.Value = newCard.cardSuitInt;
        }
    }

    public void BtnDrawCard()
	{
		if (drawCardInteractable)
		{
            drawCard(deck, hand, cardDisplay);
            drawCardInteractable = false;
        }
    }

    private void BtnDrawEnemyCard()
	{
        drawCard(deck, enemyHand, cardDisplay);
    }

    public void BtnEndTurn()
	{
		if (isPlayerTurn)
		{
            ChangeTurn();
        }
	}

    private void ChangeTurn()
	{
        isPlayerTurn = !isPlayerTurn;

		if (!isPlayerTurn)
		{
            deckButton.interactable = false;

            Color red = new Color(255, 0, 0);
            endTurn.image.color = red;
            endTurn.interactable = false;

            StartCoroutine(EnemyTurn());

            ChangeTurn();
        }
        else
		{
            drawCardInteractable = true;

            deckButton.interactable = true;

            Color green = new Color(0, 248, 23);
            endTurn.image.color = green;
            endTurn.interactable = true;
        }
    }

    private IEnumerator EnemyTurn()
	{
        yield return new WaitForSeconds(1);

        BtnDrawEnemyCard();
	}

	void Start()
	{
        StartCoroutine(setupKingsCorner());
    }

    private IEnumerator setupKingsCorner()
	{
        yield return new WaitForSeconds(.5f);

        Draggable d = cardDisplay.GetComponent<Draggable>();
        d.isCardDraggable = true;

        for (int i = 0; i < 5; i++)
		{
            //pull five cards for player
            yield return new WaitForSeconds(.5f);
            drawCard(deck, hand, cardDisplay);

            //pull five cards for enemys
            yield return new WaitForSeconds(.5f);
            drawCard(deck, enemyHand, cardDisplay);
        }

        //d.isCardDraggable = true;

        yield return new WaitForSeconds(.5f);
        drawCard(deck, higherCardPlacement, cardDisplay);
        
        yield return new WaitForSeconds(.5f);
        drawCard(deck, leftCardPlacement, cardDisplay);
        
        yield return new WaitForSeconds(.5f);
        drawCard(deck, rightCardPlacement, cardDisplay);
        
        yield return new WaitForSeconds(.5f);
        drawCard(deck, lowerCardPlacement, cardDisplay);
    }
}
