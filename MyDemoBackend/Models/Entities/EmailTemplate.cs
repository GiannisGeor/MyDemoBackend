using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    public class EmailTemplate
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Subject { get; set; }

        public string Description { get; set; }

        public string TextString { get; set; }

        public bool HasHtml { get; set; }

        public EmailType EmailType { get; set; }

    }
    public enum EmailType
    {
        ConfirmEmail = 1,
        ForgotPassword = 2
    }
}
