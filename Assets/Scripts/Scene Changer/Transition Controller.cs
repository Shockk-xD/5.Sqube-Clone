using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TransitionController : MonoBehaviour
{
    [SerializeField] private GameObject[] _transitionParents;
    private List<Action> _transitionActions;

    public Action doTransition;

    private void Start()
    {
        _transitionActions = new(_transitionParents.Length)
        {
            FirstTransitionAppear
        };
    }

    private Action GetRandomFadeInTransition()
    {
        return _transitionActions[Random.Range(0, _transitionActions.Count)];
    }

    private void FirstTransitionAppear()
    {

        doTransition = FirstTransitionDisappear;
    }

    private void FirstTransitionDisappear()
    {
        
    }
}
