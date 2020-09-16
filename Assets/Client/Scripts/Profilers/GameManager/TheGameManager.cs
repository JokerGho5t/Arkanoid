#if UNITY_EDITOR
using System.Collections.Generic;

using UnityEngine;
using UnityEditor;

using Sirenix.OdinInspector;
using Sirenix.Utilities.Editor;
using Sirenix.OdinInspector.Editor;

using JokerGho5t.ScriptableObjects;

public class TheGameManager : OdinMenuEditorWindow
{
    [MenuItem("Tools/The Game Manager &1")]
    public static void OpenWindow()
    {
        GetWindow<TheGameManager>().Show();
    }

    [OnValueChanged("StateChange")]
    [LabelText("Manager View")]
    [LabelWidth(100f)]
    [EnumToggleButtons]
    [ShowInInspector]
    private ManagerState managerState = ManagerState.GameProfiler; //used to control what is shown in the editor window
    private bool reBuildTree = false;

    private readonly int SizePage = 2;

    private DrawGameProfiler drawProfiler = new DrawGameProfiler();

    //these are all scriptable object based
    private DrawSelected<LevelData> drawLevels = new DrawSelected<LevelData>();

    //paths to SOs in project
    private string levelsPath = Project.GameManagerPath + "Levels";

    //set the path for each state so that new SOs of that type can be created
    protected override void Initialize()
    {
        drawLevels.SetPath(levelsPath);

        drawProfiler.SelectProfiler();
    }

    //called when the enum "manager state" is changed
    //might need more in here for later additions?
    private void StateChange()
    {
        reBuildTree = true;
    }

    //used to place title and enum buttons (Target 0) above Odin Menu Tree
    //only used in some windows
    protected override void OnGUI()
    {
        //update menu tree on type change
        if (reBuildTree && Event.current.type == EventType.Layout)
        {
            ForceMenuTreeRebuild();
            reBuildTree = false;
        }

        SirenixEditorGUI.Title("The Game Manager", "Because Every Hobby Game is Overscoped", TextAlignment.Center, true);
        UnityEditor.EditorGUILayout.Space();
        UnityEditor.EditorGUILayout.Space();

        switch (managerState)
        {
            case ManagerState.Levels:
                DrawEditor(SizePage);
                break;
            default:
                break;
        }

        UnityEditor.EditorGUILayout.Space();
        base.OnGUI();
    }

    //targets are separate classes that wrap the main class
    //the idea here was to allow the addition of buttons and other functions
    //for use in the editor window that aren't needed in the class itself
    protected override IEnumerable<object> GetTargets()
    {
        List<object> targets = new List<object>();
        targets.Add(drawProfiler); //target 0
        targets.Add(drawLevels); //target 1
        targets.Add(base.GetTarget()); //target 2

        return targets;
    }

    protected override void DrawEditors()
    {
        switch (managerState)
        {
            case ManagerState.GameProfiler:
                DrawEditor(SizePage);
                break;
            case ManagerState.Levels:
                drawLevels.SetSelected(this.MenuTree.Selection.SelectedValue);
                break;
            default:
                break;
        }

        //draw editor based on enum value
        DrawEditor((int)managerState);
    }

    //control over Odin Menu Tree
    protected override void DrawMenu()
    {
        switch (managerState)
        {
            case ManagerState.Levels:
                base.DrawMenu();
                break;
            default:
                break;
        }
    }

    //I'm building multiple trees depending on what "state" is selected
    protected override OdinMenuTree BuildMenuTree()
    {
        var tree = new OdinMenuTree();
        tree.Selection.SupportsMultiSelect = false;

        switch (managerState)
        {
            case ManagerState.Levels:
                tree.AddAllAssetsAtPath("Levels Data", levelsPath, typeof(LevelData));
                break;
            default:
                break;
        }
        return tree;
    }

    public enum ManagerState
    {
        GameProfiler,
        Levels
    }
}

//Used to draw the current object that is selected in the Menu Tree
//Look at me using generics ;)
public class DrawSelected<T> where T : ScriptableObject
{
    //[Title("@property.name")]
    [InlineEditor(InlineEditorObjectFieldModes.CompletelyHidden)]
    public T selected;

    [LabelWidth(100)]
    [PropertyOrder(-1)]
    [BoxGroup("CreateNew")]
    [HorizontalGroup("CreateNew/Horizontal")]
    public string nameForNew = "";

    private string path = "";

    [HorizontalGroup("CreateNew/Horizontal")]
    [GUIColor(0.7f, 0.7f, 1f)]
    [Button]
    public void CreateNew()
    {
        if (nameForNew == "")
            return;

        T newItem = ScriptableObject.CreateInstance<T>();

        if (path == "")
            path = "Assets/Client/";

        AssetDatabase.CreateAsset(newItem, path + "\\" + nameForNew + ".asset");
        AssetDatabase.SaveAssets();

        nameForNew = "";
    }

    [HorizontalGroup("CreateNew/Horizontal")]
    [GUIColor(1f, 0.7f, 0.7f)]
    [Button]
    public void DeleteSelected()
    {
        if (selected != null)
        {
            string path = AssetDatabase.GetAssetPath(selected);
            AssetDatabase.DeleteAsset(path);
            AssetDatabase.SaveAssets();
        }
    }

    public void SetSelected(object item)
    {
        //ensure selection is of the correct type
        var attempt = item as T;
        if (attempt != null)
            this.selected = attempt;
    }

    public void SetPath(string path)
    {
        this.path = path;
    }
}

public class DrawGameProfiler
{
    [InlineEditor(InlineEditorObjectFieldModes.CompletelyHidden)]
    public GameProfiler profiler;

    public void SelectProfiler()
    {
        profiler = AssetDatabase.LoadAssetAtPath(Project.GameManagerPath + "GameProfiller.asset", typeof(GameProfiler)) as GameProfiler;

        if (profiler == null)
        {
            profiler = ScriptableObject.CreateInstance<GameProfiler>();
            AssetDatabase.CreateAsset(profiler, Project.GameManagerPath + "GameProfiller.asset");
            AssetDatabase.SaveAssets();
        }
    }
}

public static class Project
{
    public const string GameManagerPath = "Assets/Client/Resources/GameManager/";
}
#endif


