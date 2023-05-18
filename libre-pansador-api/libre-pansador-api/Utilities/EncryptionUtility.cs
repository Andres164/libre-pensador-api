using System.Security.Cryptography;
using System.Text;

public static class EncryptionUtility
{
    private static string? Key { get; set; }

    public static void Initialize(string key)
    {
        if (EncryptionUtility.Key != null)
            throw new InvalidOperationException("The encryption key has already been initialized");
        if (key.Length * 8 != 256)
            throw new ArgumentException("Invalid key length. Key must be 32 characters long.", nameof(key));
        EncryptionUtility.Key = key;
    }

    public static string? Encrypt(string? plainText)
    {
        if (EncryptionUtility.Key == null)
            throw new InvalidOperationException("The encryption key hasn't been initialized");
        if (plainText == null)
            return null;

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
                //DEBUGGING
                Console.WriteLine($"(Encryption)Encrypted text: {Convert.ToBase64String(iv) + '-' + Convert.ToBase64String(encrypted)}");
                ////////////
                return Convert.ToBase64String(iv) + '-' + Convert.ToBase64String(encrypted);
            }
        }
    }

    public static string? Decrypt(string? encryptedText)
    {
        if (EncryptionUtility.Key == null)
            throw new InvalidOperationException("The encryption key hasn't been initialized");
        if (encryptedText == null)
            return null;

        var parts = encryptedText.Split('-');
        // DEBUGGING
        Console.WriteLine($"(Decryption)Encrypted text: {encryptedText}");
        Console.WriteLine($"parts length: {parts.Length}");
        ////////////
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
