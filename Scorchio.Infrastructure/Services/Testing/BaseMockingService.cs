// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the BaseMockingService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Scorchio.Infrastructure.Services.Testing
{
    using Scorchio.VisualStudio.Services;
    using Scorchio.VisualStudio.Services.Interfaces;

    /// <summary>
    ///  Defines the BaseMockingService type.
    /// </summary>
    public class BaseMockingService
    {
        /// <summary>
        /// The moq.
        /// </summary>
        private const string  Moq = "Moq";

        /// <summary>
        /// The rhino mocks.
        /// </summary>
        private const string RhinoMocks = "Rhino.Mocks";

        /// <summary>
        /// The nsubstitute.
        /// </summary>
        private const string NSubstitute = "NSubstitute";

        /// <summary>
        /// Updates the project.
        /// </summary>
        /// <param name="projectService">The project service.</param>
        public void UpdateProjectReferences(IProjectService projectService)
        {
            TraceService.WriteLine("BaseMockingService::UpdateProjectReferences");

            if (this.GetType().Name.Contains(Moq) == false)
            {
                projectService.RemoveReference(Moq);
                projectService.RemoveFolderItem("Lib", Moq);
            }

            if (this.GetType().Name.Contains("RhinoMocks") == false)
            {
                projectService.RemoveReference(RhinoMocks);
                projectService.RemoveFolderItem("Lib", RhinoMocks);
            }

            if (this.GetType().Name.Contains(NSubstitute) == false)
            {
                projectService.RemoveReference(NSubstitute);
                projectService.RemoveFolderItem("Lib", NSubstitute);
            }
        }
    }
}
