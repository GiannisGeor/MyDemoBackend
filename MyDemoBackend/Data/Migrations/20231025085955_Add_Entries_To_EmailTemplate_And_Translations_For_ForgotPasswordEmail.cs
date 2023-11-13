using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class Add_Entries_To_EmailTemplate_And_Translations_For_ForgotPasswordEmail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"
            INSERT into [Application].[EmailTemplate] ([Subject], [Description], [TextString], [HasHtml], [EmailType]) 
            VALUES            
            (N'forgot_password_email_subject', 
			 N'Forgot Password Email', 
             N'Hello {{#UserEmail#}}, 
              Do not worry a bit!
              Please click on the following link to reset your password: {{#PasswordResetLink#}}
              If you did not request a password reset you can safely ignore this message.
              
              Thank you!', 1, 2);

			 Insert into [Application].Translations ([key],[languageIdentifier],[TranslatedText])
			 Values
			 ('forgot_password_email_subject','EN','Password Reset'),
			 ('forgot_password_email_subject','DE','Passwort Zurucksetzen'); 
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"
            delete from [Application].EmailTemplate where emailtype = 2;
            delete from [Application].Translations where [key]='forgot_password_email_subject' and ([languageIdentifier] = 'DE' or [languageIdentifier] = 'EN');
            ");
        }
    }
}
