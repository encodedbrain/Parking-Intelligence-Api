namespace Parking_Intelligence_Api.interfaces;

public interface IUser
{
    public bool VaLidateEmail(string? email);
    public string EncryptingPassword(string? password);
    public bool ValidateName(string? name);
    public bool ValidateCpf(string? cpf);
    public string ReturnCpfFormated(string? cpf);
    public bool ValidatePhone(string? phone);
    public bool ValidatePassword(string password);
}