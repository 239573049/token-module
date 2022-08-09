using Token.EventBus;
using Token.Module;
using Token.Module.Attributes;

namespace Test.Token.EventBus;

[DependOn(typeof(TokenEventBusModule))]
public class TestTokenEventBusModule : TokenModule
{
}