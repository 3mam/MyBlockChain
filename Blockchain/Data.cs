namespace Blockchain;

public record Data(
  ulong FromWallet,
  ulong ToWallet,
  double Amount
);