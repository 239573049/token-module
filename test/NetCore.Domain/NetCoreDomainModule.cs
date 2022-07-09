using Token.Module;
using Token.Module.Attributes;

namespace NetCore.Domain;

[RunOrder(1)]
[DependOn]
public class NetCoreDomainModule : TokenModule
{
}