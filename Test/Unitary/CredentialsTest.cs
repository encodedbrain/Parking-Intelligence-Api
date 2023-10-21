using Parking_Intelligence_Api.Models;

namespace Test.Unitary;

public class CredentialsTest
{
    [Fact]
    public void CpfTestWithSpecialCharacters()
    {
        var validate = new User();

        var expected = validate.ValidateCpf("869.224.285-36");

        Assert.True(expected);
    }

    [Fact]
    public void CpfTestNoSpecialCharacters()
    {
        var validate = new User();

        var expected = validate.ValidateCpf("86922428536");

        Assert.True(expected);
    }

    [Fact]
    public void PasswordTest()
    {
        var validate = new User();

        var expected = validate.ValidatePassword("Test178@");


        Assert.True(expected);
    }

    [Fact]
    public void NameTest()
    {
        var validate = new User();

        var expected = validate.ValidateName("helena");

        Assert.True(expected);
    }

    [Fact]
    public void NameTestRepeatCharacter()
    {
        var validate = new User();

        var expected = validate.VerifyCharaterRepeat("reinaldo");

        Assert.True(expected);
    }

    [Fact]
    public void PhoneTest()
    {
        var validate = new User();

        var expected = validate.ValidatePhone("21999542145");

        Assert.True(expected);
    }

    [Fact]
    public void EmailTest()
    {
        var validate = new User();

        var expected = validate.VaLidateEmail("teste@gmail.com");

        Assert.True(expected);
    }
}