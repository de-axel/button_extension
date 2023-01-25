using FancyButtons;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(Button))]
    public class ButtonEditor : UnityEditor.Editor
    {
        private const float WIDTH = 70;
        private const float HEIGHT = 25;
        
        private SerializedProperty _buttonCallbacksProperty;
        private GUIStyle _buttonStyle;
        private Button _button;

        private bool _isInit;
        
        private void Init()
        {
            if (_isInit)
                return;

            _isInit = true;
            _buttonStyle = new GUIStyle(EditorStyles.miniButton)
            {
                fixedWidth = WIDTH,
                fixedHeight = HEIGHT
            };
            _button = (Button)target;
            _buttonCallbacksProperty = serializedObject.FindProperty(nameof(_button.ButtonEditorCallback));
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            Init();
            DrawCallbackButton();
        }

        private void DrawCallbackButton()
        {
            serializedObject.UpdateIfRequiredOrScript();
            
            _button.UseEditorCallback = GUILayout.Toggle(_button.UseEditorCallback, new GUIContent("OnClick"), _buttonStyle);
            DrawCallback(_button.UseEditorCallback, nameof(_button.ButtonEditorCallback.Event));
            
            serializedObject.ApplyModifiedProperties();
        }
        
        private void DrawCallback(bool draw, string propertyName)
        {
            if (draw)
                EditorGUILayout.PropertyField(_buttonCallbacksProperty.FindPropertyRelative(propertyName));
        }
    }
}
