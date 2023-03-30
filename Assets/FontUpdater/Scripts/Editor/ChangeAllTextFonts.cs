using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;
using UnityEditor;
using UnityEngine.UI;
using TMPro;
using Unity.EditorCoroutines.Editor;

namespace FontUpdater
{
    public class ChangeAllTextFonts : Editor
    {

        public TMP_FontAsset newTmpFontAsset;
        public Font newTextFont;
        public bool debugTextGameObjectsNames;


        public Scene starterScene;
        public int textsCounter = 0;


        public void ChangeAllFontsTMProScenes()
        {
            int sceneCount = EditorSceneManager.sceneCountInBuildSettings;

            string[] scenes = new string[sceneCount];
            string[] scenesNames = new string[sceneCount];

            for (int i = 0; i < sceneCount; i++)
            {
                scenes[i] = System.IO.Path.GetFullPath(SceneUtility.GetScenePathByBuildIndex(i));
                scenes[i] = scenes[i].Substring(scenes[i].IndexOf("Assets"));

                scenesNames[i] = System.IO.Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i));
            }


            //Then for each scene, add it, then activate it and do what it should
            for (int i = 0; i < sceneCount; i++)
            {
                Scene loadedScene;

                EditorSceneManager.OpenScene(scenes[i], OpenSceneMode.Additive);
                loadedScene = SceneManager.GetSceneByName(scenesNames[i]);
                SceneManager.SetActiveScene(loadedScene);


                TextMeshProUGUI[] allTmproInScene = (TextMeshProUGUI[])FindObjectsOfType(typeof(TextMeshProUGUI), true);



                foreach (TextMeshProUGUI tmpro in allTmproInScene)
                {
                    textsCounter++;
                    tmpro.font = newTmpFontAsset;

                    if (debugTextGameObjectsNames)
                        Debug.Log("Changed font for: " + tmpro.name);
                }

                EditorSceneManager.SaveScene(loadedScene);
            }

            //Now removes every scene loaded and it will look like it never happened
            foreach (string scene in scenesNames)
            {
                Scene loadedScene = EditorSceneManager.GetSceneByName(scene);

                if (loadedScene != starterScene)
                    EditorSceneManager.CloseScene(loadedScene, true);

            }


            if (!debugTextGameObjectsNames)
                Debug.Log("Changed " + textsCounter + " TMPros' fonts");

