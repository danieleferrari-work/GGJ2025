using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Animated toggle in UI
/// </summary>
public class SwitchToggle : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] string onToggleAnimation;
    [SerializeField] string onUntoggleAnimation;
    [Space]
    [Tooltip("Play animation on awake")][SerializeField] bool updateOnAwake;
    [Tooltip("Set in inspector to set default value")][SerializeField] bool isToggled;
    [Space]
    public UnityEvent<bool> onValueChanged;

    public bool IsToggled => isToggled;

    private void Awake()
    {
        //set animation
        if (updateOnAwake)
            SetToggle(isToggled, false);
    }

    /// <summary>
    /// Change toggle value and trigger event
    /// </summary>
    public void OnClick()
    {
        SetToggle(!isToggled);
    }

    /// <summary>
    /// Set toggle value
    /// </summary>
    /// <param name="value"></param>
    /// <param name="triggerEvent">Call OnValueChanged event?</param>
    public void SetToggle(bool value, bool triggerEvent = true)
    {
        //be sure to have animator
        if (animator == null && TryGetComponent(out animator) == false)
        {
            Debug.LogError($"Missing animator on {name}", gameObject);
            return;
        }

        //change state
        isToggled = value;
        if (gameObject.activeInHierarchy) 
            animator.Play(isToggled ? onToggleAnimation : onUntoggleAnimation);

        //and call event
        if (triggerEvent)
            onValueChanged?.Invoke(isToggled);
    }
}
