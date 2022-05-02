using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Blockchain;

namespace Test;

[TestClass]
public class UnitTest1
{
  [TestMethod]
  public void TestMethod1()
  {
    var chain = new Chain();
    var data = new Data(0, 1, 1);
    chain.CreateBlock(data);
    chain.CreateBlock(data);
    chain.CreateBlock(data);
    chain.CreateBlock(data);

    Assert.IsTrue(chain.CheckIntegrity());
  }
  
}