            textsCounter = 0;
            
            

        }
        public void ChangeAllFontsTMProPrefabs()
        {


            string[] temp = AssetDatabase.GetAllAssetPaths();

            foreach (string s in temp)
            {
                if (s.Contains(".prefab"))
                {
                    //Get the generic prefab object
                    Object o = AssetDatabase.LoadMainAssetAtPath(s);

                    //Get the gameobject from the prefab
                    GameObject go = (GameObject)o;

                    if (go.GetComponentInChildren<TextMeshProUGUI>())
                    {
                        //Get all TextMeshPros in the prefab
                        TextMeshProUGUI[] goTMPros = go.GetComponentsInChildren<TextMeshProUGUI>(true);


                        //Loop trough the TextMeshPros in the GameObject and change the font asset
                        foreach (TextMeshProUGUI tmpro in goTMPros)
                        {
                            textsCounter++;
                            tmpro.font = newTmpFontAsset;

                            if (debugTextGameObjectsNames)
                                Debug.Log("Changed font for: " + tmpro.name);
                        }


                        EditorUtility.SetDirty(go);
                        PrefabUtility.RecordPrefabInstancePropertyModifications(go);

                    }

                }
            }

            if (!debugTextGameObjectsNames)
                Debug.Log("Changed " + textsCounter + " TMPros' fonts");

            textsCounter = 0;

        }
        public void ChangeAllFontsLTScenes()
        {

            int sceneCount = EditorSceneManager.sceneCountInBuildSettings;

            string[] scenes = new string[sceneCount];
            string[] scenesNames = new string[sceneCount];

            for (int i = 0; i < sceneCount; i++)
            {
                scenes[i] = System.IO.Path.GetFullPath(SceneUtility.GetScenePathByBuildIndex(i));
                scenes[i] = scenes[i].Substring(scenes[i].IndexOf("Assets"));

                scenesNames[i] = System.IO.Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i));
            }

            //Then for each scene, add it, then activate it and do what it should
            for (int i = 0; i < sceneCount; i++)
            {
                Scene loadedScene;

                EditorSceneManager.OpenScene(scenes[i], OpenSceneMode.Additive);
                loadedScene = SceneManager.GetSceneByName(scenesNames[i]);
                SceneManager.SetActiveScene(loadedScene);


                Text[] allTextInScene = (Text[])FindObjectsOfType(typeof(Text), true);



                foreach (Text txt in allTextInScene)
                {
                    textsCounter++;
                    txt.font = newTextFont;

                    if (debugTextGameObjectsNames)
                        Debug.Log("Changed font for: " + txt.name);
                }

                EditorSceneManager.SaveScene(loadedScene);
            }

            //Now removes every scene loaded and it will look like it never happened
            foreach (string scene in scenesNames)
            {
                Scene loadedScene = EditorSceneManager.GetSceneByName(scene);

                if (loadedScene != starterScene)
                    EditorSceneManager.CloseScene(loadedScene, true);

            }


            if (!debugTextGameObjectsNames)
                Debug.Log("Changed " + textsCounter + " TMPros' fonts");

            textsCounter = 0;

        }
        public void ChangeAllFontsLTPrefabs()
        {

            string[] temp = AssetDatabase.GetAllAssetPaths();

            foreach (string s in temp)
            {
                if (s.Contains(".prefab"))
                {
                    //Get the generic prefab object
                    Object o = AssetDatabase.LoadMainAssetAtPath(s);

                    //Get the gameobject from the prefab
                    GameObject go = (GameObject)o;

                    if (go.GetComponentInChildren<TextMeshProUGUI>())
                    {
                        //Get all TextMeshPros in the prefab
                        Text[] goTMPros = go.GetComponentsInChildren<Text>(true);


                        //Loop trough the TextMeshPros in the GameObject and change the font asset
                        foreach (Text txt in goTMPros)
                        {
                            textsCounter++;
                            txt.font = newTextFont;

                            if (debugTextGameObjectsNames)
                                Debug.Log("Changed font for: " + txt.name);
                        }


                        EditorUtility.SetDirty(go);
                        PrefabUtility.RecordPrefabInstancePropertyModifications(go);

                    }

                }
            }

            if (!debugTextGameObjectsNames)
                Debug.Log("Changed " + textsCounter + " LEGACY TEXT' fonts");

            textsCounter = 0;

        }

    }
    public class ChangeAllTextFontsWindow : EditorWindow
    {
        [MenuItem("Fonts/Font Updater")]
        public static void ShowWindow()
        {
            GetWindow(typeof(ChangeAllTextFontsWindow));

        }

        public bool changesMade;
        //public WindowLayoutSetup layoutSetup;
        ChangeAllTextFonts changeAllTextFonts;
        private void Awake()
        {
            changeAllTextFonts = (ChangeAllTextFonts)ScriptableObject.CreateInstance(typeof(ChangeAllTextFonts));
        }
        private void OnGUI()
        {
            GUIPage1 gUIPage1 = new GUIPage1(changeAllTextFonts, this);

           // GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height/100 * 20));
            gUIPage1.TopLayout();
            // GUILayout.EndArea();
            gUIPage1.LeftLayout();




            GUILayout.Label("LEGACY TEXT AREA");
          
            changeAllTextFonts.newTextFont = (Font)EditorGUILayout.ObjectField(changeAllTextFonts.newTextFont, typeof(Font), false);

            if (GUILayout.Button("Change all Fonts in Scenes - LEGACY TEXT"))
            {
                changeAllTextFonts.starterScene = EditorSceneManager.GetActiveScene();
                if (changeAllTextFonts.newTextFont != null)
                {
                    changeAllTextFonts.ChangeAllFontsLTScenes();
                }
                else
                {
                    Debug.LogError("No Font selected!");
                }
            }
            if (GUILayout.Button("Change all Fonts in Prefabs - LEGACY TEXT"))
            {
                changeAllTextFonts.starterScene = EditorSceneManager.GetActiveScene();
                if (changeAllTextFonts.newTextFont != null)
                {
                    changeAllTextFonts.ChangeAllFontsLTPrefabs();
                }
                else
                {
                    Debug.LogError("No Font selected!");
                }
                changesMade = true;
                EditorCoroutineUtility.StartCoroutine(HideWarning(), this);

            }
            if (GUILayout.Button("Change all Fonts in Project - LEGACY TEXT"))
            {
                changeAllTextFonts.starterScene = EditorSceneManager.GetActiveScene();
                if (changeAllTextFonts.newTextFont != null)
                {
                    changeAllTextFonts.ChangeAllFontsLTScenes();
                    changeAllTextFonts.ChangeAllFontsLTPrefabs();
                }
                else
                {
                    Debug.LogError("No Font selected!");
                }


            }

            changeAllTextFonts.debugTextGameObjectsNames = EditorGUILayout.Toggle("Debug objects' names", changeAllTextFonts.debugTextGameObjectsNames);

            if (changesMade)
            {
                GUILayout.Label("Changes were made! Reload scene or re-open prefab");

            }
            

        }
        IEnumerator HideWarning()
        {
            yield return new WaitForSecondsRealtime(5.5f);
            changesMade = false;
        }
    }
    public class GUIPage1
    {
        public ChangeAllTextFontsWindow changeFontsWindow;
        public ChangeAllTextFonts changeAllTextFonts;

        public GUIPage1(ChangeAllTextFonts changeAllTextFonts, ChangeAllTextFontsWindow changeFontsWindow)
        {
            this.changeAllTextFonts = changeAllTextFonts;
            this.changeFontsWindow = changeFontsWindow;
        }
        public void TopLayout()
        {
            //GUILayout.FlexibleSpace();
            //GUILayout.BeginHorizontal();
            //GUI.DrawTexture(new Rect(0, 0, Screen.width / 100 * 5, Screen.height / 100 * 5),);
            //GUILayout.FlexibleSpace();
            GUILayout.Label("TMP AREA");

            //GUILayout.FlexibleSpace();
            //GUILayout.EndHorizontal();
            //GUILayout.FlexibleSpace();
            changeAllTextFonts.newTmpFontAsset = (TMP_FontAsset)EditorGUILayout.ObjectField(changeAllTextFonts.newTmpFontAsset, typeof(TMP_FontAsset), false);
        }
        public void LeftLayout()
        {


            if (GUILayout.Button("Change all Fonts in Scenes - TMPRO"))
            {
                changeAllTextFonts.starterScene = EditorSceneManager.GetActiveScene();
                if (changeAllTextFonts.newTmpFontAsset != null)
                {
                    changeAllTextFonts.ChangeAllFontsTMProScenes();
                }
                else
                {
                    Debug.LogError("No TMP_FontAsset selected!");
                }
            }
            if (GUILayout.Button("Change all Fonts in Prefabs - TMPRO"))
            {
                changeAllTextFonts.starterScene = EditorSceneManager.GetActiveScene();
                if (changeAllTextFonts.newTmpFontAsset != null)
                {
                    changeAllTextFonts.ChangeAllFontsTMProPrefabs();
                }
                else
                {
                    Debug.LogError("No TMP_FontAsset selected!");
                }


            }
            if (GUILayout.Button("Change all Fonts in Project - TMPRO"))
            {
                changeAllTextFonts.starterScene = EditorSceneManager.GetActiveScene();
                if (changeAllTextFonts.newTmpFontAsset != null)
                {
                    changeAllTextFonts.ChangeAllFontsTMProScenes();
                    changeAllTextFonts.ChangeAllFontsTMProPrefabs();
                }
                else
                {
                    Debug.LogError("No TMP_FontAsset selected!");
                }


            }
        }
    }
}

