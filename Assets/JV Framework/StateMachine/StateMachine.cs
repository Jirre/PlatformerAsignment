using System.Collections.Generic;

public class StateMachine
{
    public delegate void StateFunction();

    private Dictionary<string, StateFunction> _states;
    private string _currentState;

    /// <summary>
    /// Add State to State List
    /// </summary>
    /// <param name="pIdentifier">Identifier of the State</param>
    /// <param name="pFunction">Function of the State</param>
    public void Add(string pIdentifier, StateFunction pFunction)
    {
        //Create State List if Nescesary
        if (_states == null) _states = new Dictionary<string, StateFunction>();

        //Null Checks to prevent faulty manipulations
        if(_states.ContainsKey(pIdentifier)) throw new System.Exception(string.Format("Attempting to add already existing key to State Machine [{0}]", pIdentifier));
        if (string.IsNullOrWhiteSpace(pIdentifier)) throw new System.Exception("Trying to add empty identifier for State within State Machine");
        if (pFunction == null) throw new System.Exception("Trying to add empty function to a State Machine");

        //Add State to List
        _states.Add(pIdentifier, pFunction);
    }

    /// <summary>
    /// Go to a given State in this State List
    /// </summary>
    /// <param name="pName">Identifier of the state to transition to</param>
    public void Goto(string pName)
    {
        //Null Checks to prevent faulty manipulations
        if (_states == null || !_states.ContainsKey(pName)) throw new System.Exception(string.Format("No state with this the ID exist [{0}]", pName));

        //Set Current State to given ID
        _currentState = pName;
    }

    /// <summary>
    /// Updates the currently active State
    /// </summary>
    public void Update()
    {
        //Null Checks to prevent non existant calls
        if (string.IsNullOrWhiteSpace(_currentState) ||
            _states == null ||
            !_states.ContainsKey(_currentState)) return;

        //Call State's Function
        _states[_currentState].Invoke();
    }
}