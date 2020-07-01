namespace Geeks.DependencyInjection
{
    /// <summary>
    /// A DIPackage can be used to group and specify dependencies at one place (e.g. for a specific assembly) 
    /// </summary>
    public interface IDIPackage
    {
        /// <summary>
        /// This method is called by the DIContainer and should not be used manually.
        /// </summary>
        /// <param name="container">The actual DIConatiner instance</param>
        void Load(IDIContainer container);
    }
}