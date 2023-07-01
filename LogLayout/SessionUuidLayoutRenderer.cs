using System.Text;
using NLog;
using NLog.LayoutRenderers;

namespace LogLayout;

[LayoutRenderer("sessionuuid")]
public class SessionUuidLayoutRenderer : LayoutRenderer
{
    private static readonly string SessionUuid = Guid.NewGuid().ToString();
    protected override void Append(StringBuilder builder, LogEventInfo logEvent)
    {
        builder.Append(SessionUuid);
    }
}