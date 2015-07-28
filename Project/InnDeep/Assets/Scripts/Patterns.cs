using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace InnDeep.Patterns
{
    public abstract class State
    {
        private List<Func<bool>> transitionTests;
        private List<int> edges;
        private int nextState;

        private State(State s)
        {

        }

        public State()
        {
            transitionTests = new List<Func<bool>>();
            edges = new List<int>();
        }

        /// <summary>
        /// Add transition tests and resulting state id's
        /// </summary>
        /// <param name="test">The test for the next state</param>
        /// <param name="next">The resulting state when the test succeeds</param>
        public void addTransitionToState(Func<bool> test, int next)
        {
            if (!transitionTests.Contains(test))
            {
                transitionTests.Add(test);
                edges.Add(next);
            }
        }

        /// <summary>
        /// Handles transition tests and sets up to get next state
        /// </summary>
        /// <returns></returns>
        public virtual bool Update(float dT)
        {
            for(int i=0; i < transitionTests.Count; ++i)
            {
                var result = transitionTests[i]();
                if(!result)
                {
                    nextState = edges[i];
                    return result;
                }
            }
            return true;
        }

        /// <summary>
        /// Use this to setup a state
        /// </summary>
        public abstract void Start();

        /// <summary>
        /// Call this at the end of a state
        /// </summary>
        /// <returns></returns>
        public virtual int Stop()
        {
            return nextState;
        }
    }

    public class StateMachine
    {
        private List<State> states;
        private State current;

        public StateMachine() 
        {
            states = new List<State>();
        }

        public void addState(params State[] s)
        {
            foreach (State st in s)
            {
                states.Add(st);
            }

        }

        public void Start(int id)
        {
            if (states.Count <= id)
            {
                current = null;
                return;
            }

            current = states[id];
            current.Start();
        }

        public void Update(float dT)
        {
            if (current == null)
                return;

            if(!current.Update(dT))
            {
                Start(current.Stop());
            }
        }
    }
}
