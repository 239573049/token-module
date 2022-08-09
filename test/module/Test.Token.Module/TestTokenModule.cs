using Token.Module.Attributes;

namespace Test.Token.Module;

[DependOn(typeof(TestApplicationModule))]
public class TestTokenModule : TokenModule
{
    
}