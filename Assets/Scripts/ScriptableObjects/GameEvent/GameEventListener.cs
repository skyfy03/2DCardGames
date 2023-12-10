using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{

    public GameEvent Event;
    public UnityEvent Respoonse;

	private void OnEnable()
	{
		Event.RegisterListener(this);
	}

	private void OnDisable()
	{
		Event.UnregisterListener(this);
	}

	public void OnEventRaised()
	{
		Respoonse.Invoke();
	}

}
