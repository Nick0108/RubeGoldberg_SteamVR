/*#if UNITY_EDITOR 
using UnityEditor;
#endif 

public class ControllerInspector : Editor {

	[CustomEditor(typeof(ControllerInputManager))]  
    public class TestInspector : Editor  
    {  
        private SerializedObject obj;  
        private ControllerInputManager controllerInputManager;  

        //private SerializedProperty hand;
        //LeftHand
        private SerializedProperty TeleporterTargetObject;
        private SerializedProperty Player;
        private SerializedProperty TeleportLineRender;
        public SerializedProperty TeleporterLayer;
        //RightHand
        private SerializedProperty objectMenuManager;
        //Both hand
        private SerializedProperty throwForce;

        void OnEnable()  
        {  
            obj = new SerializedObject(target);
            //LeftHand
            TeleporterTargetObject = obj.FindProperty("TeleporterTargetObject");
            Player = obj.FindProperty("Player");
            TeleportLineRender = obj.FindProperty("TeleportLineRender");
            TeleporterLayer = obj.FindProperty("TeleporterLayer");
            //RightHand
            objectMenuManager = obj.FindProperty("objectMenuManager");
            //Both hand
            throwForce = obj.FindProperty("throwForce");
        }  
      
        public override void OnInspectorGUI()  
        {
            controllerInputManager = (ControllerInputManager)target;
            controllerInputManager.forHand = (ControllerInputManager.Hand)EditorGUILayout.EnumPopup("forHand-", controllerInputManager.forHand);  
  
            if (controllerInputManager.forHand == ControllerInputManager.Hand.LeftHand)  
            {  
                EditorGUILayout.PropertyField(TeleporterTargetObject);
                EditorGUILayout.PropertyField(Player);
                EditorGUILayout.PropertyField(TeleportLineRender);
                EditorGUILayout.PropertyField(TeleporterLayer);
            }
            if (controllerInputManager.forHand == ControllerInputManager.Hand.RightHand)
            {
                EditorGUILayout.PropertyField(objectMenuManager);
            }
            EditorGUILayout.PropertyField(throwForce);
        }  
    }  
}
*/