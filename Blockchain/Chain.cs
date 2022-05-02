using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Blockchain;

public class Chain
{
  public Dictionary<int, string> Index { get; set; }
  public List<Block> Blockchain { get; set; }

  public Chain()
  {
    var genesisHash = CreateGenesisHash();
    Index = new Dictionary<int, string> {{0, genesisHash}};
    Blockchain = new List<Block> {default!};
  }

  public void CreateBlock(Data data)
  {
    var timestamps = DateTime.Now.Ticks;
    var dataJson = JsonSerializer.Serialize(data);
    var hash = GenerateHash(timestamps, dataJson);
    var block = new Block(timestamps, hash, Blockchain.Count == 1 ? Index[0] : Blockchain[^1].Hash, data);
    Blockchain.Add(block);
    Index.Add(Blockchain.Count - 1, hash);
  }

  public void Print()
  {
    foreach (var block in Blockchain)
    {
      Console.WriteLine(block);
    }
  }

  private static string GenerateHash(long timestamps, string data)
  {
    var sha256 = SHA256.Create();
    var seed = 0;
    while (true)
    {
      var dataHash = Encoding.UTF8.GetBytes($"{seed} {timestamps} {data} ");
      var hashBytes = sha256.ComputeHash(dataHash);
      var hash = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
      if (hash[..3] == "ad0")
        return hash;
      seed++;
    }
  }

  private static string CreateGenesisHash()
  {
    var timestamps = new DateTime(2019, 01, 01).Ticks;
    return GenerateHash(timestamps, "{}");
  }

  public bool CheckIntegrity()
  {
    var previous = Index[0];
    for (var i = 1; i < Blockchain.Count; i++)
    {
      var hash = Blockchain[i].PreviousBlock;
      if (previous != hash)
        return false;
      previous = Blockchain[i].Hash;
    }
    return true;
  }
}