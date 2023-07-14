using System.Security.Cryptography;
using System.Text;

string fullPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\simple-hash-salt.key";
string hashKey = "";
bool keySaved = false;

static string getInputFromUser(string question)
{
    Console.Write(question);
    return Console.ReadLine() ?? "";
}

string useNewKey = getInputFromUser("Use new key yes/no?\n> ");
if (useNewKey.ToLower().Trim() == "no")
{
    try
    {
        hashKey = File.ReadAllText(fullPath).Trim() ?? "";
        keySaved = true;
    }
    catch
    {
        Console.WriteLine("No key found!");
    }
}

if (hashKey == "")
{
    hashKey = getInputFromUser("Salt\n> ");
}

string password = getInputFromUser("Hash This\n> ");

Console.Write("Hashed Key\n> ");
using (HMACSHA256 hmac = new(Encoding.UTF8.GetBytes(hashKey)))
{
    byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
    byte[] hashBytes = hmac.ComputeHash(passwordBytes);
    string hexString = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
    Console.WriteLine(hexString);
}

if (!keySaved)
{
    Console.Write("Save secret yes/no?\n> ");
    string yesNo = Console.ReadLine() ?? "";
    if (yesNo.ToLower().Trim() == "yes")
    {
        using (StreamWriter writer = new(fullPath))
        {
            writer.WriteLine(hashKey);
        }
    }
}
