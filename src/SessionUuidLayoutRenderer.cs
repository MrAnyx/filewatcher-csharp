using System.Text;
using NLog;
using NLog.LayoutRenderers;

[LayoutRenderer("sessionuuid")]
class SessionUuidLayoutRenderer : LayoutRenderer
{
    private static readonly string SessionUuid = Guid.NewGuid().ToString();
    protected override void Append(StringBuilder builder, LogEventInfo logEvent)
    {
        builder.Append(SessionUuid);
    }
}