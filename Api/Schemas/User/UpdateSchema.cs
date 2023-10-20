namespace Parking_Intelligence_Api.Schemas.User
{
    public class UpdateSchema
    {
        public UpdateSchema(string? fieldEdit, string? password, string? email, string? value)
        {
            this.FieldEdit = fieldEdit;
            this.Password = password;
            this.Email = email;
            this.Value = value;
        }

        public string? FieldEdit { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? Value { get; set; }
    }
}
