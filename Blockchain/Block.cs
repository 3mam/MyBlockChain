namespace Blockchain;

public record Block(
  long Timestamps,
  string Hash,
  string PreviousBlock,
  Data Data
);