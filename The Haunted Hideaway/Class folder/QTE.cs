using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace The_Haunted_Hideaway;

public class QTE
{
    private List<Keys> _sequence;
    private readonly int _qteSequenceLength;
    private int _currentIndex;
    private bool _isActive;
     
    public QTE(List<Keys> sequence)
    {
        _qteSequenceLength = _sequence.Count;
        _sequence = sequence;
        _currentIndex = 0;
        _isActive = false;
    }

    public bool Update()
    {
        if (!_isActive) return false;

        var state = Keyboard.GetState();

        foreach (var key in state.GetPressedKeys())
        {
            if (_currentIndex == -1 && key == _sequence[0])
            {
                _currentIndex++;
                continue;
            }

            if (_currentIndex >= 0 && key == _sequence[_currentIndex + 1])
            {
                _currentIndex++;

                if (_currentIndex == _qteSequenceLength - 1)
                {
                    _isActive = false;
                    _currentIndex = -1;
                    return true;
                }

                continue;
            }

            _isActive = false;
            _currentIndex = -1;
            return false;
        }

        return false;
    }
    
     
    public void Start()
    {
        _isActive = true;
        _currentIndex = -1;
    }

}