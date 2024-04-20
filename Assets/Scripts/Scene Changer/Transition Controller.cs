using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TransitionController : MonoBehaviour
{
    private Animator[] _transitionAnimators;
    private Animator _currentTransition;
    private int _currentTransitionIndex = 0;

    public Action doTransition;

    private void Start()
    {
        _transitionAnimators = GetComponentsInChildren<Animator>();
        _currentTransition = GetNextTransition();
    }

    private Animator GetNextTransition()
    {
        doTransition = AppearTransition;
        _currentTransitionIndex++;
        _currentTransitionIndex = _currentTransitionIndex >= _transitionAnimators.Length ? 0 : _currentTransitionIndex;
        return _transitionAnimators[_currentTransitionIndex];
    }

    private void AppearTransition()
    {
        _currentTransition.SetTrigger("Appear");

        doTransition = DisappearTransition;
    }

    private void DisappearTransition()
    {
        _currentTransition.SetTrigger("Disappear");

        _currentTransition = GetNextTransition();
    }
}
