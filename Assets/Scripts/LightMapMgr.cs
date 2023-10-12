using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/*
    Kód hrdě yoinknutý z https://gist.github.com/DevClicky/ce6b336b27dbfde8fa8751641405fad6
    a lehce modifikován pro moje potřeby
*/

public class LightMapMgr : MonoBehaviour {
    [Header("Lightmaps")]
	public Texture2D[] darkLightmapDir, darkLightmapColor;
	public Texture2D[] brightLightmapDir, brightLightmapColor;
	
    [Header("Materials & Game Objects")]
    public Material[] lightMaterials;
    public GameObject[] lights;
    public GameObject yeeterPlushie; // Yeeterův plyšák má debilní shader kterej pořád svítí i bez lightmapy

    public InputActionMap map;

	private LightmapData[] darkLightmap, brightLightmap;
	
    void Start() {
		List<LightmapData> dlightmap = new List<LightmapData>();
		
		for(int i = 0; i < darkLightmapDir.Length; i++) {
			LightmapData lmdata = new LightmapData();
			
   			lmdata.lightmapDir = darkLightmapDir[i];
   			lmdata.lightmapColor = darkLightmapColor[i];
			
			dlightmap.Add(lmdata);
		}
		
		darkLightmap = dlightmap.ToArray();
		
		List<LightmapData> blightmap = new List<LightmapData>();
		
		for(int i = 0; i < brightLightmapDir.Length; i++) {
			LightmapData lmdata = new LightmapData();
			
   			lmdata.lightmapDir = brightLightmapDir[i];
   			lmdata.lightmapColor = brightLightmapColor[i];
			
			blightmap.Add(lmdata);
		}
		
		brightLightmap = blightmap.ToArray();

        map.FindAction("toggleDark").performed += SwitchToDarkKeybind;
        map.FindAction("toggleLight").performed += SwitchToLightKeybind;
        map.Enable();
	}

    public void SwitchToDark() {
        LightmapSettings.lightmaps = darkLightmap;
        for (int i = 0; i < lightMaterials.Length; i++) {
            lights[i].GetComponent<Renderer>().material = lightMaterials[1];
        }
        yeeterPlushie.SetActive(false);
    }

    public void SwitchToLight() {
        LightmapSettings.lightmaps = brightLightmap;
        for (int i = 0; i < lightMaterials.Length; i++) {
            lights[i].GetComponent<Renderer>().material = lightMaterials[0];
        }
        yeeterPlushie.SetActive(true);
    }

    void SwitchToDarkKeybind(InputAction.CallbackContext context) {
        if (Debug.isDebugBuild) {
            SwitchToDark();
        }
    }

    void SwitchToLightKeybind(InputAction.CallbackContext context) {
        if (Debug.isDebugBuild) {
            SwitchToLight();
        }
    }
}
