using System.Security.Cryptography;
using System.Text;

Console.WriteLine("Salt:");
string hashKey = Console.ReadLine() ?? "";
Console.WriteLine("Hash This:");
string password = Console.ReadLine() ?? "";
Console.WriteLine("Hashed Key:");
using (HMACSHA256 hmac = new(Encoding.UTF8.GetBytes(hashKey)))
{
    byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
    byte[] hashBytes = hmac.ComputeHash(passwordBytes);
    string hexString = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
    Console.WriteLine(hexString);
}