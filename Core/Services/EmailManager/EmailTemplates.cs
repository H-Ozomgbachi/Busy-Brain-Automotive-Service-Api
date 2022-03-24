using Common.Data.Domain;
using EmailService;
using System;

namespace Common.Core.Services.EmailManager
{
    public static class EmailTemplates
    {
        public static Message PasswordResetEmail(User user)
        {
            string dateTokenExpires = DateTime.Now.AddMinutes(5).ToString("yyyy-MM-dd*hh:mm:ss");

            var message = new Message(new string[] { $"{user.Username}" }, "Forgot your password?", $"<h4>Dear {user.FirstName} {user.LastName}, </h4> <p>Did you forget your password ? Click on the button below to reset it.</p> <br/> <a href={$"www.thehaulagehub.com/reset-password/{user.PasswordResetToken}+{user.Id}+{dateTokenExpires}"} style={"background-color: blue;color: white;padding: 15px 25px;text-decoration: none; display: block; width: 50px; margin: 5px auto;"}>Reset Password</a> <br/> <p>If you did not ask to reset your password, please ignore this email and nothing will change.</p>", null);

            return message;
        }
    }
}
