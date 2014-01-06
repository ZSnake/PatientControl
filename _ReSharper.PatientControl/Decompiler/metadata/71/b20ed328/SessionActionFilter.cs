// Type: System.ServiceModel.Security.SessionActionFilter
// Assembly: System.ServiceModel, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\System.ServiceModel.dll

using System.Runtime;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace System.ServiceModel.Security
{
  internal class SessionActionFilter : HeaderFilter
  {
    [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    public SessionActionFilter(SecurityStandardsManager standardsManager, params string[] actions);
    public override bool Match(Message message);
  }
}
