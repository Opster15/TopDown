using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class InputManager : MonoBehaviour
{
    private enum InputChanged
    {
        None,
        Pressed,
        Released,
        ValueChanged
    }

    /// <summary>
    /// The input action asset defining all inputs (keyboard, controller, etc) for the game.
    /// </summary>
    [SerializeField] private InputActionAsset m_inputActions;

    /// <summary>
    /// The name of the horizontal input axis in the selected input action asset.
    /// </summary>
    [SerializeField] private string m_movementActionName;

    [SerializeField] private string m_spaceActionName;

    [SerializeField] private string m_InteractActionName;

    //[SerializeField] private string m_SwitchActionName;

    [SerializeField] private string m_leftClickActionName;

    //[SerializeField] private string m_rightClickActionName;

    [SerializeField] private string m_mousePositionActionName;

    /// <summary>
    /// The input action representing the horizontal input axis.
    /// </summary>
    private InputAction m_movement;

    /// <summary>
    /// The input action representing the horizontal input axis.
    /// </summary>
    private InputAction m_Interact;

    /// <summary>
    /// The input action representing the horizontal input axis.
    /// </summary>
    //private InputAction m_Switch;

    /// <summary>
    /// The input action representing the left mouse button.
    /// </summary>
    private InputAction m_leftClick;

    /// <summary>
    /// The input action representing the right mouse button.
    /// </summary>
    //private InputAction m_rightClick;

    /// <summary>
    /// The input action representing the space button.
    /// </summary>
    private InputAction m_space;

    /// <summary>
    /// The input action representing the space button.
    /// </summary>
    private InputAction m_mousePosition;

    /// <summary>
    /// The user's movement input this frame.
    /// </summary>
    public Vector2 m_movementInput;

    /// <summary>
    /// The user's movement input this frame.
    /// </summary>
    private bool m_spaceInput;

    /// <summary>
    /// The user's movement input this frame.
    /// </summary>
    private bool m_InteractInput;

    /// <summary>
    /// The user's movement input this frame.
    /// </summary>
    //private bool m_SwitchInput;

    /// <summary>
    /// The user's movement input this frame.
    /// </summary>
    private bool m_LastInteractInput;

    /// <summary>
    /// The user's left click input this frame.
    /// </summary>
    private bool m_leftClickInput;

    /// <summary>
    /// The user's right click input this frame.
    /// </summary>
    //private bool m_rightClickInput;

    /// <summary>
    /// The user's movement input this frame.
    /// </summary>
    public Vector2 m_mousePositionInput;

    /// <summary>
    /// The user's movement input last frame.
    /// </summary>
    private Vector2 m_lastMovementInput;

    /// <summary>
    /// The user's left click input last frame.
    /// </summary>
    private bool m_lastLeftClickInput;

    /// <summary>
    /// The user's right click input last frame.
    /// </summary>
    //private bool m_lastRightClickInput;

    private bool m_lastSpaceInput;

    //private bool m_lastSwitchInput;

    /// <summary>
    /// The way the user's movement input has changed this frame.
    /// </summary>
    private InputChanged m_movementChanged;

    /// <summary>
    /// The way the user's movement input has changed this frame.
    /// </summary>
    private InputChanged m_InteractChanged;

    /// <summary>
    /// The way the user's movement input has changed this frame.
    /// </summary>
    //private InputChanged m_SwitchChanged;

    /// <summary>
    /// The way the user's left click input has changed this frame.
    /// </summary>
    private InputChanged m_leftClickChanged;

    /// <summary>
    /// The way the user's right click input has changed this frame.
    /// </summary>
    //private InputChanged m_rightClickChanged;

    private InputChanged m_spaceChanged;

    /// <summary>
    /// Returns whether there is movement input.
    /// </summary>
    private bool m_isMoving;

    private InputAction MovementAction
    {
        get
        {
            if (m_movement == null)
            {
                if (m_movement == null) m_movement = m_inputActions[m_movementActionName];
                return m_movement;
            }
            return m_movement;
        }
    }

    private InputAction InteractAction
    {
        get
        {
            if (m_Interact == null)
            {
                if (m_Interact == null) m_Interact = m_inputActions[m_InteractActionName];
                return m_Interact;
            }
            return m_Interact;
        }
    }
    /*
    private InputAction SwitchAction
    {
        get
        {
            if (m_Switch == null)
            {
                if (m_Switch == null) m_Switch = m_inputActions[m_SwitchActionName];
                return m_Switch;
            }
            return m_Switch;
        }
    }
    */
    private InputAction LeftClickAction
    {
        get
        {
            if (m_leftClick == null)
            {
                if (m_leftClick == null) m_leftClick = m_inputActions[m_leftClickActionName];
                return m_leftClick;
            }
            return m_leftClick;
        }
    }
    /*
    private InputAction RightClickAction
    {
        get
        {
            if (m_rightClick == null)
            {
                m_rightClick = m_inputActions[m_rightClickActionName];
            }
            return m_rightClick;
        }
    }
    */
    private InputAction SpaceAction
    {
        get
        {
            if (m_space == null)
            {
                if (m_space == null) m_space = m_inputActions[m_spaceActionName];
                return m_space;
            }
            return m_space;
        }
    }
    
    private InputAction MousePositionAction
    {
        get
        {
            if (m_mousePosition == null)
            {
                if (m_mousePosition == null) m_mousePosition = m_inputActions[m_mousePositionActionName];
                return m_mousePosition;
            }
            return m_mousePosition;
        }
    }
      

    // MOVEMENT

    public Vector2 Movement => m_movementInput;

    public bool IsMoving => m_isMoving;

    public bool MovementPressed => m_movementChanged == InputChanged.Pressed;

    public bool MovementReleased => m_movementChanged == InputChanged.Released;

    public bool MovementChanged => m_movementChanged == InputChanged.ValueChanged;

    // INTERACT

    public bool Interact => m_InteractInput;

    public bool InteractPressed => m_InteractChanged == InputChanged.Pressed;

    public bool InteractReleased => m_InteractChanged == InputChanged.Released;

    // INTERACT

    //public bool Switch => m_SwitchInput;

    //public bool SwitchPressed => m_SwitchChanged == InputChanged.Pressed;

    //public bool SwitchReleased => m_SwitchChanged == InputChanged.Released;

    // LEFT CLICK

    public bool LeftClick => m_leftClickInput;

    public bool LeftClickPressed => m_leftClickChanged == InputChanged.Pressed;

    public bool LeftClickReleased => m_leftClickChanged == InputChanged.Released;

    // RIGHT CLICK

    //public bool RightClick => m_rightClickInput;

    //public bool RightClickPressed => m_rightClickChanged == InputChanged.Pressed;

    //public bool RightClickReleased => m_rightClickChanged == InputChanged.Released;

    // Space
    public bool spacePress => m_spaceInput;

    public bool spacePressed => m_spaceChanged == InputChanged.Pressed;

    public bool spaceReleased => m_spaceChanged == InputChanged.Released;

    private void OnEnable()
    {
        MovementAction.Enable();
        MousePositionAction.Enable();
        LeftClickAction.Enable();
        //RightClickAction.Enable();
        SpaceAction.Enable();
        InteractAction.Enable();
        //SwitchAction.Enable();
    }

    private void OnDisable()
    {
        MovementAction.Disable();
        MousePositionAction.Disable();
        LeftClickAction.Disable();
        //RightClickAction.Disable();
        SpaceAction.Disable();
        InteractAction.Disable();
        //SwitchAction.Disable();
    }

    private void Update()
    {
        ReadInputs();
        UpdateProperties();
        UpdateLastInputs();
    }

    /// <summary>
    /// Read the current user inputs for this frame.
    /// </summary>
    private void ReadInputs()
    {
        m_leftClickInput = ReadButtonInput(LeftClickAction);
        //m_rightClickInput = ReadButtonInput(RightClickAction);
        m_spaceInput = ReadButtonInput(SpaceAction);
        m_InteractInput = ReadButtonInput(InteractAction);
        //m_SwitchInput = ReadButtonInput(SwitchAction);

        m_movementInput = ReadVectorInput(MovementAction);
        m_mousePositionInput = ReadVectorInput(MousePositionAction);
    }

    /// <summary>
    /// Update the publicly accessible properties, used by other classes to work with player input.
    /// </summary>
    private void UpdateProperties()
    {
        m_leftClickChanged = HasBoolChanged(m_leftClickInput, m_lastLeftClickInput);
        //m_rightClickChanged = HasBoolChanged(m_rightClickInput, m_lastRightClickInput);
        m_spaceChanged = HasBoolChanged(m_spaceInput, m_lastSpaceInput);
        m_InteractChanged = HasBoolChanged(m_InteractInput, m_LastInteractInput);
        //m_SwitchChanged = HasBoolChanged(m_SwitchInput, m_LastInteractInput);

        var wasMoving = m_isMoving;
        m_isMoving = m_movementInput != Vector2.zero;

        m_movementChanged = HasBoolChanged(m_isMoving, wasMoving);
        if (m_movementChanged == InputChanged.None && m_movementInput != m_lastMovementInput)
        {
            m_movementChanged = InputChanged.ValueChanged;
        }
    }

    /// <summary>
    /// Update the "last frame" variables for next frame's inputs.
    /// </summary>
    private void UpdateLastInputs()
    {
        m_lastLeftClickInput = m_leftClickInput;
        //m_lastRightClickInput = m_rightClickInput;
        m_lastMovementInput = m_movementInput;
        m_lastSpaceInput = m_spaceInput;
        m_LastInteractInput = m_InteractInput;
        //m_lastSwitchInput = m_SwitchInput;
    }

    /// <summary>
    /// Reads the user's input for a specific button.
    /// </summary>
    /// <param name="_inputButton">The button to be read.</param>
    /// <returns>The user's input for this button.</returns>
    private static bool ReadButtonInput(InputAction _inputButton)
        => _inputButton.ReadValue<float>() > 0.0f;

    /// <summary>
    /// Reads the user's input for a specified vector.
    /// </summary>
    /// <param name="_inputVector">The vector to be read.</param>
    /// <returns>The user's X/Y input for this vector.</returns>
    private static Vector2 ReadVectorInput(InputAction _inputVector)
        => _inputVector.ReadValue<Vector2>();

    /// <summary>
    /// Check how the value of a bool between the current and last frames.
    /// </summary>
    /// <param name="_value">The value of a bool.</param>
    /// <param name="_lastValue">The value of a bool, last frame.</param>
    /// <returns>Value determining whether the bool has been enabled, disabled, or hasn't changed at all.</returns>
    private static InputChanged HasBoolChanged(bool _value, bool _lastValue)
    {
        if (_value && !_lastValue)
        {
            return InputChanged.Pressed;
        }
        else if (!_value && _lastValue)
        {
            return InputChanged.Released;
        }

        return InputChanged.None;
    }
}
