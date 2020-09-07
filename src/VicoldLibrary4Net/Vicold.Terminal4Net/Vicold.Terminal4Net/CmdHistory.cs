using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vicold.Terminal4Net
{
    internal class CmdHistory
    {
        private readonly List<string> _history;
        private int _currentIndex = -1;
        internal CmdHistory()
        {
            _history = new List<string>();
        }

        internal void Add(string cmd)
        {
            if (_currentIndex >= 0 && _currentIndex < _history.Count)
            {
                // 有历史
                if (_history[_currentIndex] == cmd)
                {
                    // 和所取的历史相同
                    _history.RemoveAt(_currentIndex);
                }
            }
            _history.Add(cmd);
            _currentIndex = -1;
        }

        internal string FlipUp()
        {
            if (_currentIndex == -1)
            {
                _currentIndex = _history.Count ;
            }

            if (_currentIndex <= 0)
            {
                return _history[0];
            }
            return _history[--_currentIndex];
        }

        internal string FlipDown()
        {
            if (_currentIndex >= _history.Count - 1)
            {
                return _history[_history.Count - 1];
            }
            return _history[++_currentIndex];
        }


    }
}
