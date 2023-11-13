using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class Add_Entries_To_EmailTemplate_And_Translations_For_ConfirmEmail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"
            INSERT into [Application].[EmailTemplate] ([Subject], [Description], [TextString], [HasHtml], [EmailType]) 
            VALUES            
            (N'email_confirmation_subject', 
			 N'Confirm Email', 
             N'Hello {{#UserEmail#}}, 
               Nice to meet you! 
               Please note that you need to confirm your email in order to proceed. 
               Visit the following link to confirm your email : {{#EmailConfirmationLink#}}

               If you did not sign up for an account please ignore this message. Thank you!', 1, 1);

			 Insert into [Application].[Translations] ([key],[languageIdentifier],[TranslatedText])
			 Values
			 ('email_confirmation_subject','EN','Email Confirmation'),
			 ('email_confirmation_subject','DE','Email Bestatigung'); 
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"
            delete from [Application].[EmailTemplate] where emailtype = 1;
            delete from [Application].[Translations] where [key]='email_confirmation_subject' and ([languageIdentifier] = 'DE' or [languageIdentifier] = 'EN');
            ");
        }
    }
}
