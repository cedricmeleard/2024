Feature: Rock Paper Scissors Game

    Scenario Outline: Draw
        Given Player 1 chooses <choice>
        And Player 2 chooses <choice>
        When they play
        Then the result should be Draw because same choice

    Examples:
          | choice |
          | 🪨     |
          | ✂️     |
          | 📄     |
          | 🖖     |
          | 🦎     |

    Scenario: Player 1 wins over player 2
        Given Player 1 chooses <winnerChoice>
        And Player 2 chooses <looserChoice>
        When they play
        Then the result should be Player 1 because <reason>

    Examples: 
      | winnerChoice | looserChoice | reason                      |
      | 🪨           | ✂️           | rock crushes scissors       |
      | 🪨           | 🦎           | rock crushes lizard         |
      | 📄           | 🪨           | paper covers rock           |
      | 📄           | 🖖           | paper disproves spock       |
      | ✂️           | 📄           | scissors cuts paper         |
      | ✂️           | 🦎           | scissors decapitates lizard |
      | 🦎           | 🖖           | lizard poisons spock        |
      | 🦎           | 📄           | lizard eats paper           |
      | 🖖           | 🪨           | spock vaporizes rock        |
      | 🖖           | ✂️           | spock smashes scissors      |
   
    Scenario: Player 2 wins over player 1
        Given Player 1 chooses <looserChoice>
        And Player 2 chooses <winnerChoice>
        When they play
        Then the result should be Player 2 because <reason>  

    Examples: 
        | winnerChoice | looserChoice | reason                |
        | 🪨           | ✂️           | rock crushes scissors |
        | 🪨           | 🦎           | rock crushes lizard   |
        | 📄           | 🪨           | paper covers rock     |
        | 📄           | 🖖           | paper disproves spock |
        | ✂️           | 📄           | scissors cuts paper   |
        | ✂️           | 🦎           | scissors decapitates lizard |
        | 🦎           | 🖖           | lizard poisons spock        |
        | 🦎           | 📄           | lizard eats paper           |
        | 🖖           | 🪨           | spock vaporizes rock        |
        | 🖖           | ✂️           | spock smashes scissors      |