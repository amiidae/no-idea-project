using VContainer;
using VContainer.Unity;

namespace Bnny.Scripts
{
    // Question:
    // why is this ?kind of? composition root if we do not compose modules together
    // we just register modules
    // basically we are just saying - "here is the module, we have it, keep that in mind"
    // or not?
    // do we register the "provider" modules and the modules dependent on this "provider" modules together
    // so that IContainerBuilder could than build a dependency graph, consistent of both of this modules
    // and be like - "this dependent module needs this and this and this "provider" module
    // do i have them registered? yes i have them registered - lets insert them"
    // ?
    public class GameLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder) { }
    }
}
