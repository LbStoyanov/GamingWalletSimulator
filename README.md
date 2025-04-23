# Gaming Wallet Simulator

A .NET Core console application that simulates a gaming wallet, supporting deposits, withdrawals, and betting with win/loss probabilities.

## Description

This application simulates a simple gaming wallet system where users can:

- Deposit and withdraw funds
- Place bets between $1 and $10
- Win or lose money based on random game logic

It follows clean code practices and includes unit tests for edge cases and game behavior.

## Features

- **Deposit** funds into your wallet.
- **Withdraw** money from your balance.
- **Place bets** with fair random outcomes.
- **Win multipliers**:
  - 50% chance to lose the bet
  - 40% chance to win 0.1x–2.0x the bet
  - 10% chance to win 2.0x–10.0x the bet
- **Command-based console interaction**
- **Unit tested** logic using XUnit

## Prerequisites

- [.NET SDK 6.0+](https://dotnet.microsoft.com/download)
- A terminal or IDE that supports running .NET applications

## Installation

1. **Clone the repository**:
   ```bash
   git clone https://github.com/LbStoyanov/GamingWalletSimulator.git

2. **Navigate to the project directory**:
   ```bash
   cd GamingWalletSimulator
   
3. ** Build the project **:
   ```bash
   dotnet build

4. **  Run the application**:
   ```bash
   dotnet run

## Available Commands
 
5. **Deposit money into your wallet**:
   ```bash
   deposit 100

6. **Withdraw money from your wallet**:
   ```bash
   withdraw 50

7. **Place a bet between $1 and $10**:
   ```bash
   bet 5

   
8. **Exit the game**:
   ```bash
   exit
