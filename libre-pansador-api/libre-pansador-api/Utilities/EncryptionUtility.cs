using System.Security.Cryptography;
using System.Text;

public static class EncryptionUtility
{
    private static string? Key { get; set; } // Replace this with your secret key (32 characters)

    public static void Initialize(string key)
    {
        if (EncryptionUtility.Key != null)
            throw new InvalidOperationException("The encryption key has already been initialized");
        EncryptionUtility.Key = key;
    }

    public static string Encrypt(string plainText)
    {
        if (EncryptionUtility.Key == null)
            throw new InvalidOperationException("The encryption key hasn't been initialized");

        using (var aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(EncryptionUtility.Key);
            aes.GenerateIV();

            using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
            using (var msEncrypt = new MemoryStream())
            {
                using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                using (var swEncrypt = new StreamWriter(csEncrypt))
                {
                    swEncrypt.Write(plainText);
                }

                var iv = aes.IV;
                var encrypted = msEncrypt.ToArray();

                return Convert.ToBase64String(iv) + ':' + Convert.ToBase64String(encrypted);
            }
        }
    }

    public static string Decrypt(string encryptedText)
    {
        if (EncryptionUtility.Key == null)
            throw new InvalidOperationException("The encryption key hasn't been initialized");

        var parts = encryptedText.Split(':');
        if (parts.Length != 2) throw new ArgumentException("Invalid encrypted text format");

        var iv = Convert.FromBase64String(parts[0]);
        var encrypted = Convert.FromBase64String(parts[1]);

        using (var aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(EncryptionUtility.Key);
            aes.IV = iv;

            using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
            using (var msDecrypt = new MemoryStream(encrypted))
            using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
            using (var srDecrypt = new StreamReader(csDecrypt))
            {
                return srDecrypt.ReadToEnd();
            }
        }
    }
}
