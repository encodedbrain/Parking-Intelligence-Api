namespace Parking_Intelligence_Api.Schemas
{
    public class UpdateSchema
    {
        public UpdateSchema(string? fieldEdit, string? password, string? email, string? value)
        {
            this.fieldEdit = fieldEdit;
            this.password = password;
            this.email = email;
            this.value = value;
        }

        public string? fieldEdit { get; set; }
        public string? password { get; set; }
        public string? email { get; set; }
        public string? value { get; set; }
    }
}
