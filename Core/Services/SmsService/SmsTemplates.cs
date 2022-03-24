namespace Common.Core.Services.SmsService
{
    public static class SmsTemplates
    {
        private static string ProcessPhoneNumber(string phone, string countryCode = "+234")
        {
            var resultingPhone = phone;
            if (phone.StartsWith("0"))
            {
                resultingPhone = countryCode + phone[1..];
            }
            else
            {
                resultingPhone = countryCode + phone;
            }
            return resultingPhone;
        }
    }
}
