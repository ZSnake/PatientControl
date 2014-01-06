// Type: System.ServiceModel.Channels.ISession
// Assembly: System.ServiceModel, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\System.ServiceModel.dll

namespace System.ServiceModel.Channels
{
  /// <summary>
  /// Define la interfaz para establecer un contexto compartido entre las partes que intercambian los mensajes proporcionando una id. para la sesión de comunicaciones.
  /// </summary>
  [__DynamicallyInvokable]
  public interface ISession
  {
    /// <summary>
    /// Obtiene el identificador que identifica de forma exclusiva la sesión.
    /// </summary>
    /// 
    /// <returns>
    /// El identificador que identifica de forma exclusiva la sesión.
    /// </returns>
    [__DynamicallyInvokable]
    string Id { [__DynamicallyInvokable] get; }
  }
}
