using UnityEngine;

namespace BaseTemplate
{
    /// <summary>
    /// This is the base class from which inherit every state for BasicStateMachine
    /// </summary>
    public abstract class State
    {
        private BasicStateMachine _stateMachine;
        public BasicStateMachine StateMachine { get => _stateMachine; set => _stateMachine = value; }

        private bool _isActive;
        public bool IsActive { get => _isActive; set => _isActive = value; }

        bool isInitialized;

        #region protected

        /// <summary>
        /// Use statemachine's transform if possible
        /// </summary>
        protected Transform transformState => _stateMachine != null ? _stateMachine.transform : null;

        /// <summary>
        /// Return StateMachine casted to inherited class
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected T GetStateMachine<T>() where T : BasicStateMachine
        {
            return (T)_stateMachine;
        }

        /// <summary>
        /// Get unity component in stateMachine or its parents. If not found, show warning
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="showWarningIfNotFound"></param>
        /// <returns></returns>
        protected T GetStateMachineUnityComponent<T>(bool showWarningIfNotFound = true)
        {
            //get in parent
            T component = _stateMachine != null ? _stateMachine.transform.GetComponentInParent<T>() : default;

            //show warning if not found
            if (showWarningIfNotFound && component == null)
                Debug.LogWarning($"Miss {typeof(T).Name} on {_stateMachine}", _stateMachine.transform.gameObject);

            return component;
        }

        /// <summary>
        /// Try get unity component in stateMachine or its parents
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="foundComponent"></param>
        /// <returns></returns>
        protected bool TryGetStateMachineUnityComponent<T>(out T foundComponent)
        {
            //get in parent
            foundComponent = _stateMachine != null ? _stateMachine.transform.GetComponentInParent<T>() : default;
            return foundComponent != null;
        }

        /// <summary>
        /// Get blackboard or normal value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="v"></param>
        /// <returns></returns>
        protected T GetValue<T>(VarOrBlackboard<T> v) => v != null ? v.GetValue(_stateMachine) : default;

        #endregion

        #region statemachine functions

        /// <summary>
        /// Called by StateMachine, to set owner and to be sure to call OnInit() only one time
        /// </summary>
        /// <param name="stateMachine"></param>
        public void Initialize(BasicStateMachine stateMachine)
        {
            if (isInitialized)
                return;

            isInitialized = true;

            //set state machine and init
            _stateMachine = stateMachine;
            OnInit();
        }

        /// <summary>
        /// Called by StateMachine, to set active and call OnEnter()
        /// </summary>
        public void Enter()
        {
            _isActive = true;
            OnEnter();
        }

        /// <summary>
        /// Called by StateMachine, to set not active and call OnExit()
        /// </summary>
        public void Exit()
        {
            _isActive = false;
            OnExit();
        }

        /// <summary>
        /// Called by StateMachine, to call OnUpdate()
        /// </summary>
        public void Update()
        {
            if (_isActive)
                OnUpdate();
        }

        /// <summary>
        /// Called by StateMachine, to call OnFixedUpdate()
        /// </summary>
        public void FixedUpdate()
        {
            if (_isActive)
                OnFixedUpdate();
        }

        /// <summary>
        /// Called by StateMachine, to call OnLateUpdate()
        /// </summary>
        public void LateUpdate()
        {
            if (_isActive)
                OnLateUpdate();
        }

        #endregion

        /// <summary>
        /// Called only one time, first time enter in this state
        /// </summary>
        protected virtual void OnInit() { }
        /// <summary>
        /// Called every time enter in this state
        /// </summary>
        protected virtual void OnEnter() { }
        /// <summary>
        /// Called every time exit from this state
        /// </summary>
        protected virtual void OnExit() { }
        /// <summary>
        /// Called every time update this state
        /// </summary>
        protected virtual void OnUpdate() { }
        /// <summary>
        /// Called every fixed update in this state
        /// </summary>
        protected virtual void OnFixedUpdate() { }
        /// <summary>
        /// Called every late update in this state
        /// </summary>
        protected virtual void OnLateUpdate() { }
    }
}