using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TransitionController : MonoBehaviour
{
    private Animator[] _transitionAnimators;
    private Animator _currentTransition;

    public Action doTransition;

    private void Start()
    {
        _transitionAnimators = GetComponentsInChildren<Animator>();
        _currentTransition = GetRandomTransition();
    }

    private Animator GetRandomTransition()
    {
        doTransition = AppearTransition;
        return _transitionAnimators[Random.Range(0, _transitionAnimators.Length)];
    }

    private void AppearTransition()
    {
        _currentTransition.SetTrigger("Appear");

        doTransition = DisappearTransition;
    }

    private void DisappearTransition()
    {
        _currentTransition.SetTrigger("Disappear");

        _currentTransition = GetRandomTransition();
    }
}
