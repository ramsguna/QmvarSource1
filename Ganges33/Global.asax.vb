
Public Class Global_asax
    Inherits HttpApplication

    Sub Application_Start(sender As Object, e As EventArgs)
        ' アプリケーションの起動時に呼び出されます
        log4net.Config.XmlConfigurator.Configure()

    End Sub
End Class