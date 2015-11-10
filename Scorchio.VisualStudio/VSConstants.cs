// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the VsConstants type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Scorchio.VisualStudio
{
    /// <summary>
    /// Defines the VsConstants type.
    /// </summary>
  public static class VSConstants
  {
    /// <summary>
    /// A text document, opened with a text editor.
    /// </summary>
    public const string VsDocumentKindText = "{8E7B96A8-E33D-11D0-A6D5-00C04FB67F6A}";

    /// <summary>
    /// An HTML document. Can get the IHTMLDocument2 interface, also known as the Document Object Model (DOM).
    /// </summary>
    public const string VsDocumentKindHTML = "{C76D83F8-A489-11D0-8195-00A0C91BBEE3}";

    /// <summary>
    /// A resource file, opened with the resource editor.
    /// </summary>
    public const string VsDocumentKindResource = "{00000000-0000-0000-0000-000000000000}";

    /// <summary>
    /// A binary file, opened with a binary file editor.
    /// </summary>
    public const string VsDocumentKindBinary = "{25834150-CD7E-11D0-92DF-00A0C9138C45}";

    /// <summary>
    /// View in default viewer.
    /// </summary>
    public const string VsViewKindPrimary = "{00000000-0000-0000-0000-000000000000}";

    /// <summary>
    /// Use the view that was last used.
    /// </summary>
    public const string VsViewKindAny = "{FFFFFFFF-FFFF-FFFF-FFFF-FFFFFFFFFFFF}";

    /// <summary>
    /// View in debugger.
    /// </summary>
    public const string VsViewKindDebugging = "{7651A700-06E5-11D1-8EBD-00A0C90F26EA}";

    /// <summary>
    /// View in code editor.
    /// </summary>
    public const string VsViewKindCode = "{7651A701-06E5-11D1-8EBD-00A0C90F26EA}";

    /// <summary>
    /// View in Visual Designer (forms designer).
    /// </summary>
    public const string VsViewKindDesigner = "{7651A702-06E5-11D1-8EBD-00A0C90F26EA}";

    /// <summary>
    /// View in text editor.
    /// </summary>
    public const string VsViewKindTextView = "{7651A703-06E5-11D1-8EBD-00A0C90F26EA}";
    
    /// <summary>
    /// The Task List window.
    /// </summary>
    public const string VsWindowKindTaskList = "{4A9B7E51-AA16-11D0-A8C5-00A0C921A4D2}";
    
    /// <summary>
    /// The Toolbox.
    /// </summary>
    public const string VsWindowKindToolbox = "{B1E99781-AB81-11D0-B683-00AA00A3EE26}";
    
    /// <summary>
    /// The Call Stack window.
    /// </summary>
    public const string VsWindowKindCallStack = "{0504FF91-9D61-11D0-A794-00A0C9110051}";
    
    /// <summary>
    /// The Debugger window.
    /// </summary>
    public const string VsWindowKindThread = "{E62CE6A0-B439-11D0-A79D-00A0C9110051}";
    
    /// <summary>
    /// The Debugger window.
    /// </summary>
    public const string VsWindowKindLocals = "{4A18F9D0-B838-11D0-93EB-00A0C90F2734}";
    
    /// <summary>
    /// The Debugger window.
    /// </summary>
    public const string VsWindowKindAutoLocals = "{F2E84780-2AF1-11D1-A7FA-00A0C9110051}";
    
    /// <summary>
    /// The Watch window.
    /// </summary>
    public const string VsWindowKindWatch = "{90243340-BD7A-11D0-93EF-00A0C90F2734}";
    
    /// <summary>
    /// The Properties window.
    /// </summary>
    public const string VsWindowKindProperties = "{EEFA5220-E298-11D0-8F78-00A0C9110057}";
    
    /// <summary>
    /// The Solution Explorer.
    /// </summary>
    public const string VsWindowKindSolutionExplorer = "{3AE79031-E1BC-11D0-8F78-00A0C9110057}";
    
    /// <summary>
    /// The Output window.
    /// </summary>
    public const string VsWindowKindOutput = "{34E76E81-EE4A-11D0-AE2E-00A0C90FFFC3}";
    
    /// <summary>
    /// The Object Browser window.
    /// </summary>
    public const string VsWindowKindObjectBrowser = "{269A02DC-6AF8-11D3-BDC4-00C04F688E50}";
    
    /// <summary>
    /// The Macro Explorer window.
    /// </summary>
    public const string VsWindowKindMacroExplorer = "{07CD18B4-3BA1-11D2-890A-0060083196C6}";
    
    /// <summary>
    /// The Dynamic Help window.
    /// </summary>
    public const string VsWindowKindDynamicHelp = "{66DBA47C-61DF-11D2-AA79-00C04F990343}";
    
    /// <summary>
    /// The Class View window.
    /// </summary>
    public const string VsWindowKindClassView = "{C9C0AE26-AA77-11D2-B3F0-0000F87570EE}";
    
    /// <summary>
    /// The Resource Editor.
    /// </summary>
    public const string VsWindowKindResourceView = "{2D7728C2-DE0A-45b5-99AA-89B609DFDE73}";
    
    /// <summary>
    /// The Document Outline window.
    /// </summary>
    public const string VsWindowKindDocumentOutline = "{25F7E850-FFA1-11D0-B63F-00A0C922E851}";
    
    /// <summary>
    /// The Server Explorer.
    /// </summary>
    public const string VsWindowKindServerExplorer = "{74946827-37A0-11D2-A273-00C04F8EF4FF}";
    
    /// <summary>
    /// The Command window.
    /// </summary>
    public const string VsWindowKindCommandWindow = "{28836128-FC2C-11D2-A433-00C04F72D18A}";
    
    /// <summary>
    /// The Find Symbol dialog box.
    /// </summary>
    public const string VsWindowKindFindSymbol = "{53024D34-0EF5-11D3-87E0-00C04F7971A5}";
    
    /// <summary>
    /// The Find Symbol Results window.
    /// </summary>
    public const string VsWindowKindFindSymbolResults = "{68487888-204A-11D3-87EB-00C04F7971A5}";
    
    /// <summary>
    /// The Find Replace dialog box.
    /// </summary>
    public const string VsWindowKindFindReplace = "{CF2DDC32-8CAD-11D2-9302-005345000000}";
    
    /// <summary>
    /// The Find Results 1 window.
    /// </summary>
    public const string VsWindowKindFindResults1 = "{0F887920-C2B6-11D2-9375-0080C747D9A0}";
    
    /// <summary>
    /// The Find Results 2 window.
    /// </summary>
    public const string VsWindowKindFindResults2 = "{0F887921-C2B6-11D2-9375-0080C747D9A0}";
    
    /// <summary>
    /// The Visual Studio IDE window.
    /// </summary>
    public const string VsWindowKindMainWindow = "{9DDABE98-1D02-11D3-89A1-00C04F688DDE}";
    
    /// <summary>
    /// A linked window frame.
    /// </summary>
    public const string VsWindowKindLinkedWindowFrame = "{9DDABE99-1D02-11D3-89A1-00C04F688DDE}";
    
    /// <summary>
    /// A Web browser window hosted in Visual Studio.
    /// </summary>
    public const string VsWindowKindWebBrowser = "{E8B06F52-6D01-11D2-AA7D-00C04F990343}";
    
    /// <summary>
    /// Represents the "AddSubProject" wizard type.
    /// </summary>
    public const string VsWizardAddSubProject = "{0F90E1D2-4999-11D1-B6D1-00A0C90F2744}";
    
    /// <summary>
    /// Represents the "AddItem" wizard type.
    /// </summary>
    public const string VsWizardAddItem = "{0F90E1D1-4999-11D1-B6D1-00A0C90F2744}";
    
    /// <summary>
    /// Represents the "NewProject" wizard type.
    /// </summary>
    public const string VsWizardNewProject = "{0F90E1D0-4999-11D1-B6D1-00A0C90F2744}";
    
    /// <summary>
    /// A miscellaneous files project.
    /// </summary>
    public const string VsProjectKindMisc = "{66A2671D-8FB5-11D2-AA7E-00C04F688DDE}";
    
    /// <summary>
    /// A project item located in the miscellaneous files folder of the solution.
    /// </summary>
    public const string VsProjectItemsKindMisc = "{66A2671E-8FB5-11D2-AA7E-00C04F688DDE}";
    
    /// <summary>
    /// A project item in the miscellaneous files folder of the solution.
    /// </summary>
    public const string VsProjectItemKindMisc = "{66A2671F-8FB5-11D2-AA7E-00C04F688DDE}";
    
    /// <summary>
    /// An unmodeled project.
    /// </summary>
    public const string VsProjectKindUnmodeled = "{67294A52-A4F0-11D2-AA88-00C04F688DDE}";
    
    /// <summary>
    /// A solution items project.
    /// </summary>
    public const string VsProjectKindSolutionItems = "{66A26720-8FB5-11D2-AA7E-00C04F688DDE}";
    
    /// <summary>
    /// A collection of items in the solution items folder of the solution.
    /// </summary>
    public const string VsProjectItemsKindSolutionItems = "{66A26721-8FB5-11D2-AA7E-00C04F688DDE}";
    
    /// <summary>
    /// A project item type in the solution.
    /// </summary>
    public const string VsProjectItemKindSolutionItems = "{66A26722-8FB5-11D2-AA7E-00C04F688DDE}";
    
    /// <summary>
    /// The <see cref="T:EnvDTE.Projects"/> collection's <see cref="P:EnvDTE.Projects.Kind"/> property returns a GUID identifying the collection of project types that it contains.
    /// </summary>
    public const string VsProjectsKindSolution = "{96410B9F-3542-4A14-877F-BC7227B51D3B}";
    
    /// <summary>
    /// The GUID that is used for a command when you call <see cref="M:EnvDTE.Commands.AddNamedCommand(EnvDTE.AddIn,System.String,System.String,System.String,System.Boolean,System.Int32,System.Object[]@,System.Int32)"/>. Each command has a GUID and an ID associated with it, and this is the GUID for all add-in created commands.
    /// </summary>
    public const string VsAddInCmdGroup = "{1E58696E-C90F-11D2-AAB2-00C04F688DDE}";
    
    /// <summary>
    /// Indicates that a solution is currently being built.
    /// </summary>
    public const string VsContextSolutionBuilding = "{ADFC4E60-0397-11D1-9F4E-00A0C911004F}";
    
    /// <summary>
    /// Indicates that the IDE is in Debugging mode.
    /// </summary>
    public const string VsContextDebugging = "{ADFC4E61-0397-11D1-9F4E-00A0C911004F}";
    
    /// <summary>
    /// Indicates that the view of the integrated development environment (IDE) is full screen.
    /// </summary>
    public const string VsContextFullScreenMode = "{ADFC4E62-0397-11D1-9F4E-00A0C911004F}";
    
    /// <summary>
    /// Indicates that the IDE is in Design view.
    /// </summary>
    public const string VsContextDesignMode = "{ADFC4E63-0397-11D1-9F4E-00A0C911004F}";
    
    /// <summary>
    /// Indicates that the integrated development environment (IDE) has no solution.
    /// </summary>
    public const string VsContextNoSolution = "{ADFC4E64-0397-11D1-9F4E-00A0C911004F}";
    
    /// <summary>
    /// Indicates that the solution has no projects.
    /// </summary>
    public const string VsContextEmptySolution = "{ADFC4E65-0397-11D1-9F4E-00A0C911004F}";
    
    /// <summary>
    /// Indicates that the solution contains only one project.
    /// </summary>
    public const string VsContextSolutionHasSingleProject = "{ADFC4E66-0397-11D1-9F4E-00A0C911004F}";
    
    /// <summary>
    /// Indicates that the solution contains multiple projects.
    /// </summary>
    public const string VsContextSolutionHasMultipleProjects = "{93694FA0-0397-11D1-9F4E-00A0C911004F}";
    
    /// <summary>
    /// Indicates that a macro is being recorded.
    /// </summary>
    public const string VsContextMacroRecording = "{04BBF6A5-4697-11D2-890E-0060083196C6}";
    
    /// <summary>
    /// Indicates that the Macro Recorder toolbar is displayed.
    /// </summary>
    public const string VsContextMacroRecordingToolbar = "{85A70471-270A-11D2-88F9-0060083196C6}";
    
    /// <summary>
    /// The unique name for the Miscellaneous files project. Can be used to index the Solution.Projects object, such as: DTE.Solution.Projects.Item(VsMiscFilesProjectUniqueName).
    /// </summary>
    public const string VsMiscFilesProjectUniqueName = "<MiscFiles>";
    
    /// <summary>
    /// The unique name for projects in the solution. Can be used to index the <see cref="T:EnvDTE.SolutionClass"/> object's <see cref="P:EnvDTE.SolutionClass.Projects"/> collection, such as: DTE.Solution.Projects.Item(VsProjectsKindSolution).
    /// </summary>
    public const string VsSolutionItemsProjectUniqueName = "<SolnItems>";
    
    /// <summary>
    /// A file in the system.
    /// </summary>
    public const string VsProjectItemKindPhysicalFile = "{6BB5F8EE-4483-11D3-8BCF-00C04F8EC28C}";
    
    /// <summary>
    /// A folder in the system.
    /// </summary>
    public const string VsProjectItemKindPhysicalFolder = "{6BB5F8EF-4483-11D3-8BCF-00C04F8EC28C}";
    
    /// <summary>
    /// Indicates that the folder in the project does not physically appear on disk.
    /// </summary>
    public const string VsProjectItemKindVirtualFolder = "{6BB5F8F0-4483-11D3-8BCF-00C04F8EC28C}";
    
    /// <summary>
    /// A subproject under the project. If returned by <see cref="P:EnvDTE.ProjectItem.Kind"/>, then <see cref="P:EnvDTE.ProjectItem.SubProject"/> returns as a <see cref="T:EnvDTE.Project"/> object.
    /// </summary>
    public const string VsProjectItemKindSubProject = "{EA6618E8-6E24-4528-94BE-6889FE16485C}";
    
      /// <summary>
    /// The CATID for the solution.
    /// </summary>
    public const string VsCATIDSolution = "{52AEFF70-BBD8-11d2-8598-006097C68E81}";
    
      /// <summary>
    /// The CATID for items in the Property window when the solution node is selected in Solution Explorer.
    /// </summary>
    public const string VsCATIDSolutionBrowseObject = "{A2392464-7C22-11d3-BDCA-00C04F688E50}";
    
      /// <summary>
    /// The CATID for the miscellaneous files project.
    /// </summary>
    public const string VsCATIDMiscFilesProject = "{610d4612-d0d5-11d2-8599-006097c68e81}";
    
    /// <summary>
    /// The CATID for the miscellaneous files project item.
    /// </summary>
    public const string VsCATIDMiscFilesProjectItem = "{610d4613-d0d5-11d2-8599-006097c68e81}";
    
    /// <summary>
    /// The CATID for generic projects — that is, projects without a specific object model.
    /// </summary>
    public const string VsCATIDGenericProject = "{610d4616-d0d5-11d2-8599-006097c68e81}";
    
    /// <summary>
    /// The CATID for documents.
    /// </summary>
    public const string VsCATIDDocument = "{610d4611-d0d5-11d2-8599-006097c68e81}";
  }
}
