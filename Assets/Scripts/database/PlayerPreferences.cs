using System;
using System.IO;
using System.Security.Cryptography;
using UnityEngine;
using System.Text;
public class PlayerPreferences
{
    private string PlayerUsernameValue = "Username";
    static readonly string PasswordHash = "&~JyP5;%tm(<%mB:";
    static readonly string SaltKey = "nNe+/Z8$k?tG6_C$";
    static readonly string VIKey = "`M~9P}tjuS=q.AC3";


    private static Aes myAes;
    public PlayerPreferences()
    {


    }
    public void setUsername(string email)
    {
        PlayerPrefs.SetString(PlayerUsernameValue, Encrypt(email));
        PlayerPrefs.Save();
    }

    public string getUsername()
    {
        if (PlayerPrefs.HasKey(PlayerUsernameValue))
        {
            return Decrypt(PlayerPrefs.GetString(PlayerUsernameValue));
        }
        return null;
    }

    public string Encrypt(string plainText)
    {
        byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

        byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
        var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros };
        var encryptor = symmetricKey.CreateEncryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));

        byte[] cipherTextBytes;

        using (var memoryStream = new MemoryStream())
        {
            using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
            {
                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                cryptoStream.FlushFinalBlock();
                cipherTextBytes = memoryStream.ToArray();
                cryptoStream.Close();
            }
            memoryStream.Close();
        }
        return Convert.ToBase64String(cipherTextBytes);
    }

    public string Decrypt(string encryptedText)
    {
        byte[] cipherTextBytes = Convert.FromBase64String(encryptedText);
        byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
        var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.None };

        var decryptor = symmetricKey.CreateDecryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));
        var memoryStream = new MemoryStream(cipherTextBytes);
        var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
        byte[] plainTextBytes = new byte[cipherTextBytes.Length];

        int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
        memoryStream.Close();
        cryptoStream.Close();
        return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount).TrimEnd("\0".ToCharArray());
    }

    public void deleteAll()
    {
        PlayerPrefs.DeleteAll();
    }
}
