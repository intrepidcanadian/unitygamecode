using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Solana.Unity.Metaplex.NFT.Library;
using Solana.Unity.Metaplex.Utilities;
using Solana.Unity.Programs;
using Solana.Unity.Rpc.Builders;
using Solana.Unity.Rpc.Models;
using Solana.Unity.Rpc.Types;
using Solana.Unity.SDK;
using Solana.Unity.Wallet;
using UnityEngine;


public class NftMint : MonoBehaviour
{
    public Toast toastComponent;
    public LogicScript logicScript;

    public void MintNft()
    {
        StartMinting().Forget();
    }


    private async UniTask StartMinting()
    {
        Loading.StartLoading();

        // Mint and ATA
        var mint = new Account();
        var associatedTokenAccount = AssociatedTokenAccountProgram
            .DeriveAssociatedTokenAccount(Web3.Account, mint.PublicKey);

        int playerScore = logicScript.playerScore;

        string nftName = $"Hackathon Score {playerScore}";

        var metadata = new Metadata()
        {
            name = nftName,
            symbol = "BW",
            uri = "https://raw.githubusercontent.com/intrepidcanadian/assetsforbc/main/bcnft",
            sellerFeeBasisPoints = 0,
            creators = new List<Creator> { new(Web3.Account.PublicKey, 100, true) }
        };

        // Prepare the transaction
        var blockHash = await Web3.Rpc.GetLatestBlockHashAsync();
        var minimumRent = await Web3.Rpc.GetMinimumBalanceForRentExemptionAsync(TokenProgram.MintAccountDataSize);
        var transaction = new TransactionBuilder()
            .SetRecentBlockHash(blockHash.Result.Value.Blockhash)
            .SetFeePayer(Web3.Account)
            .AddInstruction(
                SystemProgram.CreateAccount(
                    Web3.Account,
                    mint.PublicKey,
                    minimumRent.Result,
                    TokenProgram.MintAccountDataSize,
                    TokenProgram.ProgramIdKey))
            .AddInstruction(
                TokenProgram.InitializeMint(
                    mint.PublicKey,
                    0,
                    Web3.Account,
                    Web3.Account))
            .AddInstruction(
                AssociatedTokenAccountProgram.CreateAssociatedTokenAccount(
                    Web3.Account,
                    Web3.Account,
                    mint.PublicKey))
            .AddInstruction(
                TokenProgram.MintTo(
                    mint.PublicKey,
                    associatedTokenAccount,
                    1,
                    Web3.Account))
            .AddInstruction(MetadataProgram.CreateMetadataAccount(
                PDALookup.FindMetadataPDA(mint),
                mint.PublicKey,
                Web3.Account,
                Web3.Account,
                Web3.Account.PublicKey,
                metadata,
                TokenStandard.NonFungible,
                true,
                true,
                null,
                metadataVersion: MetadataVersion.V3))
            .AddInstruction(MetadataProgram.CreateMasterEdition(
                    maxSupply: null,
                    masterEditionKey: PDALookup.FindMasterEditionPDA(mint),
                    mintKey: mint,
                    updateAuthorityKey: Web3.Account,
                    mintAuthority: Web3.Account,
                    payer: Web3.Account,
                    metadataKey: PDALookup.FindMetadataPDA(mint),
                    version: CreateMasterEditionVersion.V3
                )
            );
        var tx = Transaction.Deserialize(transaction.Build(new List<Account> { Web3.Account, mint }));

        // Sign and Send the transaction
        var res = await Web3.Wallet.SignAndSendTransaction(tx);

        // Show Confirmation
        if (res?.Result != null)
        {
            await Web3.Rpc.ConfirmTransaction(res.Result, Commitment.Confirmed);
            Debug.Log("Minting succeeded, see transaction at https://explorer.solana.com/tx/"
                      + res.Result + "?cluster=" + Web3.Wallet.RpcCluster.ToString().ToLower());
            toastComponent.ShowToast("NFT Minted Successfully! See transaction at https://explorer.solana.com/tx/"
                      + res.Result + "?cluster=" + Web3.Wallet.RpcCluster.ToString().ToLower(), 3);
        }
        else
        {
            toastComponent.ShowToast("NFT Minting Failed. Sign-in with Wallet", 3);
        }

        Loading.StopLoading();
    }
}