using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor (typeof (uMMO_NetObject_NetworkViewSerializer_Module),true)]
public class uMMO_NetObject_NetworkViewSerializer_ModuleEditor : PropertyEditor {
	
	
	public const string NL = "\r\n";
	public const string DNL = "\r\n\r\n";	

	protected uMMO_NetObject_NetworkViewSerializer_Module instance;
	
	protected override void Initialize () {

	}
	
	public override void OnInspectorGUI () {
		EditorGUIUtility.LookLikeControls(Screen.width/1.7f);
		
		instance = (uMMO_NetObject_NetworkViewSerializer_Module) target;
		
		DrawDefaultInspector ();
		
		if (uMMO.get.showDocumentationInEditor)
			Comment ("NetworkViewSerializer modules are pretty handy for syncronizing properties thorughout the network. A couple of ready-to-use NetworkViewSerializer module come with vanilla uMMO. Those are located in " +
				"\"Assets/uMMO/prefabs/NetObject modules/\" and can quite easily be dragged-and-dropped here onto the \"networkViewSerializerMods\" property. This property resembles a list of modules, which are ALL being executed " +
				"once the function OnSerializeNetworkView is being called by Unity. This way, you can, instead of having to have all syncronization code in one class, you can split them, to have maximum flexibility. "+
				NL + "So when you previously had:"+
				DNL +
				"void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info) {" + NL +
				"   if (stream.isReading) {"+ NL +
				"  	   //reading" + NL +
				"   } else {" + NL +
				"      //writing" + NL +	
				"   }" + NL +
				"}"+ NL +
				NL + 
				"you can now use:" +DNL+
				"public class uMMO_NetObject_YOURMODNAME : uMMO_NetObject_NetworkViewSerializer_Module {" +NL+
				"   public override void onReadFromNetworkView(BitStream stream, NetworkMessageInfo info) {" +NL+
				"      //reading" +NL+
				"   }"+NL+
				"   public override void onWriteToNetworkView(BitStream stream, NetworkMessageInfo info) {" +NL+
				"      //writing" +NL+
				"   }"+NL+
				"}"
			);
	}
}
