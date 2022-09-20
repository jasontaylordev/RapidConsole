using Microsoft.Extensions.Hosting;
using Spectre.Console.Cli;

namespace AwesomeConsole.Services;

public sealed class TypeResolver : ITypeResolver, IDisposable
{
   private readonly IHost _host;

   public TypeResolver(IHost provider)
   {
      _host = provider ?? throw new ArgumentNullException(nameof(provider));
   }

   public object? Resolve(Type? type)
   {
      return type != null ? _host.Services.GetService(type) : null;
   }

   public void Dispose()
   {
      _host.Dispose();
   }
}