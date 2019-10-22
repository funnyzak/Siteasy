DELETE FROM [@tbprefix_variables] WHERE [key] =  N'ctaa_connector_config'
GO

INSERT [@tbprefix_variables] ([name], [likeid], [key], [desc], [value], [system]) VALUES (N'青少儿服务平台连接器配置', N'插件.青少儿考级服务', N'ctaa_connector_config', N'青少儿服务平台的连接配置', N'{"apiHost":" http://localhost:9779","id":"48931735830","secretKey":"0af89e84b77e49da9ab3894e7b7d67aa"}', 1)