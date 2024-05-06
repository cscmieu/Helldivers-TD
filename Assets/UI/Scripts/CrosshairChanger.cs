using System;
using Singletons;
using UnityEngine;

namespace UI
{
    public class CrosshairChanger : SimpleSingleton<CrosshairChanger>
    {
        public Texture2D defaultCursor;
        public Texture2D hoveringCursor;
        public Texture2D placingCursor;

        [Range(0, 2)] private int _actualState; // 0 : default, 1 : hovering, 2 : placing

        private void Start()
        {
            Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.ForceSoftware);
        }

        public void OnBeginHovering()
        {
            Cursor.SetCursor(hoveringCursor, Vector2.zero, CursorMode.ForceSoftware);
            _actualState = 1;
        }
        
        public void OnExitHovering()
        {
            if (_actualState != 1) return;
            Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.ForceSoftware);
            _actualState = 0;
        }
        
        public void OnEnablePlacing()
        {
            Cursor.SetCursor(placingCursor, Vector2.zero, CursorMode.ForceSoftware);
            _actualState = 2;
        }
        
        public void OnDisablePlacing()
        {
            if (_actualState != 2) return;
            Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.ForceSoftware);
            _actualState = 0;
        }
    }
}
