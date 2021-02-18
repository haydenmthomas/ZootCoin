using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using EllipticCurve;

namespace ZootCoin
{
    class Program
    {
        static void Main(string[] args)
        {
            PrivateKey key1 = new PrivateKey();
            PublicKey wallet1 = key1.publicKey();

            PrivateKey key2 = new PrivateKey();
            PublicKey wallet2 = key2.publicKey();

            Blockchain zootcoin = new Blockchain(2, 100);

            Console.WriteLine("Start the miner.");
            zootcoin.MinePendingTransactions(wallet1);
            Console.WriteLine("\nBalance of wallet1 is: $" + zootcoin.GetBalanceOfWallet(wallet1).ToString());

            Transaction tx1 = new Transaction(wallet1, wallet2, 10);
            tx1.SignTransaction(key1);
            zootcoin.addPendingTransaction(tx1);
            Console.WriteLine("Start the miner.");
            zootcoin.MinePendingTransactions(wallet2);
            Console.WriteLine("\nBalance of wallet1 is: $" + zootcoin.GetBalanceOfWallet(wallet1).ToString());
            Console.WriteLine("\nBalance of wallet2 is: $" + zootcoin.GetBalanceOfWallet(wallet2).ToString());

            string blockJSON = JsonConvert.SerializeObject(zootcoin, Formatting.Indented);
            Console.WriteLine(blockJSON);

            //zootcoin.GetLatestBlock().PreviousHash = "12345";

            if (zootcoin.IsChainValid())
            {
                Console.WriteLine("Blockchain is valid");
            }
            else
            {
                Console.WriteLine("Blockchain is not valid");
            }
        }
    }

}
