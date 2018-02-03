INSERT INTO [dbo].[NotificationTemplates] ([Id], [TemplateType], [Lang], [TemplateHtml], [TemplatePlainText], [TemplateSubject], [TemplateFromEmail], [TemplateFromName], [MediaType], [IsValid], [ModifiedDateUtc], [AddedDateUtc]) VALUES ('f05d48d3-7287-499c-93e7-46b7ae52960d', N'ForgotPassword', N'en', N'&lt;div style=\"padding-bottom:10px;\"&gt;
        &lt;strong&gt;Dear {0} ,&lt;/strong&gt;
    &lt;/div&gt; &lt;br /&gt; 
    &lt;div style=\"padding-bottom:10px;\"&gt;
        Thank you for requesting a new password, please click on the
        below link to complete your your request: &lt;a href="{1}"&gt;{1}&lt;/a&gt;
    &lt;/div&gt; &lt;br/&gt; 
    &lt;div style=\"padding-bottom:10px;\"&gt;
        CommiHub - &lt;strong&gt;Please do not reply to this automated email&lt;/strong&gt;
    &lt;/div&gt;', NULL, N'CommiHub Password Reset', N'dsasu1@gmail.com', N'CommiHub', N'Email', 1, '2017-09-28 16:10:23.200', '2017-09-28 16:10:23.200')
INSERT INTO [dbo].[NotificationTemplates] ([Id], [TemplateType], [Lang], [TemplateHtml], [TemplatePlainText], [TemplateSubject], [TemplateFromEmail], [TemplateFromName], [MediaType], [IsValid], [ModifiedDateUtc], [AddedDateUtc]) VALUES ('38b71e55-6a20-4248-9abb-557534917357', N'RegisterConfirmation', N'en', N'&lt;div style=\"padding-bottom:10px;\"&gt;
        &lt;strong&gt;Dear {0} , Welcome to CommiHub&lt;/strong&gt;
    &lt;/div&gt; &lt;br /&gt; 
    &lt;div style=\"padding-bottom:10px;\"&gt;
        Thank you for your registration, please click on the
        below link to complete your registration: &lt;a href="{1}"&gt;{1}&lt;/a&gt;
    &lt;/div&gt; &lt;br/&gt; Copy the link and paste in your browser if clicking do not work. &lt;br/&gt;
    &lt;div style=\"padding-bottom:10px;\"&gt;
        CommiHub - &lt;strong&gt;Please do not reply to this automated email&lt;/strong&gt;
    &lt;/div&gt;', NULL, N'CommiHub Registration Confirmation', N'dsasu1@gmail.com', N'CommiHub', N'Email', 1, '2017-09-21 00:58:59.550', '2017-09-21 00:58:59.550')
